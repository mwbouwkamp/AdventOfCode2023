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

    public static int GetScore(string input)
    {
        string sortedCards = new(input.OrderBy(c => c).ToArray());
        
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1\\1\\1"))
            return  6;
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1\\1"))
            return 5;
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1([A-Z0-9])\\2") || Regex.IsMatch(sortedCards, "([A-Z0-9])\\1([A-Z0-9])\\2\\2"))
            return 4;
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1\\1"))
            return 3;
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1.*([A-Z0-9])\\2"))
            return 2;
        if (Regex.IsMatch(sortedCards, "([A-Z0-9])\\1"))
            return 1;
        else
            return 0;
    }

    public int CompareTo(object? obj)
    {
        Hand comparingHand = obj as Hand 
            ?? throw new ArgumentException("Object is not of type Hand", nameof(obj));

        if (this.Score != comparingHand.Score)
            return this.Score.CompareTo(comparingHand.Score);

        for (int i = 0; i < 5; i++)
            if (this.CardValues[i] != comparingHand.CardValues[i])
                return this.CardValues[i].CompareTo(comparingHand.CardValues[i]);

        return 0;
    }
}
