namespace TelMe;

readonly record struct Result {
    internal bool Success { get; init; }
    internal string? Message { get; init; }
}
