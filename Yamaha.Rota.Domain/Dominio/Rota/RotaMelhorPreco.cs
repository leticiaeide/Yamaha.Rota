using System.Collections.Generic;
using System.Linq;

namespace Yamaha.Rota.Domain.Dominio.Rota
{
    public class RotaMelhorPreco
    {   
        public IList<RotaConexao> Conexoes { get; private set; }
        public int NumeroRota { get; private set; }
        public RotaMelhorPreco()
        {
            NumeroRota = 1;
            Conexoes = new List<RotaConexao>();           
        }

        public string CalcularRota(string origem, IEnumerable<Rota> rotasExistentes)
        {
            var rotasOrigem = rotasExistentes.Where(re => re.Origem == origem).ToList();

            CalcularMelhorRota(rotasOrigem, rotasExistentes);

            var conexoes = Conexoes.GroupBy(c => c.NumeroRota)
                .Select(r => new
                {
                    NumeroRota = r.Key,
                    Valor = r.Sum(con => con.Valor),
                }).ToList();


            var melhorConexao = conexoes.OrderBy(mc => mc.Valor).First();

            var nomeConexao = string.Empty;
            foreach (var item in Conexoes.Where(a => a.NumeroRota == melhorConexao.NumeroRota))
            {
                nomeConexao += $"{item.Origem} - ";
            }

            nomeConexao = nomeConexao.Substring(0, nomeConexao.LastIndexOf("-")).Trim();

            return $"{melhorConexao.NumeroRota}. {nomeConexao} ao custo de ${melhorConexao.Valor}";
        }

        public void CalcularMelhorRota(IList<Rota> rotasOrigem, IEnumerable<Rota> rotasExistentes)
        {
            foreach (var item in rotasOrigem)
            {
                Conexoes.Add(new RotaConexao(item.Origem, item.Destino, item.Valor, NumeroRota));

                CalcularMelhorRota(rotasExistentes.Where(r => r.Origem == item.Destino).ToList(), rotasExistentes);
                NumeroRota = Conexoes.Max(c => c.NumeroRota) + 1;
            }
        }
    }
}
