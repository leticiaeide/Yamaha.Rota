using System;
using System.Collections.Generic;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;
using Yamaha.Rota.Domain.Dominio.Rota.Service;

namespace Yamaha.Rota.Domain.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var result = new Dictionary<Type, Type>
            {                
                { typeof(IRotaService), typeof(RotaService) }
            };
            return result;
        }
    }
}
