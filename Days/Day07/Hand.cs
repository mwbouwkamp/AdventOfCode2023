using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Hand : IComparable
{
    public string Cards { get; set; }
    public int Bid { get; set; }
    public List<int> CardValues { get; set; }
    public int Score { get; set; }

    public Hand(string input)
    {
        string[] inputParts = input.Split(' ');
        Cards = inputParts[0];
        Bid = int.Parse(inputParts[1]);
        CardValues = Cards.ToCharArray()
            .Select(card => char.IsLetter(card)
            ? card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => throw new ArgumentException($"Unknown card: {card}")
            }
            : int.Parse(card.ToString()))
            .ToList();

        Score = GetScore(Cards);

    }

    public void ApplyJokers()
    {
        CardValues = CardValues
            .Select(cardValue => cardValue == 11 ? -1 : cardValue)
            .ToList();

        if (Cards == "JJJJJ")
            return;

        Score = Cards.Replace("J", "")
            .ToCharArray()
            .Select(chr => Cards.Replace('J', chr))
            .Select(chr => GetScore(chr.ToString()))
            .Max();
    }
    public int GetScore(string input)
    {
        List<char> cards = input.ToCharArray().ToList();
        cards.Sort();
        string sortedCards = new(cards.ToArray());
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1\\1\\1"))
            return  6;
        else if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1\\1"))
            return 5;
        else if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1([A-Z0-9])\\2") || Regex.IsMatch(sortedCards, "([A-Z0-9])\\1([A-Z0-9])\\2\\2"))
            return 4;
        else if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1"))
            return 3;
        else if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1.*([A-Z0-9])\\2"))
            return 2;
        else if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1"))
            return 1;
        else
            return 0;
    }

    public int CompareTo(object? obj)
    {
        Hand comparingHand = obj as Hand;
        if (this.Score != comparingHand.Score)
            return this.Score.CompareTo(comparingHand.Score);
        if (this.CardValues[0] != comparingHand.CardValues[0])
            return this.CardValues[0].CompareTo(comparingHand.CardValues[0]);
        if (this.CardValues[1] != comparingHand.CardValues[1])
            return this.CardValues[1].CompareTo(comparingHand.CardValues[1]);
        if (this.CardValues[2] != comparingHand.CardValues[2])
            return this.CardValues[2].CompareTo(comparingHand.CardValues[2]);
        if (this.CardValues[3] != comparingHand.CardValues[3])
            return this.CardValues[3].CompareTo(comparingHand.CardValues[3]);
        return this.CardValues[4].CompareTo(comparingHand.CardValues[4]);
    }
}
