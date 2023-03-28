using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelMe;

public class Worker : BackgroundService {
    TelMeService Service { get; }
    ILogger<Worker> Logger { get; }

    Action<ILogger, string, Exception> LogMessage { get; }
    Action<ILogger, string, Exception> LogError { get; }

    public Worker(TelMeService service, ILogger<Worker> logger) {
        this.LogMessage = LoggerMessage.Define<string>(
            LogLevel.Information, 0, "{Message}"
        );

        this.LogError = LoggerMessage.Define<string>(
            LogLevel.Error, 0, "{Message}"
        );

        this.Logger = logger;
        this.Service = service;
    }

    async Task ExecuteService(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            DateTimeOffset now = DateTimeOffset.Now;
            this.LogMessage(this.Logger, $"Worker running at: {now}", null);
            _ = await this.Service.NotifyStartup(now);
            this.Exit();
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        try {
            await this.ExecuteService(stoppingToken);
        }

        catch (TaskCanceledException exception) {
            this.LogMessage(this.Logger, $"Worker cancelled at: {DateTimeOffset.Now}", exception);
        }

        catch (Exception exception) {
            this.LogError(this.Logger, $"Worker failed at: {DateTimeOffset.Now}", exception);
        }

        finally {
            this.Exit();
        }
    }

    void Exit() {
        this.Dispose();
        Environment.Exit(1);
    }
}
