using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TelMe;

public class Config {
    IConfigurationRoot ConfigRoot { get; }

    public Config() {
        this.ConfigRoot = Config.Load();
    }

    public T? GetValue<T>(string key) => this.ConfigRoot.GetValue<T>($"User:{key}");

    static IConfigurationRoot Load() =>
        new ConfigurationBuilder().AddIniFile($"{Path.GetDirectoryName(Environment.ProcessPath)}/settings.ini")
                                  .Build();

}
