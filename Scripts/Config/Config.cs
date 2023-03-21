using System;
using Microsoft.Extensions.Configuration;

namespace TelMe;

public class ConfigLoader {
    public static IConfigurationRoot Load() => new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                                         .AddJsonStream(new Resources().GetStream("appsettings.json"))
                                                                         .AddEnvironmentVariables()
                                                                         .Build();
}
