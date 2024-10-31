using App.Console.Interfaces.Strategy;
using CsvHelper;
using System.Globalization;

namespace App.Console.Impl.Strategy;

public class CSVStrategy<T> : IFileProcessorStrategy<T>
{
    public async Task<IEnumerable<T>> ReadFile(string path)
    {
        if (Path.GetExtension(path) != ".csv")
            throw new InvalidOperationException("Must process a .csv file.");

        using StreamReader reader = new(path);
        using CsvReader csvReader = new(reader, CultureInfo.InvariantCulture);

        return await Task.FromResult(csvReader.GetRecords<T>().ToList());
    }
}
