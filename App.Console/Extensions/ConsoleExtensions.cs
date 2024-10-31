namespace App.Console.Extensions;

static class ConsoleExtensions
{
    /// <summary>
    /// Outputs each <typeparamref name="T"/> to standard output using ToString()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    public static void WriteToConsole<T>(this IEnumerable<T> items)
        => System.Console.WriteLine(string.Join(Environment.NewLine, items));
}
