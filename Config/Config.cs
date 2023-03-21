using System.IO;
using Microsoft.Extensions.Configuration;

namespace TelMe.Config;

public class ConfigLoader {
#if DEBUG

    public static IConfigurationRoot Load() {
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                         .AddJsonFile("appsettings.json", true)
                                         .AddEnvironmentVariables()
                                         .Build();
    }

#else

    public static IConfigurationRoot Load() {
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("appsettings.json", true)
                                      .AddEnvironmentVariables()
                                      .Build();
    }

#endif
}
