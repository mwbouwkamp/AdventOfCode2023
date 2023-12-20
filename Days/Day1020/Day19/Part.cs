using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Part
{
    public int XValue { get; set; }
    public int MValue { get; set; }
    public int AValue { get; set; }
    public int SValue { get; set; }

    public Part(string input)
    {
        XValue = int.Parse(Regex.Match(input, "(?<=(x=))\\d+").Value);
        MValue = int.Parse(Regex.Match(input, "(?<=(m=))\\d+").Value);
        AValue = int.Parse(Regex.Match(input, "(?<=(a=))\\d+").Value);
        SValue = int.Parse(Regex.Match(input, "(?<=(s=))\\d+").Value);
    }   

    public int Sum()
    {
        return XValue + MValue + AValue + SValue;
    }
}
