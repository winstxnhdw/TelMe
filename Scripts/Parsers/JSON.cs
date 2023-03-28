using System.Text.Json;

namespace TelMe;

public class JSON {
    public static string Parse<T>(T payload, bool writeIndented = false) =>
        JsonSerializer.Serialize(payload, new JsonSerializerOptions {
            WriteIndented = writeIndented,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
}
