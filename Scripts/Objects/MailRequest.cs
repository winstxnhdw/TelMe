namespace TelMe;

public readonly struct MailRequest {
    public string[] To { get; }
    public string From { get; }
    public string Subject { get; }
    public string Html { get; }

    public MailRequest(string[] to, string from, string subject, string body) {
        this.To = to;
        this.From = from;
        this.Subject = subject;
        this.Html = body;
    }

    public MailRequest(string to, string from, string subject, string body) : this(new[] { to }, from, subject, body) { }

    public override string ToString() => JSON.Parse(this, true);
}
