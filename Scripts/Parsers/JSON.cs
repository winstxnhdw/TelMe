using System.Text.Json;

namespace TelMe;

public partial class JSON {
    static JsonSerializerOptions Options { get; } = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string Parse<T>(T payload, bool writeIndented = false) {
        JSON.Options.WriteIndented = writeIndented;
        return JsonSerializer.Serialize(payload, JSON.Options);
    }
}
