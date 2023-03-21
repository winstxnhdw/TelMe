# TelMe

`TelMe` is a native Windows background service for sending telemetry to the host.

## Requirements

- [.NET Core 7.0 SDK or later](https://dotnet.microsoft.com/en-us/download)

## Setup

Populate the `appsettings.json` file in the root of the directory.

```bash
echo
"
{
  "EMAIL_FROM": "<sender-email-address>",
  "EMAIL_TO": "<recipient-email-address>",
  "AWS_ACCESS_KEY_ID": "<aws-access-key-id>",
  "AWS_SECRET_ACCESS_KEY": "<aws-secret-access-key>",
  "VERIFY_EMAIL": true
}" >> appsettings.json
```

Verify your email address by running the program once. Read more [here](https://docs.aws.amazon.com/ses/latest/dg/creating-identities.html#verify-email-addresses-procedure).

```ps1
dotnet run
```

Set the `VERIFY_EMAIL` property to `false` in the `appsettings.json` file and publish the application.

```ps1
dotnet publish -c Release
```

Move the `PublishDir` directory to `C:\Program Files\TelMe`.

```bash
mv TelMe C:/Program Files/TelMe
```

Install the service.

```ps1
C:\Program Files\TelMe\TelMe.exe install
```

## Uninstall

You may remove the service permanently by running the following.

```ps1
C:\Program Files\TelMe\TelMe.exe uninstall
```
