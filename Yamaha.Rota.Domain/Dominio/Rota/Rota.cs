﻿namespace Yamaha.Rota.Domain.Dominio.Rota
{
    public class Rota
    {   
        public int Id { get; set; }
        public string Origem { get; set; }      
        public string Destino { get; set; }       
        public decimal Valor { get; set; }   
    }
}
