using Microsoft.Extensions.Configuration;

namespace TelMe;

public class ConfigLoader {
    public static IConfigurationRoot Load() =>
        new ConfigurationBuilder().AddJsonStream(new Resources().GetStream("appsettings.json"))
                                  .AddEnvironmentVariables()
                                  .Build();
}
