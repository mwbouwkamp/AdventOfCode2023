namespace AdventOfCode2023;

public class Day07 : Day
{

    public Day07(string inptut) : base(inptut) { }
    public override string ExecuteA()
    {
        List<Hand> hands = new FileUtils(input).GetLines()
            .Select(line => new Hand(line))
            .ToList();

        hands.Sort();
        long result = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            result += (i + 1) * hands[i].Bid;
        }
        return result.ToString();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
