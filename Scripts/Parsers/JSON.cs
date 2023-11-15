#pragma warning disable IL3050
#pragma warning disable IL2026

using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace TelMe;

public partial class JSON {
    static JsonSerializerOptions Options { get; } = new() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string Parse<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(T payload, bool writeIndented = false) {
        JSON.Options.WriteIndented = writeIndented;
        return JsonSerializer.Serialize(payload, JSON.Options);
    }
}
