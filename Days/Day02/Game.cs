namespace AdventOfCode2023;

public class Game
{
    public string Name { get; }
    private List<(string color, int amount)> CubeGroups { get; }
    public Game(string input)
    {
        //Game 1: 1 red, 10 blue, 5 green; 11 blue, 6 green; 6 green; 1 green, 1 red, 12 blue; 3 blue; 3 blue, 4 green, 1 red
        string[] gameParts = input.Split(": ");
        Name = gameParts[0].Replace("Game ", "");

        CubeGroups = gameParts[1]
            .Replace(";", ",")
            .Split(", ")
            .Select(group => group.Split(" "))
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
        int red = CubeGroups
            .Where(cubeGroup => cubeGroup.color == "red")
            .Select(cubeGroup => cubeGroup.amount)
            .Max();

        int green = CubeGroups
            .Where(cubeGroup => cubeGroup.color == "green")
            .Select(cubeGroup => cubeGroup.amount)
            .Max();

        int blue = CubeGroups
            .Where(cubeGroup => cubeGroup.color == "blue")
            .Select(cubeGroup => cubeGroup.amount)
            .Max();

        return red * green * blue;
    }
}
