using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Row
{
    public string Line { get; set; }
    public List<int> Sizes { get; set; }
    public int Hashes { get; set; }
    public int Dots { get; set; }

    public Row(string line, bool isB)
    {
        string[] lineParts = line.Split(' ');
        this.Line = lineParts[0];
        if (isB)
        {
            string toAddLine = new string(Line);
            for (int i = 0; i < 4; i++)
                Line = new string($"{Line}?{toAddLine}");
        }
        this.Sizes = lineParts[1].Split(",").Select(number => int.Parse(number)).ToList();
        List<int> toAddSizes = new(Sizes);
        if (isB)
        {
            for (int i = 0; i < 4; i++)
            {
                Sizes.AddRange(toAddSizes);
            }
        }
        Hashes = Sizes
            .Sum();
        Dots = Line.Length - Hashes;
    }

    public int CountPossibilities()
    {
        List<string> inProgress = new()
        {
            Line
        };
        List<string> processed = new();
        while (inProgress.Count > 0)
        {
            inProgress = inProgress
                .Distinct()
                .Where(line => line != "")
                .SelectMany(line =>
                {
                    int position = line.IndexOf('?');
                    if (position >= 0)
                    {
                        char[] charsHash = line.ToCharArray();
                        charsHash[position] = '#';
                        char[] charsDot = line.ToCharArray();
                        charsDot[position] = '.';
                        List<string> toReturn = new();
                        int numHashes = charsHash.Where(chr => chr == '#').ToList().Count;
                        int numDots = charsDot.Where(chr => chr == '.').ToList().Count;
                        string hashString = new(charsHash);
                        string dotsString = new(charsDot);
                        if (numHashes <= Hashes && CouldBeMatching(hashString))
                        {
                            toReturn.Add(hashString);
                        }
                        if (numDots <= Dots && CouldBeMatching(dotsString))
                        {
                            toReturn.Add(new string(charsDot));
                        }
                        return toReturn;
                    }
                    else
                    {
                        if (IsMatching(line))
                            processed.Add(line);
                        return new List<string>();
                    }
                })
                .ToList();
        }

        //processed = processed
        //    .Where(line =>
        //    {
        //        return IsMatching(line);
        //    })
        //    .ToList();

        return processed.Count;
    }

    private bool IsMatching(string line)
    {
        List<int> sizes = Regex.Matches(line, "[#]+").Select(match => match.Value.Length).ToList();
        if (sizes.Count != Sizes.Count)
        {
            return false;
        }
        for (int i = 0; i < sizes.Count; i++)
        {
            if (sizes[i] != Sizes[i])
            {
                return false;
            }
        }
        return true;
    }
    private bool CouldBeMatching(string line)
    {
        bool ltr = true;
        string fromStart = line.Split('.')[0];
        if (fromStart.Length == 0)
        {
            ltr = true;
        } else
        {
            List<int> sizesHashOnly = Regex.Matches(fromStart, "[#]+").Select(match => match.Value.Length).ToList();
            //List<int> sizesHashAndUnknown = Regex.Matches(fromStart, "[#?]+").Select(match => match.Value.Length).ToList();
            if (sizesHashOnly.Count > Sizes.Count)
            {
                ltr = false;
            }
            if (sizesHashOnly.Max() > Sizes.Max())
            {
                ltr = false;
            }
            for (int i = 0; i < sizesHashOnly.Count; i++)
            {
                if (i >= Sizes.Count || sizesHashOnly[i] > Sizes[i])
                {
                    ltr = false;
                }
            }
        }

        if (ltr == false)
        {
            return false;
        }

        //bool rtl = true;
        //char[] chars = line.ToCharArray();
        //chars = chars.Reverse().ToArray();
        //string reversed = new(chars);
        //string fromBack = reversed.Split('.')[0];
        //if (fromBack.Length == 0)
        //{
        //    rtl = true;
        //}
        //else
        //{
        //    List<int> sizesHashOnlyReversed = Regex.Matches(fromBack, "[#]+").Select(match => match.Value.Length).ToList();
        //    //List<int> sizesHashAndUnknown = Regex.Matches(fromStart, "[#?]+").Select(match => match.Value.Length).ToList();
        //    if (sizesHashOnlyReversed.Count > Sizes.Count)
        //    {
        //        rtl = false;
        //    }
        //    if (sizesHashOnlyReversed.Max() > Sizes.Max())
        //    {
        //        rtl = false;
        //    }
        //    for (int i = 0; i < sizesHashOnlyReversed.Count; i++)
        //    {
        //        if (i >= Sizes.Count || sizesHashOnlyReversed[i] > Sizes[Sizes.Count - i - 1])
        //        {
        //            rtl = false;
        //        }
        //    }

        //}
        //return rtl;
        return ltr;
    }
}
