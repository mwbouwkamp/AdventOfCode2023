using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Row
{
    public string Line { get; set; }
    public List<int> Sizes { get; set; }

    public Row(string line, bool isB)
    {
        string[] lineParts = line.Split(' ');
        this.Line = lineParts[0];
        if (isB)
        {
            string toAddLine = new string(Line);
            for (int i = 0; i < 4; i++)
                Line = new string($"{Line}?{toAddLine}");
        }
        this.Sizes = lineParts[1].Split(",").Select(number => int.Parse(number)).ToList();
        List<int> toAddSizes = new(Sizes);
        if (isB)
        {
            for (int i = 0; i < 4; i++)
            {
                Sizes.AddRange(toAddSizes);
            }
        }
        //CountPossibilities();
    }

    public int CountPossibilities()
    {
        List<string> inProgress = new()
        {
            Line
        };
        List<string> processed = new();
        while (inProgress.Count > 0)
        {
            inProgress = inProgress
                .Distinct()
                .Where(line => line != "")
                .SelectMany(line =>
                {
                    int position = line.IndexOf('?');
                    if (position >= 0)
                    {
                        char[] charsHash = line.ToCharArray();
                        charsHash[position] = '#';
                        char[] charsDot = line.ToCharArray();
                        charsDot[position] = '.';
                        return new List<string>() { new string(charsDot), new string(charsHash) };
                    }
                    else
                    {
                        processed.Add(line);
                        return new List<string>();
                    }
                })
                .ToList();
        }

        processed = processed
            .Where(line =>
            {
                List<int> sizes = Regex.Matches(line, "[#]+").Select(match => match.Value.Length).ToList();
                if (sizes.Count != Sizes.Count)
                {
                    return false;
                }
                for (int i = 0; i < sizes.Count; i++)
                {
                    if (sizes[i] != Sizes[i])
                    {
                        return false;
                    }
                }
                return true;
            })
            .ToList();

        return processed.Count;
    }
}
