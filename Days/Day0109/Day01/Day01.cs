using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day01 : Day
{
    private static readonly Dictionary<string, int> numbersDictionary = new()
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

    private static readonly Dictionary<string, int> reverseNumbersDictionary = new()
        {
            { "orez", 0 },
            { "eno", 1 },
            { "owt", 2 },
            { "eerht", 3 },
            { "ruof", 4 },
            { "evif", 5 },
            { "xis", 6 },
            { "neves", 7 },
            { "thgie", 8 },
            { "enin", 9 }
        };

    public Day01(string input) : base(input) { }

    public override string ExecuteA()
    {
        return new FileUtils(input).GetLines()
            .Select(line => Regex.Replace(line, "[a-z]", ""))
            .Select(line => line.ToArray())
            .Select(line => $"{line.First()}{line.Last()}")
            .Select(line => int.Parse(line))
            .Sum()
            .ToString();
    }

    public override string ExecuteB()
    {
        return new FileUtils(input).GetLines()
            .Select(line => $"{GetFirstNumber(line, false)}{GetFirstNumber(line, true)}")
            .Select(line => int.Parse(line))
            .Sum()
            .ToString();
    }

    public static string GetFirstNumber(string input, bool isReversed)
    {
        string preparedInput = isReversed 
            ? StringUtils.GetReversedString(input) 
            : input;

        while (preparedInput.Length > 0)
        {
            if (char.IsDigit(preparedInput[0]))
                return preparedInput[0].ToString();

            string? firstNumber = isReversed 
                ? ReturnValueIfInputStartsWithString(preparedInput, reverseNumbersDictionary)
                : ReturnValueIfInputStartsWithString(preparedInput, numbersDictionary);

            if (firstNumber != null)
                return firstNumber;

            preparedInput = preparedInput[1..];
        }
        throw new ArgumentException($"The input does not contain a number: {preparedInput}");
    }

    private static string? ReturnValueIfInputStartsWithString(string input, Dictionary<string, int> numbersDictionary)
    {
        string? key = numbersDictionary.Keys
            .FirstOrDefault(key => input.StartsWith(key));

        if (key == null)
            return null;

        return numbersDictionary[key].ToString();
    }
}
