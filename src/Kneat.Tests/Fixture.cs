using Kneat.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Kneat.Tests
{
    public class Fixture : IDisposable
    {
        public IConfigurationRoot Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public Fixture()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var services = new ServiceCollection();

            services.ConfigureServices(Configuration);

            ServiceProvider = services.BuildServiceProvider();

        }

        public void Dispose()
        {
            Configuration = null;
            ServiceProvider = null;
        }
    }
}
