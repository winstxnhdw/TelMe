using System.Text.Json;

class JSON {
    static JsonSerializerOptions Options { get; } = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    internal static string Parse<T>(T payload, bool writeIndented = false) {
        JSON.Options.WriteIndented = writeIndented;
        return JsonSerializer.Serialize(payload, JSON.Options);
    }
}
