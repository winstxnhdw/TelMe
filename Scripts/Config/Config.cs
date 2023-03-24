using System;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace TelMe;

public class ConfigLoader {
    public static IConfigurationRoot Load() =>
        new ConfigurationBuilder().AddIniFile($"{Path.GetDirectoryName(Environment.ProcessPath)}/settings.ini")
                                  .Build();
}
