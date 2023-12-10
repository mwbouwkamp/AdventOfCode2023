namespace AdventOfCode2023;

public class Day02 : Day
{
    private readonly int redCubes = 12;
    private readonly int greenCubes = 13;
    private readonly int blueCubes = 14;

    public Day02(string input) : base(input) { }

    public override string ExecuteA()
    {
        return new FileUtils(input).GetLines()
            .Select(line => new Game(line))
            .Where(game => !game.WorksNotForColor("red", redCubes) && !game.WorksNotForColor("green", greenCubes) && !game.WorksNotForColor("blue", blueCubes))
            .Select(game => int.Parse(game.Name))
            .Sum()
            .ToString();
    }

    public override string ExecuteB()
    {
        return new FileUtils(input).GetLines()
            .Select(line => new Game(line))
            .Select(game => game.GetPower())
            .Sum()
            .ToString();
    }
}
