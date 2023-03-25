using Newtonsoft.Json;

namespace TelMe;

public struct MailRequest {
    [JsonProperty("to")]
    public string[] To { get; set; }

    [JsonProperty("from")]
    public string From { get; set; }

    [JsonProperty("subject")]
    public string Subject { get; set; }

    [JsonProperty("html")]
    public string Body { get; set; }
}
