using System.Text.RegularExpressions;

namespace App.Console.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Returns the most frequent separation character in <paramref name="input"/> 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static char DetermineSeparationCharacter(this string input)
        => new char[] { ',', '\t', ';', ' ' }
            .OrderByDescending(d => Regex.Matches(input, Regex.Escape(d.ToString())).Count)
            .FirstOrDefault();
}
