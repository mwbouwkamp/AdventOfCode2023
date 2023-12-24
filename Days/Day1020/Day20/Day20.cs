namespace AdventOfCode2023;

public class Day20 : Day
{
    public Day20(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();
        List<Station> stations = lines
            .Select<string, Station>(line =>
            {
                if (line.StartsWith('%'))
                    return new FlipFlopStation(line[1..]);
                if (line.StartsWith('&'))
                    return new ConjunctionStation(line[1..]);
                return new BroadcasterStation(line[1..]);
            })
            .ToList();
        stations
            .Where(station => station is ConjunctionStation)
            .Select(station => (ConjunctionStation) station)
            .ToList()
            .ForEach(station => station.InitiatePreviousSignals(GetParentStationNames(station, stations)));

        Dictionary<string, Station> stationsDictionary = stations
            .ToDictionary(station => station.Name, station => station);

        Station broadcaster = stations.First(station => station is BroadcasterStation);
        long high = 0;
        long low = 0;

        for (int i = 0; i < 1000; i++)
        {
            Console.WriteLine("-----");
            List<(Station station, bool signal)> toDo = new() { (broadcaster, false) };
            while (toDo.Count > 0)
            {
                (Station station, bool signal) current = toDo.First();
                toDo.Remove(current);
                (List<string> destinations, bool signal) result = current.station.Execute(current.signal, current.station.Name); // THIS GEOES WRONG... THE CALLING SHOULD BE THE INPUT HERE, NOT ITSELF
                result.destinations.ForEach(destination => Console.WriteLine($"{current.station.Name} -{result.signal} -> {destination}"));
                if (result.signal)
                    high++;
                else
                    low++;
                List<(Station station, bool signal)> toAdd = result.destinations
                        .Select(destination =>
                        {
                            stationsDictionary.TryGetValue(destination, out Station value);
                            return (value, result.signal);
                        })
                        .Where(tuple => tuple.value != null)
                        .ToList();
                //toAdd.Reverse();
                toDo
                    .AddRange(toAdd);
            }
        }

        return (low * high).ToString();

    }

    public static List<string> GetParentStationNames(Station targetStation, List<Station> stations)
    {
        return stations
            .Where(station => station.Destinations.Contains(targetStation.Name))
            .Select(station => station.Name)
            .ToList();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
