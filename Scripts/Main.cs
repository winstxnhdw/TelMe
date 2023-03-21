using System;
using System.IO;
using CliWrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using TelMe;


const string ServiceName = "TelMe";

if (args is { Length: 1 }) {
    Command serviceControlManager = Cli.Wrap("sc");

    if (args[0] is "install") {
        Console.WriteLine($"Installing {ServiceName}..");

        _ = await serviceControlManager.WithArguments(new[] { "create", ServiceName, $"binPath={Directory.GetCurrentDirectory()}\\{ServiceName}.exe", "start=auto" })
                                       .ExecuteAsync();

        _ = await serviceControlManager.WithArguments(new[] { "start", ServiceName })
                                       .ExecuteAsync();
    }

    else if (args[0] is "uninstall") {
        Console.WriteLine($"Uinstalling {ServiceName}..");

        _ = await serviceControlManager.WithArguments(new[] { "stop", ServiceName })
                                       .WithValidation(CommandResultValidation.None)
                                       .ExecuteAsync();

        _ = await serviceControlManager.WithArguments(new[] { "delete", ServiceName })
                                       .ExecuteAsync();
    }

    return;
}

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
IServiceCollection services = builder.Services;

services.AddWindowsService(options => options.ServiceName = ServiceName)
        .AddSingleton<TelMeService>()
        .AddHostedService<Worker>();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(services);

builder.Build()
       .Run();
