using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yamaha.Rota.Data.Repositories;
using Yamaha.Rota.Domain.Dominio.Rota.Arguments;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;
using Yamaha.Rota.Domain.Dominio.Rota.Service;

namespace Yamaha.Rota
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Informe o diretório do arquivo de rotas: ");
            string arquivo = Console.ReadLine();


            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var rotaService = serviceProvider.GetService<IRotaService>();

            var rotas = rotaService.SalvarRotaAsync(arquivo).Result;

            if (rotas.Any())
            {
                var rota = new RotaRequest();

                Console.Write("Digite a localização de origem:");
                rota.Origem = Console.ReadLine();

                Console.Write("Digite o destino desejado:");
                rota.Destino = Console.ReadLine();

                var melhorRota = rotaService.ObterMelhorRotaAsync(rota).Result;

                Console.WriteLine(melhorRota);
            }

            Console.WriteLine("Pressione qualquer tecla para encerrar!");
            Console.ReadKey();

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRotaService, RotaService>();
            serviceCollection.AddSingleton<IRotaRepository, RotaRepository>();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<DbSession>();
        }
    }
}


