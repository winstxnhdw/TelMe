using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TelMe;

public class Worker(TelMeService service, ILogger<Worker> logger) : BackgroundService {
    TelMeService Service { get; } = service;
    ILogger<Worker> Logger { get; } = logger;
    Action<ILogger, string, Exception?> LogMessage { get; } = LoggerMessage.Define<string>(LogLevel.Information, 0, "{Message}");
    Action<ILogger, string, Exception?> LogError { get; } = LoggerMessage.Define<string>(LogLevel.Error, 0, "{Message}");

    async Task ExecuteService() {
        DateTimeOffset now = DateTimeOffset.Now;
        this.LogMessage(this.Logger, $"Worker running at: {now}", null);
        Result result = await this.Service.NotifyStartup(now);

        if (!result.Success) {
            this.LogError(this.Logger, $"Worker failed at: {now}", new Exception(result.Message));
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        try {
            await this.ExecuteService();
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
        Environment.Exit(0);
    }
}
