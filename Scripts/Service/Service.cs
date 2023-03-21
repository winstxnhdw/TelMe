using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;

namespace TelMe;

public sealed class TelMeService : IDisposable {
    string EmailFrom { get; }
    string EmailTo { get; }

    IConfigurationRoot Config { get; }
    AmazonSimpleEmailServiceClient EmailClient { get; }

    public TelMeService() {
        this.Config = ConfigLoader.Load();
        this.EmailTo = this.Config.GetValue<string>("EMAIL_TO");
        this.EmailFrom = this.Config.GetValue<string>("EMAIL_FROM");

        string awsAccessKey = this.Config.GetValue<string>("AWS_ACCESS_KEY_ID");
        string awsSecretKey = this.Config.GetValue<string>("AWS_SECRET_ACCESS_KEY");
        this.EmailClient = new AmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey, RegionEndpoint.APSoutheast1);
    }

    public async Task<bool> NotifyStartup(DateTimeOffset startupTime) {
        if (this.Config.GetValue<bool>("VERIFY_EMAIL")) return await this.VerifyEmailIdentity();

        const string TimeFormat = "dd/MM/yyyy hh:mm:ss tt";
        string singaporeTime = startupTime.ToOffset(TimeSpan.FromHours(8)).ToString(TimeFormat);
        string localTime = startupTime.ToLocalTime().ToString(TimeFormat);

        Destination destination = new() {
            ToAddresses = new List<string>() { this.EmailTo }
        };

        Content subject = new() {
            Data = $"TelMe has started!"
        };

        Body body = new() {
            Text = new Content() {
                Data = $"TelMe has started at {localTime} ({singaporeTime} SGT)"
            }
        };

        Message message = new() {
            Subject = subject,
            Body = body
        };

        SendEmailResponse response = await this.EmailClient.SendEmailAsync(new SendEmailRequest() {
            Source = this.EmailFrom,
            Destination = destination,
            Message = message
        });

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<bool> VerifyEmailIdentity() {
        VerifyEmailIdentityResponse response =
            await this.EmailClient.VerifyEmailIdentityAsync(new VerifyEmailIdentityRequest() {
                EmailAddress = this.EmailFrom
            });

        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public void Dispose() => this.EmailClient.Dispose();
}
