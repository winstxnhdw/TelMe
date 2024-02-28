using System;
using System.IO;
using Microsoft.Extensions.Configuration;

class Config {
    IConfigurationRoot ConfigRoot { get; } = Config.Load();

    internal T? GetValue<T>(string key) => this.ConfigRoot.GetValue<T>($"User:{key}");

    static IConfigurationRoot Load() =>
        new ConfigurationBuilder().AddIniFile($"{Path.GetDirectoryName(Environment.ProcessPath)}/settings.ini")
                                  .Build();
}
