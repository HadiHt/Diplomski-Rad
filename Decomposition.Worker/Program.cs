using AutoMapper;
using Decomposition.Worker;
using Decomposition.Worker.Mappers;
using Decomposition.Worker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;


try
{
    IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) => ConfigureConfiguration(builder, context.HostingEnvironment.EnvironmentName))
        .UseDefaultServiceProvider((context, options) => { options.ValidateOnBuild = true; options.ValidateScopes = true; })
        .UseWindowsService();

    // Add services to the container.
    
    hostBuilder.ConfigureServices((context, services) =>
    {
        services.AddSingleton<MessagesHandler>();
        services.AddHostedService<Worker>();
        services.AddAutoMapper(typeof(OrderWriteDtoToOrderModel));

    });

    var host = hostBuilder.Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    
}
finally
{
    
}


static void ConfigureConfiguration(IConfigurationBuilder configuration, string environment)
{

    configuration
        .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Config"))
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
}
