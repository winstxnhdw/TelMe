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

        MailRequest mailRequest = new(
            this.Config.GetValue<string>("EMAIL_TO"),
            this.Config.GetValue<string>("EMAIL_FROM"),
            "TelMe has started!",
            $"TelMe has started at {localTime} ({singaporeTime} SGT)"
        );

        using Requests requests = new();
        return await requests.Post(this.Config.GetValue<string>("SERVER_ENDPOINT"), mailRequest);
    }
}
