using System;
using System.Net;
using System.Threading.Tasks;

namespace TelMe;

public sealed class TelMeService {
    Config Config { get; }

    public TelMeService() {
        this.Config = new Config();
    }

    public async Task<HttpStatusCode> NotifyStartup(DateTimeOffset startupTime) {
        const string TimeFormat = "dd/MM/yyyy hh:mm:ss tt";
        string singaporeTime = startupTime.ToOffset(TimeSpan.FromHours(8)).ToString(TimeFormat);
        string localTime = startupTime.ToLocalTime().ToString(TimeFormat);

        MailRequest mailRequest = new() {
            To = new string[] { this.Config.GetValue<string>("EMAIL_TO") },
            From = this.Config.GetValue<string>("EMAIL_FROM"),
            Subject = "TelMe has started!",
            Body = $"TelMe has started at {localTime} ({singaporeTime} SGT)"
        };

        return await new Requests().Post(this.Config.GetValue<string>("SERVER_ENDPOINT"), mailRequest);
    }
}
