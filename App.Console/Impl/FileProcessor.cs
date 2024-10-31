using App.Console.Interfaces.Strategy;

namespace App.Console.Impl;

public class FileProcessor<T>(string filepath, IFileProcessorStrategy<T>? strategy = null)
{
    public string FilePath { get; set; } = filepath;
    public IFileProcessorStrategy<T>? CurrentStrategy { get; set; } = strategy;

    public Task<IEnumerable<T>> Process()
    {
        CurrentStrategy ??= Factory.CreateStrategy<T>(FilePath);

        return CurrentStrategy.ReadFile(FilePath);
    }
}
