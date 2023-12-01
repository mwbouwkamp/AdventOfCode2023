namespace AdventOfCode2023;

public class StringUtils
{
    public static string GetReversedString(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}
