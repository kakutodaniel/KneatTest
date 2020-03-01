using Kneat.Application.Services.External;
using Kneat.Application.Settings.External;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using Polly;
using Microsoft.Extensions.Logging;
using Kneat.Application.Services;
using Kneat.Application.Services.Interfaces;
using Kneat.Application.Services.External.Interfaces;

namespace Kneat.Application
{
    public static class KneatConfiguration
    {

        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddOptions();

            services.AddLogging(x =>
            {
                x.AddFile(config.GetSection("Logging"));
            });


            //compress json
            services.Configure<GzipCompressionProviderOptions>(opt => opt.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(opt => opt.Providers.Add<GzipCompressionProvider>());


            //settings
            services.Configure<SwapiSettings>(config.GetSection(SwapiSettings.Section));
            services.TryAddSingleton(resolver => resolver.GetRequiredService<IOptions<SwapiSettings>>().Value);

            //services
            services.TryAddSingleton<ISwapiService, SwapiService>();
            services.TryAddSingleton<IKneatService, KneatService>();


            //get http swapi settings
            var swapiSettings = config.GetSection(SwapiSettings.Section).Get<SwapiSettings>();

            //inject http info into interface class
            services.AddHttpClient<ISwapiService, SwapiService>(opt =>
            {
                opt.BaseAddress = new Uri(swapiSettings.BaseUrl);
                opt.DefaultRequestHeaders.Accept.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(swapiSettings.ContentType));

            }) //retry 2 times every 600 ms
            .AddTransientHttpErrorPolicy(opt => opt.WaitAndRetryAsync(2, x => TimeSpan.FromMilliseconds(600)));

        }
    }
}
