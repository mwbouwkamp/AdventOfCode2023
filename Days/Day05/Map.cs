namespace AdventOfCode2023;

public class Map
{
    public string Description { get; set; }
    public List<(long dest, long source, long range)> lines;
    //public Dictionary<long, long> mapper;

    public Map(List<string> input)
    {
        this.Description = input[0];
        input.RemoveAt(0);
        lines = input
            .Select(line => line.Split(" "))
            .Select(lineParts => (long.Parse(lineParts[0]), long.Parse(lineParts[1]), long.Parse(lineParts[2])))
            //.OrderBy( line => line.Item1)
            .ToList();

        //mapper = new();
        //lines.ForEach(line =>
        //{
        //    for (int i = 0; i < line.range; i++)
        //    {
        //        mapper.Add(line.source + i, line.dest + i);
        //    }
        //});
    }

    public long GetConverted(long value)
    {
        (long dest, long source, long range)? bucket = lines.FirstOrDefault(line => value >= line.source && value < line.source + line.range);
        return bucket != null
            ? value + bucket.Value.dest - bucket.Value.source
            : value;
    }

}
