namespace TelMe;

public readonly struct MailRequest {
    public string[] To { get; init; }
    public string From { get; init; }
    public string Subject { get; init; }
    public string Html { get; init; }

    public override string ToString() => JSON.Parse(this, true);
}
