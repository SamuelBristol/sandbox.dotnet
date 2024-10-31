using System.Diagnostics;
using System.Text.RegularExpressions;

namespace App.Console.Tests;

public class CSVFileReaderShould
{
    private static readonly string Logs_FilePath = "C:\\Users\\samue\\source\\repos\\Sandbox\\App.Console.Tests\\Logs\\";

    private static readonly string[] TestStrings =
    {
        string.Empty,      // Empty string
        "a",               // 1 character string
        new ('a', 1000),   // 1000 characters
        new ('a', 10000),  // 10000 characters
        new ('a', 100000), // 100000 characters
        new ('a', 1000000) // 1000000 characters
    };

    private static readonly bool ShouldRunTests = true;

    //[Theory(Skip = nameof(ShouldRunTests))]
    [Theory]
    [MemberData(nameof(TestData))]
    public void BenchmarkDelimiterPerformance(string input, int iterations)
    {
        Stopwatch stopwatch = new();
        string path = Path.Combine(Logs_FilePath, $"{nameof(BenchmarkDelimiterPerformance)}.csv");
        using StreamWriter writer = new(path, true);
        foreach(int iteration in Enumerable.Range(0, iterations))
        {
            // LINQ Count Method
            stopwatch.Start();
            DetermineDelimiterUsingLinq(input);
            stopwatch.Stop();
            var linqTime = stopwatch.Elapsed;

            // Regex Method
            stopwatch.Restart();
            DetermineDelimiterUsingRegex(input);
            stopwatch.Stop();
            var regexTime = stopwatch.Elapsed;

            string output = $"{DateTime.UtcNow}, {input.Length}, {linqTime.TotalMilliseconds}, {regexTime.TotalMilliseconds}";
            writer.WriteLine(output);
        }
    }

    public static IEnumerable<object[]> TestData()
    {
        var iterations = new int[] { 1, 10, 100, 1000 };

        foreach (var testString in TestStrings)
        {
            foreach (var iteration in iterations)
            {
                yield return new object[] { testString, iteration };
            }
        }
    }

    private char DetermineDelimiterUsingLinq(string input)
    {
        var potentialDelimiters = new char[] { ',', '\t', ';', ' ' };
        return potentialDelimiters.OrderByDescending(d => input.Count(c => c == d)).FirstOrDefault();
    }

    private char DetermineDelimiterUsingRegex(string input)
    {
        var potentialDelimiters = new char[] { ',', '\t', ';', ' ' };
        return potentialDelimiters.OrderByDescending(d => Regex.Matches(input, Regex.Escape(d.ToString())).Count).FirstOrDefault();
    }
}
