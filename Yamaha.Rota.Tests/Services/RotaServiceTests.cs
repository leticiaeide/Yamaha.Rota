using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;
using Yamaha.Rota.Domain.Dominio.Rota.Service;

namespace Yamaha.Rota.Tests.Services
{
    public class RotaServiceTests
    {
        private readonly IRotaService _service;
        private readonly IRotaRepository _repository;    

        public RotaServiceTests()
        {
            _repository = Substitute.For<IRotaRepository>();            

            _service = new RotaService(_repository);
        }


        [Fact]
        public async Task Deve_Salvar_Rota_Async()
        {           
            var rota = ObterRota();
            _repository.ObterRotaAsync(Arg.Any<Domain.Dominio.Rota.Rota>()).Returns(rota);
            _repository.IncluirAsync(Arg.Any<Domain.Dominio.Rota.Rota>()).Returns(true);
            string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"Arquivos\Rota\rotas.csv");
            var retorno = _service.SalvarRotaAsync(caminhoArquivo).Result;

            await _repository.Received().ObterRotaAsync(Arg.Any<Domain.Dominio.Rota.Rota>());
            await _repository.Received().AtualizarAsync(Arg.Any<Domain.Dominio.Rota.Rota>());
            await _repository.DidNotReceive().IncluirAsync(Arg.Any<Domain.Dominio.Rota.Rota>());

            Assert.True(retorno.Count() == 7);           
        }

        [Fact]
        public async Task Nao_Deve_Salvar_Rota_Arquivo_Path_Nao_Encontrado()
        {
            var rota = ObterRota();
            _repository.ObterRotaAsync(Arg.Any<Domain.Dominio.Rota.Rota>()).Returns(rota);
            _repository.IncluirAsync(Arg.Any<Domain.Dominio.Rota.Rota>()).Returns(true);

            var exception = Assert.ThrowsAsync<Exception>(() => _service.SalvarRotaAsync("Arquivo inválido")).Result;
            await _repository.DidNotReceive().ObterRotaAsync(Arg.Any<Domain.Dominio.Rota.Rota>());
            await _repository.DidNotReceive().AtualizarAsync(Arg.Any<Domain.Dominio.Rota.Rota>());
            await _repository.DidNotReceive().IncluirAsync(Arg.Any<Domain.Dominio.Rota.Rota>());

            Assert.Equal("Arquivo de rotas não encontrado!", exception.Message);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_Rota_Async_Extensao_Arquivo_Invalido()
        {
            var rota = ObterRota();
            _repository.ObterRotaAsync(Arg.Any<Domain.Dominio.Rota.Rota>()).Returns(rota);
            _repository.IncluirAsync(Arg.Any<Domain.Dominio.Rota.Rota>()).Returns(true);

            string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"Arquivos\Rota\rotas.txt");

            var exception = Assert.ThrowsAsync<Exception>(() => _service.SalvarRotaAsync(caminhoArquivo)).Result;
            await _repository.DidNotReceive().ObterRotaAsync(Arg.Any<Domain.Dominio.Rota.Rota>());
            await _repository.DidNotReceive().AtualizarAsync(Arg.Any<Domain.Dominio.Rota.Rota>());
            await _repository.DidNotReceive().IncluirAsync(Arg.Any<Domain.Dominio.Rota.Rota>());

            Assert.Equal("Extensão do arquivo inválido!", exception.Message);
        }

        [Fact]
        public async Task Nao_Deve_Obter_Melhor_Rota_Async_Nenhuma_Rota_Encontrada()
        {
            var request = PreencherRotaRequest();
            var rotas = new List<Domain.Dominio.Rota.Rota>();

            _repository.ObterRotasAsync().Returns(rotas);

            var retorno = _service.ObterMelhorRotaAsync(request).Result;

            await _repository.Received().ObterRotasAsync();

            Assert.Equal("Nenhuma rota encontrada, realize o upload de Rotas!", retorno);
        }

        [Fact]
        public async Task Nao_Deve_Obter_Melhor_Rota_Async_Origem_Nao_Encontrada()
        {
            var request = PreencherRotaRequest();
            request.Origem = "Não existe";
            var rotas = ObterRotas();

            _repository.ObterRotasAsync().Returns(rotas);

            var retorno = _service.ObterMelhorRotaAsync(request).Result;

            await _repository.Received().ObterRotasAsync();

            Assert.Equal("Rota não encontrada para a origem: Não existe", retorno);
        }

        [Fact]
        public async Task Nao_Deve_Obter_Melhor_Rota_Async_Destino_Nao_Encontrada()
        {
            var request = PreencherRotaRequest();
            request.Destino = "Não existe";
            var rotas = ObterRotas();
            _repository.ObterRotasAsync().Returns(rotas);

            var retorno = _service.ObterMelhorRotaAsync(request).Result;

            await _repository.Received().ObterRotasAsync();

            Assert.Equal("Rota não encontrada para o destino: Não existe", retorno);
        }

        [Fact]
        public async Task Deve_Obter_Melhor_Rota_Async()
        {
            var request = PreencherRotaRequest();
            request.Destino = "GUA";
            request.Destino = "CDG";
            var rotas = ObterRotas();
            _repository.ObterRotasAsync().Returns(rotas);

            var retorno = _service.ObterMelhorRotaAsync(request).Result;

            await _repository.Received().ObterRotasAsync();

            Assert.Equal("1. GUA - BRC - SCL - ORL ao custo de $40", retorno);
        }

        private Domain.Dominio.Rota.Rota ObterRota()
        {
            return new Domain.Dominio.Rota.Rota { Id = 1, Destino = "GUA", Origem = "BRC", Valor = 10 };
        }

        private Domain.Dominio.Rota.Arguments.RotaRequest PreencherRotaRequest()
        {
            return new Domain.Dominio.Rota.Arguments.RotaRequest("GUA", "BRC", "10");
        }

        private List<Domain.Dominio.Rota.Rota> ObterRotas()
        {
            return new List<Domain.Dominio.Rota.Rota>
            {
                new Domain.Dominio.Rota.Rota { Id = 1, Origem = "GUA", Destino = "BRC", Valor = 10 },
                new Domain.Dominio.Rota.Rota { Id = 2, Origem = "BRC", Destino = "SCL", Valor = 5 },
                new Domain.Dominio.Rota.Rota { Id = 3, Origem = "GUA", Destino = "CDG", Valor = 75 },
                new Domain.Dominio.Rota.Rota { Id = 4, Origem = "GUA", Destino = "SCL", Valor = 20 },
                new Domain.Dominio.Rota.Rota { Id = 5, Origem = "GUA", Destino = "ORL", Valor = 56 },
                new Domain.Dominio.Rota.Rota { Id = 6, Origem = "ORL", Destino = "CDG", Valor = 5 },
                new Domain.Dominio.Rota.Rota { Id = 7, Origem = "SCL", Destino = "ORL", Valor = 20 },
            };
        }
    }
}
