using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TelMe;

public class Requests : IDisposable {
    HttpClient HttpClient { get; }

    public Requests() {
        this.HttpClient = new HttpClient();
    }

    static StringContent SerialiseJSON<T>(T payload) => new(JSON.Parse(payload), Encoding.UTF8, "application/json");

    public async Task<HttpStatusCode> Post<T>(string endpoint, T payload) {
        using HttpResponseMessage request = await this.HttpClient.PostAsync(endpoint, Requests.SerialiseJSON(payload));
        return request.StatusCode;
    }

    public void Dispose() {
        this.HttpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
