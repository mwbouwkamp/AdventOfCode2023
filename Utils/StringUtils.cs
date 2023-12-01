namespace AdventOfCode2023;

public class StringUtils
{
    public static string GetReversedString(string input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input), "Input cannot be null");

        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}
