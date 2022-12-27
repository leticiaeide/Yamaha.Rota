using System;
using System.Collections.Generic;
using Yamaha.Rota.Data.Repositories;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;

namespace Yamaha.Rota.Data.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var references = new Dictionary<Type, Type>
            {
                 { typeof(IRotaRepository), typeof(RotaRepository) },

            };
            return references;
        }
    }
}
