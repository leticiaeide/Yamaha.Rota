namespace Yamaha.Rota.Domain.Dominio.Rota
{
    public class RotaConexao
    {
        public RotaConexao(string origem, string destino, decimal valor, int numeroRota)
        {
            NumeroRota = numeroRota;
            Origem = origem;
            Destino = destino;
            Valor = valor;
        }

        public int NumeroRota { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
    }
}
