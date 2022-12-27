using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamaha.Rota.Domain.Dominio.Rota.Arguments;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;

namespace Yamaha.Rota.Domain.Dominio.Rota.Service
{
    public class RotaService : IRotaService
    {

        private readonly IRotaRepository _repository;

        public RotaService(IRotaRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ObterMelhorRotaAsync(RotaRequest request)
        {
            return PreencherMelhorRota(request.Origem, request.Destino);
        }

        public async Task<IEnumerable<Rota>> ObterRotasAsync()
        {
            return await _repository.ObterRotasAsync();
        }

        public async Task<IEnumerable<Rota>> SalvarRotaAsync(string caminhoArquivo)
        {
            if (!ArquivoPathExiste(caminhoArquivo))
            {
                throw new Exception("Arquivo de rotas não encontrado!");
            }

            if (!ArquivoExtensaoValido(caminhoArquivo))
            {
                throw new Exception("Extensão do arquivo inválido!");
            }

            var rotas = new List<Rota>();
            var rotasRequest = new List<RotaRequest>();

            string linha = "";
            string[] linhaseparada = null;
            StreamReader reader = new StreamReader(caminhoArquivo, Encoding.UTF8, true);
            while (true)
            {
                linha = reader.ReadLine();
                if (linha == null)
                    break;

                if (linha != "origem;destino;valor")
                {
                    linhaseparada = linha.Split(';');
                    rotasRequest.Add(new RotaRequest(linhaseparada[0], linhaseparada[1], linhaseparada[2]));
                }
            }

            foreach (var request in rotasRequest)
            {
                var resultValidator = new RotaValidacao().Validate(request);

                if (resultValidator.IsValid)
                {
                    var rota = new Rota { Destino = request.Destino, Origem = request.Origem, Valor = decimal.Parse(request.Valor) };
                    var rotaAtualiza = await _repository.ObterRotaAsync(rota);

                    if (rotaAtualiza == null)
                    {
                        await _repository.IncluirAsync(rota);
                    }
                    else
                    {
                        rota.Id = rotaAtualiza.Id;
                        await _repository.AtualizarAsync(rota);
                    }

                    rotas.Add(rota);
                }
            }
            return rotas;
        }

        private string PreencherMelhorRota(string origem, string destino)
        {
            var rotasExistentes = _repository.ObterRotasAsync().Result;

            if (!rotasExistentes.Any())
            {
                return $"Nenhuma rota encontrada, realize o upload de Rotas!";
            }

            if (!rotasExistentes.Where(c => c.Origem == origem).Any())
                return $"Rota não encontrada para a origem: {origem}";

            if (!rotasExistentes.Where(c => c.Destino == destino).Any())
                return $"Rota não encontrada para o destino: {destino}";

           return new RotaMelhorPreco().CalcularRota(origem, rotasExistentes);         

        }

        private static bool ArquivoPathExiste(string caminhoArquivo)
        {
            return !string.IsNullOrEmpty(caminhoArquivo) && File.Exists(caminhoArquivo);
        }

        private static bool ArquivoExtensaoValido(string caminhoArquivo)
        {
            string extensao = Path.GetExtension(caminhoArquivo);

            return extensao == ".csv";
        }
    }        
}
