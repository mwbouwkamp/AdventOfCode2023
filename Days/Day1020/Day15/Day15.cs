using System.Text;

namespace AdventOfCode2023;

public class Day15 : Day
{
    public Day15(string input) : base(input) { }

    public override string ExecuteA()
    {
        string[] steps = new FileUtils(input).GetString().Split(',');
        return steps
            .Select(step => PerformStep(step))
            .Sum()
            .ToString();
    }

    private static int PerformStep(string input)
    {
        int FACTOR = 17;
        int MOD = 256;
        byte[] asciiBytes = Encoding.ASCII.GetBytes(input);
        return asciiBytes.Aggregate(0, (a, b) => (a + b) * FACTOR % MOD);
    }

    public override string ExecuteB()
    {
        List<string> steps = new FileUtils(input).GetString().Split(',').ToList();
        Dictionary<int, List<string>> boxes = new();
        steps
            .ForEach(step =>
            {
                string label = step.EndsWith("-") ? step.Split('-')[0] : step.Split('=')[0];
                int boxNumber = PerformStep(label);
                boxes.TryGetValue(boxNumber, out List<string> lenses);
                if (step.Contains('='))
                {
                    if (lenses != null)
                    {
                        int firstIndex = lenses.FindIndex(lens => lens.StartsWith(step.Split('=')[0]));
                        if (firstIndex == -1)
                        {
                            lenses.Add(step);
                        }
                        else
                        {
                            lenses[firstIndex] = step;
                        }
                    } else
                    {
                        lenses = new() { step };
                        boxes.Add(boxNumber, lenses);
                    }
                }
                else
                {
                    if (lenses != null) 
                    {
                        int firstIndex = lenses.FindIndex(lens => lens.StartsWith(step.Split('-')[0]));
                        if (firstIndex != -1)
                        lenses.RemoveAt(firstIndex);
                    }
                }
            });

        return boxes.Keys
            .ToDictionary(key => key, key => boxes[key].Count > 0 
                ? boxes[key]
                    .Select((lens, index) => (key + 1) * int.Parse(lens.Split('=')[1]) * (index + 1))
                    .Sum()
                : 0)
            .Values
            .Sum()
            .ToString();
    }
}
