using App.Console.Configuration;
using Microsoft.Extensions.Configuration;

namespace App.Console.Extensions;

public static class IConfigurationExtensions
{
    public static ApplicationInformation? GetApplicationInformation(this IConfiguration configuration)
        => configuration.GetSection("Application").Get<ApplicationInformation>();

    public static IConfiguration GetCurrentConfiguration(this IConfigurationBuilder builder)
        => builder
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile(path: "configuration/appsettings.json", optional: false)
        .Build();

    public static string GetDataPath(this IConfiguration configuration)
    {
        string? dataPath = configuration.GetSection("DataPath").Get<string>();
        
        if (string.IsNullOrEmpty(dataPath))
            return string.Empty;

        return $"{Environment.CurrentDirectory}\\{dataPath}";
    }
}
