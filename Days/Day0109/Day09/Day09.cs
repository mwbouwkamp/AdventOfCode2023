namespace AdventOfCode2023;

public class Day09 : Day
{
    public Day09(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<List<int>> seriesList = new FileUtils(input).GetLines()
            .Select(line => line.Split(" ").Select(chr => int.Parse(chr.ToString())).ToList())
            .ToList();

        return seriesList
            .Select(series => PredictNext(series))
            .Sum()
            .ToString();
    }

    private int PredictNext(List<int> series, bool isReversed = false)
    {
        if (series.All(x => x == 0))
            return 0;

        List<int> derived = new();
        for (int i = 0; i < series.Count - 1; i++)
            derived.Add(series[i + 1] - series[i]);

        return isReversed
            ? series[0] - PredictNext(derived, true)
            : series[^1] + PredictNext(derived);
    }

    public override string ExecuteB()
    {
        List<List<int>> seriesList = new FileUtils(input).GetLines()
            .Select(line => line.Split(" ").Select(chr => int.Parse(chr.ToString())).ToList())
            .ToList();

        return seriesList
            .Select(series => PredictNext(series, true))
            .Sum()
            .ToString();
    }
}
