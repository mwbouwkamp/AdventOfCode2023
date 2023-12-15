using System.Text;

namespace AdventOfCode2023;

public class Day13 : Day
{
    public Day13(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> inputLines = new FileUtils(input).GetLines();
        List<List<string>> grids = new();
        List<string> collector = new();
        while (inputLines.Count > 0)
        {
            string current = inputLines[0];
            inputLines.RemoveAt(0);
            if (current.Length > 0)
            {
                collector.Add(current);
            } else
            {
                grids.Add(collector);
                collector = new();
            }
        }
        grids.Add(collector);

        // low: 23105
        return grids.Select(grid => SolveInput(grid)).Sum().ToString();
    }

    private int SolveInput(List<string> lines)
    {
        List<string> vertical = lines;
        List<string> horizontal = RotateGrid(vertical);

        int verticalSolution = Solve(vertical);

        int test = Solve(horizontal);


        return verticalSolution != -1
            ? verticalSolution
            : 100* Solve(horizontal);
    }

    private static int Solve(List<string> vertical)
    {
        List<List<int>> mirrors = vertical
            .Select(line =>
            {
                List<int> mirrors = new();
                for (int i = 1; i < line.Length / 2; i++)
                {
                    string left = line[..i];
                    string right = line.Substring(i, left.Length);
                    char[] rightChars = right.ToCharArray();
                    rightChars = rightChars.Reverse().ToArray();
                    right = new string(rightChars);
                    if (right == left)
                    {
                        mirrors.Add(i);
                    }
                }
                for (int i = line.Length / 2 + 1; i < line.Length - 1; i++)
                {
                    string right = line[i..];
                    string left = line.Substring(i - right.Length, right.Length);
                    char[] leftChars = left.ToCharArray();
                    leftChars = leftChars.Reverse().ToArray();
                    left = new string(leftChars);
                    if (right == left)
                    {
                        mirrors.Add(i);
                    }
                }
                return mirrors;
            })
            .ToList();

        List<int> candidates = mirrors[0];
        for (int i = 0; i < mirrors.Count; i++)
        {
            candidates = candidates
                .Where(candidate => mirrors[i].Any(x => x == candidate))
                .ToList();
        }
        return candidates.Count == 1 ? candidates[0] : -1;
    }

    private static List<string> RotateGrid(List<string> lines)
    {
        List<List<char>> chars = lines
            .Select(line => line.ToCharArray().ToList())
            .ToList();
        RectangleGrid<char> inputGrid = new(chars, '.');
        List<string> newRows = new();
        for (int col = 0; col < inputGrid.Width; col++)
        {
            StringBuilder builder = new();
            for (int row = 0; row < inputGrid.Height; row++)
            {
                builder.Append(inputGrid.GetElement(row, col));
            }
            newRows.Add(builder.ToString());
        }
        return newRows;
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
