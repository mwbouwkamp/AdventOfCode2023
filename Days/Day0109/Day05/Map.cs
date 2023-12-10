namespace AdventOfCode2023;

public class Map
{
    public string Description { get; set; }
    public List<(long dest, long source, long range)> lines;
    public List<(long start, long end, long delta)> ranges;

    public Map(List<string> input)
    {
        this.Description = input[0];
        input.RemoveAt(0);
        lines = input
            .Select(line => line.Split(" "))
            .Select(lineParts => (long.Parse(lineParts[0]), long.Parse(lineParts[1]), long.Parse(lineParts[2])))
            .ToList();
        ranges = lines
            .Select(line => (line.source, line.source + line.range - 1, line.dest - line.source))
            .ToList();
    }

    public long GetConverted(long value)
    {
        (long dest, long source, long range)? bucket = lines.FirstOrDefault(line => value >= line.source && value < line.source + line.range);
        return bucket != null
            ? value + bucket.Value.dest - bucket.Value.source
            : value;
    }

    public List<Day05.Range> ConvertRanges(Day05.Range rangeToConvert)
    {
        List<(long start, long end, long delta)> overlappingRanges = ranges
            .Where(range => range.start <= rangeToConvert.end && range.end >= rangeToConvert.start)
            .ToList();

        if (overlappingRanges.Count == 0)
            return new() { rangeToConvert };

        overlappingRanges.Sort((a, b) => a.start.CompareTo(b.start));

        List<(long start, long delta)> points = new()
        {
            (rangeToConvert.end, 0)
        };

        foreach ((long start, long end, long delta) in overlappingRanges)
        {
            if (start <= rangeToConvert.start && end >= rangeToConvert.end)
            {
                points.Add((rangeToConvert.start, delta));
            }
            else if (start > rangeToConvert.start && end < rangeToConvert.end)
            {
                points.Add((rangeToConvert.start, 0));
                points.Add((start, delta));
                points.Add((end + 1, 0));
            }
            else if (start <= rangeToConvert.start && end < rangeToConvert.end)
            {
                points.Add((rangeToConvert.start, delta));
                points.Add((end + 1, 0));
            }
            else
            {
                points.Add((rangeToConvert.start, 0));
                points.Add((start, delta));
            }
        }
        points.Sort((a, b) => a.start.CompareTo(b.start));

        List<Day05.Range> converted = new();
        for (int i = 0; i < points.Count - 1; i++)
        {
            if (points[i + 1].start - points[i].start > 1)
                converted.Add(new(points[i].start + points[i].delta, points[i + 1].start + points[i].delta));
        }

        return converted;
    }

}
