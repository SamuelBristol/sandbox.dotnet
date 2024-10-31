using App.Console.Impl.Strategy;
using App.Console.Interfaces.Strategy;

namespace App.Console.Impl;

public static class Factory
{
    public static FileProcessor<T> CreateProcessor<T>(string path)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();
        return extension switch
        {
            ".csv" => new FileProcessor<T>(path, new CSVStrategy<T>()),
            ".json" => new FileProcessor<T>(path, new JsonStrategy<T>()),
            _ => new FileProcessor<T>(path)
        };
    }

    public static IFileProcessorStrategy<T> CreateStrategy<T>(string path)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();
        return extension switch
        {
            ".csv" => new CSVStrategy<T>(),
            ".json" => new JsonStrategy<T>(),
            _ => throw new InvalidOperationException($"No matching strategy for: {extension}")
        };
    }
}
