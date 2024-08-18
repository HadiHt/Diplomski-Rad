using AutoMapper;
using Decomposition.Worker;
using Decomposition.Worker.Contexts;
using Decomposition.Worker.Mappers;
using Decomposition.Worker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

try
{
    IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
            ConfigureConfiguration(builder, context.HostingEnvironment.EnvironmentName))
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateOnBuild = true;
            options.ValidateScopes = true;
        })
        .UseWindowsService()
        .ConfigureServices((context, services) =>
        {
            // Access configuration via context.Configuration
            var configuration = context.Configuration;

            services.AddScoped<MessagesHandler>();
            services.AddScoped<CamundaService>();

            services.AddHostedService<Worker>();
            services.AddAutoMapper(typeof(OrderWriteDtoToOrderModel));

            services.AddDbContext<CamundaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CamundaDatabase")));
        });

    var host = hostBuilder.Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    // Handle exceptions
}
finally
{
    // Perform cleanup if necessary
}

static void ConfigureConfiguration(IConfigurationBuilder configuration, string environment)
{
    configuration
        .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Config"))
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .AddEnvironmentVariables();
}
