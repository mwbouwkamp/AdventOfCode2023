namespace AdventOfCode2023;

public class Day10 : Day
{
    public Day10(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<List<char>> inputChars = new FileUtils(input).GetLines()
            .Select(line => line.ToCharArray().ToList()).ToList();

        Position start = FindStart(new FileUtils(input).GetLines());
        PipeSystem pipeSystem = new PipeSystem(inputChars, start);

        return (pipeSystem.Segments.Count / 2).ToString();
    }

    public override string ExecuteB()
    {
        List<List<char>> inputChars = new FileUtils(input).GetLines()
            .Select(line => line.ToCharArray().ToList()).ToList();

        Position start = FindStart(new FileUtils(input).GetLines());
        PipeSystem pipeSystem = new PipeSystem(inputChars, start);

        Dictionary<int, List<int>> horizontalDictionary = new();
        Dictionary<int, List<int>> verticalDictionary = new();

        pipeSystem.Segments.ForEach(segment =>
        {
            int row = segment.Row;
            int col = segment.Col;

            horizontalDictionary.TryGetValue(row, out List<int> horizontal);
            if (horizontal != null)
            {
                horizontal.Add(col);
            }
            else
            {
                horizontalDictionary.Add(row, new List<int>() { col });
            }

            verticalDictionary.TryGetValue(col, out List<int> vertical);
            if (vertical != null)
            {
                vertical.Add(row);
            }
            else
            {
                verticalDictionary.Add(col, new List<int>() { row });
            }
        });

        List<Position> solvable = new();
        List<Position> insolvable = new();
        for (int row = 0; row < pipeSystem.Height; row++)
        {
            if (pipeSystem.GetElement(row, 0) == '.')
                solvable.Add(new(row, 0));
            if (pipeSystem.GetElement(row, pipeSystem.Height - 1) == '.')
                solvable.Add(new(row, pipeSystem.Height - 1));
        }
        for (int col = 0; col < pipeSystem.Width; col++)
        {
            if (pipeSystem.GetElement(0, col) == '.')
                solvable.Add(new(0, col));
            if (pipeSystem.GetElement(pipeSystem.Width - 1, col) == '.')
                solvable.Add(new(pipeSystem.Width - 1, col));
        }

        for (int row = 1; row < pipeSystem.Height - 1; row++)
        {
            for (int col = 1; col < pipeSystem.Width - 1; col++)
            {
                if (pipeSystem.Segments.Any(segment => segment.Row == row && segment.Col == col))
                {
                    continue;
                }
                horizontalDictionary.TryGetValue(row, out List<int> horizontal);
                int numEast = horizontal == null
                    ? 0
                    : horizontal
                        .Where(element => element < col)
                        .Count();
                int numWest = horizontal == null
                    ? 0
                    : horizontal
                        .Where(element => element > col)
                        .Count();

                verticalDictionary.TryGetValue(col, out List<int> vertical);
                int numNorth = vertical == null
                    ? 0
                    : vertical
                        .Where(element => element < row)
                        .Count();
                int numSouth = vertical == null
                    ? 0
                    : vertical
                        .Where(element => element > row)
                        .Count();
                int barriers = 0;
                if (numEast % 2 == 1)
                    barriers++;
                if (numWest % 2 == 1)
                    barriers++;
                if (numNorth % 2 == 1)
                    barriers++;
                if (numSouth % 2 == 1)
                    barriers++;

                if (barriers ==4)
                {
                    insolvable.Add(new Position(row, col));
                }
                else
                {
                    solvable.Add(new Position(row, col));
                }

            }
        }
        return insolvable.Count.ToString(); 

    }

    public static Position FindStart(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            int position = lines[i].IndexOf('S');
            if (position >= 0)
            {
                return new(i, position);
            }
        }
        return new(-1, -1);
    }
}
