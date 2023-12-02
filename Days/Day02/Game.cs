using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Game
{
    public string Name { get; }
    private List<(string color, int amount)> CubeGroups { get; }
    public Game(string input)
    {
        string inputRegex = @"Game \d+: (\d+ (red|green|blue)([,;])\s*)+";

        if (!Regex.IsMatch(input, inputRegex))
            throw new ArgumentException($"Input is not of the correct type: {input}");

        Name = Regex.Matches(input, "Game \\d+")
            .First()
            .ToString()
            .Replace("Game ", "");

        CubeGroups = Regex.Matches(input, "\\d+ (red|green|blue)")
            .Select(group => group.ToString().Split(" "))
            .Select(group => (group[1], int.Parse(group[0])))
            .ToList();
    }

    public bool WorksNotForColor(string color, int amount)
    {
        return CubeGroups
            .Where(cubeGroup => cubeGroup.color == color)
            .Any(cubeGroup => cubeGroup.amount > amount);
    }

    public int GetPower()
    {
        return GetMaxForColor("red") * GetMaxForColor("green") * GetMaxForColor("blue");
    }

    private int GetMaxForColor(string color)
    {
        return CubeGroups
            .Where(cubeGroup => cubeGroup.color == color)
            .Select(cubeGroup => cubeGroup.amount)
            .Max();
    }
}
