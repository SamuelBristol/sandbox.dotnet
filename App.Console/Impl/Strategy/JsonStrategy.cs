using App.Console.Interfaces.Strategy;
using System.Text.Json;

namespace App.Console.Impl.Strategy;

public class JsonStrategy<T> : IFileProcessorStrategy<T>
{
    public async Task<IEnumerable<T>> ReadFile(string path)
    {
        if (Path.GetExtension(path) != ".json")
            throw new InvalidOperationException("Must process a .json file.");

        using StreamReader reader = new(path);

        return JsonSerializer.Deserialize<IEnumerable<T>>(await reader.ReadToEndAsync()) ?? [];
    }
}
