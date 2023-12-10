namespace AdventOfCode2023;

public  class Day04 : Day
{
    public Day04(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<Card> cards = new FileUtils(input).GetLines()
            .Select(line => new Card(line))
            .ToList();

        return cards
            .Select(card => card.GetPower())
            .Sum()
            .ToString();
    }

    public override string ExecuteB()
    {
        List<Card> cards = new FileUtils(input).GetLines()
            .Select(line => new Card(line))
            .ToList();

        for (int i = 0; i < cards.Count; i++)
        {
            int wins = cards[i].GetWins();
            for (int j = 1; j < wins + 1; j++)
            {
                if (i + j < cards.Count)
                    cards[i + j].AddAmount(cards[i].Amount);
            }
        }

        return cards
            .Select(card => card.Amount)
            .Sum()
            .ToString();
    }
}

