using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelMe;

public class Worker : BackgroundService, IDisposable {
    TelMeService Service { get; }
    ILogger<Worker> Logger { get; }

    Action<ILogger, string, Exception> LogMessage { get; }
    Action<ILogger, string, Exception> LogError { get; }

    public Worker(TelMeService service, ILogger<Worker> logger) {
        this.LogMessage = LoggerMessage.Define<string>(
            LogLevel.Information,
            0,
            "{Message}"
        );

        this.LogError = LoggerMessage.Define<string>(
            LogLevel.Error,
            0,
            "{Message}"
        );

        this.Logger = logger;
        this.Service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        try {
            while (!stoppingToken.IsCancellationRequested) {
                this.LogMessage(this.Logger, $"Worker running at: {DateTimeOffset.Now}", null);
                _ = await this.Service.NotifyStartup(DateTimeOffset.Now);
                this.Exit();
            }
        }

        catch (TaskCanceledException exception) {
            this.LogMessage(this.Logger, $"Worker cancelled at: {DateTimeOffset.Now}", exception);
        }

        catch (Exception exception) {
            this.LogError(this.Logger, $"Worker failed at: {DateTimeOffset.Now}", exception);
            this.Exit();
        }
    }

    void Exit() {
        this.Service.Dispose();
        this.Dispose();
        Environment.Exit(1);
    }
}
