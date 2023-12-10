namespace AdventOfCode2023;

public class Day10 : Day
{
    public Day10(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<List<char>> inputChars = new FileUtils(input).GetLines()
            .Select(line => line.ToCharArray().ToList()).ToList();

        (int row, int col) start = FindStart(new FileUtils(input).GetLines());
        PipeSystem pipeSystem = new PipeSystem(inputChars, start);

        return (pipeSystem.Segments.Count / 2).ToString();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }

    public static (int row, int col) FindStart(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            int position = lines[i].IndexOf('S');
            if (position >= 0)
            {
                return (i, position);
            }
        }
        return (-1, -1);
    }
}
