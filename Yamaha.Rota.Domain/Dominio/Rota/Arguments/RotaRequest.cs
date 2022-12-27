namespace Yamaha.Rota.Domain.Dominio.Rota.Arguments
{
    public class RotaRequest
    {
        public RotaRequest()
        {

        }

        public RotaRequest(string origem, string destino, string valor)
        {
            Origem = origem;
            Destino = destino;
            Valor = valor;
        }
        
        public string Origem { get; set; }
   
        public string Destino { get; set; }
     
        public string Valor { get; set; }        
    }     
}
