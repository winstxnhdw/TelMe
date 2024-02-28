namespace TelMe;

readonly record struct MailRequest {
    internal required string[] To { get; init; }
    internal required string From { get; init; }
    internal required string Subject { get; init; }
    internal required string Html { get; init; }

    public override string ToString() => JSON.Parse(this, true);
}
