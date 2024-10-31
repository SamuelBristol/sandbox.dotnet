namespace App.Console.Interfaces.Strategy;

public interface IFileProcessorStrategy<T>
{
    Task<IEnumerable<T>> ReadFile(string path);
}
