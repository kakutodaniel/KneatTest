using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Kneat.Application;
using Microsoft.Extensions.Logging;
using Kneat.Application.Services;
using Kneat.Application.Contracts.External;
using System.Linq;
using System.Text;

namespace Kneat.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Launch();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Type the distance and press ENTER (ESC to cancel)");

                var t = ReadLineWithCancel();

                if (t == null)
                {
                    Environment.Exit(0);
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Processing...");

                var service = serviceProvider.GetService<KneatService>();

                long.TryParse(t, out var dist);

                var request = new GetStarShipRequest { Distance = dist };

                var result = service.Process(request).Result;

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine($" ===== Total starships: {result.Items.Count} ====== (Sorted by name)");
                foreach (var item in result.Items.OrderBy(x => x.Name))
                {
                    Console.WriteLine($"Name: {item.Name} - NumberStops: {item.NumberStops}");
                }

                if (!result.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.ErrorMessage);
                }

                Console.WriteLine(Environment.NewLine);
            }
        }

        //Returns null if ESC key pressed during input.
        private static string ReadLineWithCancel()
        {
            string result = null;

            var buffer = new StringBuilder();

            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }

            return result;
        }

        private static IServiceProvider Launch()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.dev.json", optional: false, reloadOnChange: true);


            var config = builder.Build();

            var services = new ServiceCollection();

            services.ConfigureServices(config);

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("=========== Initialize ===========");

            return serviceProvider;
        }
    }
}
