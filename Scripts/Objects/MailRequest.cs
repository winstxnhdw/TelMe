using System.Text.Json;
using System.Text.Json.Serialization;

namespace TelMe;

public readonly struct MailRequest {
    public string[] To { get; init; }
    public string From { get; init; }
    public string Subject { get; init; }
    public string Html { get; init; }

    public override string ToString() => JSON.Parse(this, true);
}

[JsonSerializable(typeof(MailRequest))]
public partial class MailRequestContext : JsonSerializerContext { }

public partial class JSON {
    public static string Parse(MailRequest payload) {
        return JsonSerializer.Serialize(payload, MailRequestContext.Default.MailRequest);
    }
}
