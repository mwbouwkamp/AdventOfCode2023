namespace AdventOfCode2023;

public class Day12 : Day
{
    public Day12(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<Row> rows = new FileUtils(input)
            .GetLines()
            .Select(line => new Row(line, false))
            .ToList();

        return rows
            .Select(row => row.CountPossibilities())
            .Sum()
            .ToString();        
    }

    public override string ExecuteB()
    {
        List<Row> rows = new FileUtils(input)
            .GetLines()
            .Select(line => new Row(line, true))
            .ToList();

        return rows
            .Select(row => row.CountPossibilities())
            .Sum()
            .ToString();
    }
}
