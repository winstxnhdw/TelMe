# TelMe

`TelMe` is a native Windows background service for sending telemetry to the host.

## Requirements

- [.NET Core 7.0 SDK or later](https://dotnet.microsoft.com/en-us/download)

## Setup

Verify your email address. Read more [here](https://docs.aws.amazon.com/ses/latest/dg/creating-identities.html#verify-email-addresses-procedure).

```bash
dotnet run
```

Publish the application.

```ps1
dotnet publish -c Release
```
