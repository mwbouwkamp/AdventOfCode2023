namespace AdventOfCode2023;

public abstract class Station
{
    public string Name { get; set; }
    public List<string> Destinations { get; set; }

    public Station(string line)
    {
        Name = line.Split(" -> ")[0];
        Destinations = line
            .Split(" -> ")[1]
            .Split(", ")
            .ToList();
    }

    public abstract (List<string> destinations, bool signal) Execute(Boolean signal, string parent);
}

public class BroadcasterStation : Station
{
    public BroadcasterStation(string line) : base(line)
    {
    }

    public override (List<string> destinations, bool signal) Execute(bool signal, string parent)
    {
        return (Destinations, signal);
    }
}

public class FlipFlopStation : Station
{
    private bool state = false;
    public FlipFlopStation(string line) : base(line)
    {
    }

    public override (List<string> destinations, bool signal) Execute(bool signal, string parent)
    {
        if (signal)
            return (new List<string>(), false);

        state = !state;
        return (Destinations, state);
    }
}

public class ConjunctionStation : Station
{
    Dictionary<string, bool> PreviousSignals { get; set; }

    public ConjunctionStation(string line) : base(line)
    {
        PreviousSignals = new();
    }

    public void InitiatePreviousSignals(List<string> sources)
    {
        PreviousSignals = sources.ToDictionary(source => source, _source => false);
    }

    public override (List<string> destinations, bool signal) Execute(bool signal, string parent)
    {
        PreviousSignals[parent] = signal;
        if (PreviousSignals.Values.Any(signal => signal == false))
            return (Destinations, true);

        return (Destinations, false);
    }
}