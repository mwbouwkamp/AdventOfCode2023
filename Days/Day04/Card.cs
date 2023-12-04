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
        winning = GetNumbers(parts[1]);
        mine = GetNumbers(parts[2]);
        Amount = 1;
    }

    private static List<int> GetNumbers(string line) {
        return Regex.Matches(line, "\\d+")
            .Select(number => int.Parse(number.Value))
            .ToList();
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
