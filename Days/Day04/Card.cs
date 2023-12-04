using System.Text.RegularExpressions;

namespace AdventOfCode2023;

internal class Card
{
    public int Id { get; }
    private readonly List<int> winning;
    private readonly List<int> mine;
    public int Amount { get; set; }

    public Card(string input)
    {
        Id = int.Parse(Regex.Matches(input, "\\d+").First().ToString());
        string[] parts = input.Split(new char[] { ':', '|' });
        List<string> test = parts[1].Split(" ").ToList();
        winning = Regex.Matches(parts[1], "\\d+")
            .Select(number => int.Parse(number.Value))
            .ToList();
        mine = Regex.Matches(parts[2], "\\d+")
            .Select(number => int.Parse(number.Value))
            .ToList();
        Amount = 1;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public int GetWins()
    {
        return winning.Intersect(mine).Count();
    }

    public int GetPower()
    {
        return (int)Math.Pow(2, winning.Intersect(mine).Count() - 1);
    }
}
