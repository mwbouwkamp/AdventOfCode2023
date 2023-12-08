using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day08 : Day
{
    public Day08(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();
        List<char> instructions = lines[0].ToCharArray().ToList();
        lines.RemoveRange(0, 2);
        Dictionary<string, (string, string)> directions = lines
            .Select(line => Regex.Matches(line, "[A-Z]{3}").ToList())
            .ToDictionary(matches => matches[0].Value, matches => (matches[1].Value, matches[2].Value));

        int index = 0;
        bool found = false;
        int steps = 0;
        string current = "AAA";
        string target = "ZZZ";

        while (!found)
        {
            current = instructions[index] == 'L' 
                ? directions[current].Item1
                : directions[current].Item2;
            steps++;
            index++;
            index %= instructions.Count;
            found = current == target;
        }

        return steps.ToString();
    }

    public override string ExecuteB()
    {
        List<string> lines = new FileUtils(input).GetLines();
        List<char> instructions = lines[0].ToCharArray().ToList();
        lines.RemoveRange(0, 2);
        Dictionary<string, (string, string)> directions = lines
            .Select(line => Regex.Matches(line, "[A-Z0-9]{3}").ToList())
            .ToDictionary(matches => matches[0].Value, matches => (matches[1].Value, matches[2].Value));

        List<int> currentList = directions.Keys
            .Where(key => key.EndsWith('A'))
            .Select(current => SolveSingle(current, instructions, directions))
            .ToList();

        HashSet<long> sets = currentList
            .SelectMany(steps => GetParts(steps))
            .ToHashSet();

        long result = 1;
        foreach (long number in sets)
        {
            result *= number;
        }
        return result.ToString();
    }

    private static int SolveSingle(string starting, List<char> instructions, Dictionary<string, (string, string)> directions)
    {
        int index = 0;
        bool found = false;
        int steps = 0;
        string current = starting;

        while (!found)
        {
            current = instructions[index] == 'L'
                ? directions[current].Item1
                : directions[current].Item2;
            steps++;
            index++;
            index %= instructions.Count;
            found = current.EndsWith('Z');
        }
        return steps;
    }

    private static HashSet<long> GetParts(int source)
    {
        HashSet<long> parts = new();
        int current = source;
        for (int i = 2; i < source - 1 / 2; i++)
        {
            if ((current / i) * i == current)
            {
                parts.Add(i);
                parts.Add(current / i);
            }
        }
        if (parts.Count == 0)
            parts.Add(source);
        return parts;
    }
}
