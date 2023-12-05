using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day05 : Day
{

    public Day05(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();
        List<long> seeds = Regex.Matches(lines[0], "\\d+")
            .Select(match => long.Parse(match.Value))
            .ToList();

        lines.RemoveAt(0);
        lines.RemoveAt(0);

        List<Map> maps = GetMaps(lines);

        return Solve(seeds, maps);

    }

    private static string Solve(List<long> seeds, List<Map> maps)
    {
        return seeds
            .Select(seed =>
            {
                long start = seed;
                for (int i = 0; i < maps.Count; i++)
                {
                    start = maps[i].GetConverted(start);
                }
                return start;

            })
            .Min()
            .ToString();
    }

    private static List<Map> GetMaps(List<string> lines)
    {
        List<Map> maps = new();
        while (lines.Count > 0)
        {
            List<string> input = new();
            bool emptyLine = false;
            while (lines.Count > 0 && !emptyLine)
            {
                string line = lines[0];
                lines.RemoveAt(0);
                emptyLine = string.IsNullOrEmpty(line);
                if (!emptyLine)
                    input.Add(line);
            }
            maps.Add(new Map(input));
        }

        return maps;
    }

    public override string ExecuteB()
    {
        List<string> lines = new FileUtils(input).GetLines();
        List<long> seedRanges = Regex.Matches(lines[0], "\\d+")
            .Select(match => long.Parse(match.Value))
            .ToList();

        List<long> seeds = new();

        for (int pair = 0; pair < seedRanges.Count / 2; pair++)
        {
            for (long i = seedRanges[pair * 2]; i < seedRanges[pair * 2] + seedRanges[pair * 2 + 1]; i++)
            {
                seeds.Add(i);
            }
        }

        lines.RemoveAt(0);
        lines.RemoveAt(0);

        List<Map> maps = GetMaps(lines);

        return Solve(seeds.Distinct().ToList(), maps);
    }
}
