using System;
using System.Threading.Tasks;
using TelMe;

sealed class TelMeService {
    Config Config { get; }

    TelMeService() => this.Config = new Config();

    internal async Task<Result> NotifyStartup(DateTimeOffset startupTime) {
        if (this.Config.GetValue<string>("EMAIL_TO") is not string emailTo) {
            return new Result() {
                Message = "EMAIL_TO key is not set"
            };
        }

        if (this.Config.GetValue<string>("EMAIL_FROM") is not string emailFrom) {
            return new Result() {
                Message = "EMAIL_FROM key is not set"
            };
        }

        if (this.Config.GetValue<string>("SERVER_ENDPOINT") is not string serverEndpoint) {
            return new Result() {
                Message = "SERVER_ENDPOINT key is not set"
            };
        }

        const string TimeFormat = "dd/MM/yyyy hh:mm:ss tt";
        string singaporeTime = startupTime.ToOffset(TimeSpan.FromHours(8)).ToString(TimeFormat);
        string localTime = startupTime.ToLocalTime().ToString(TimeFormat);

        MailRequest mailRequest = new() {
            To = [emailTo],
            From = emailFrom,
            Subject = "TelMe has started!",
            Html = $"TelMe has started at {localTime} ({singaporeTime} SGT)"
        };

        using Requests requests = new();
        _ = await requests.Post(serverEndpoint, mailRequest);

        return new Result() {
            Success = true
        };
    }
}
