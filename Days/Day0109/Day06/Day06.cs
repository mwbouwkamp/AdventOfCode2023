using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day06 : Day
{
    public Day06(string input) : base(input) { }


    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();
        return Solve(lines);
    }

    private string Solve(List<string> lines)
    {
        List<long> times = Regex.Matches(lines[0], "\\d+").ToList().Select(match => long.Parse(match.Value)).ToList();
        List<long> distances = Regex.Matches(lines[1], "\\d+").ToList().Select(match => long.Parse(match.Value)).ToList();

        long result = 1;
        // pressingtime * time - pressingtime * pressingtime - distance = 0
        for (int i = 0; i < times.Count; i++)
        {
            (double, double) area = getABC((double)-1, (double)times[i], (double)-distances[i]);
            long max = (int)(Math.Floor(Math.Max(area.Item1, area.Item2)));
            long min = (int)(Math.Ceiling(Math.Min(area.Item1, area.Item2)));
            if (-max * max + max * times[i] == distances[i])
                max--;
            if (-min * min + min * times[i] == distances[i])
                min++;
            result *= (max - min + 1);
        }
        return result.ToString();
    }

    public override string ExecuteB()
    {
        // Low: 71503

        List<string> lines = new FileUtils("input06b.txt").GetLines();
        return Solve(lines);
    }

    private (double, double) getABC(double a, double b, double c)
    {
        return ((-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a), (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
    }
}
