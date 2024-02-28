using System;
using System.IO;
using System.Threading.Tasks;
using CliWrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

async Task<bool> ParseArgs(string serviceName) {
    if (args.Length is not 1) return false;

    Command serviceControlManager = Cli.Wrap("sc");

    if (args[0] is "install") {
        Console.WriteLine($"Installing {serviceName}..");

        _ = await serviceControlManager.WithArguments(["create", serviceName, $"binPath={Directory.GetCurrentDirectory()}\\{serviceName}.exe", "start=delayed-auto"])
                                       .ExecuteAsync();

        _ = await serviceControlManager.WithArguments(["start", serviceName])
                                       .ExecuteAsync();
    }

    else if (args[0] is "uninstall") {
        Console.WriteLine($"Uninstalling {serviceName}..");

        _ = await serviceControlManager.WithArguments(["stop", serviceName])
                                       .WithValidation(CommandResultValidation.None)
                                       .ExecuteAsync();

        _ = await serviceControlManager.WithArguments(["delete", serviceName])
                                       .ExecuteAsync();
    }

    else {
        Console.WriteLine("Invalid argument.");
    }

    return true;
}

async Task Main() {
    const string ServiceName = "TelMe";
    bool isRunningInstaller = await ParseArgs(ServiceName);

    if (isRunningInstaller) {
        return;
    }

    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    IServiceCollection services = builder.Services;
    _ = builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

    _ = services.AddWindowsService(options => options.ServiceName = ServiceName)
                .AddSingleton<TelMeService>()
                .AddHostedService<Worker>();

    LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(services);

    builder.Build()
           .Run();
}

await Main();
