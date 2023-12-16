using System.Text;

namespace AdventOfCode2023;

public class Day13 : Day
{
    public Day13(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<List<string>> grids = CreateGrids();

        return grids.Select(grid => SolveInput(grid)).Sum().ToString();
    }

    private List<List<string>> CreateGrids()
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
            }
            else
            {
                grids.Add(collector);
                collector = new();
            }
        }
        grids.Add(collector);
        return grids;
    }

    private static int SolveInput(List<string> lines, int smudgeRow = -1, int smudgeCol = -1, int noSmudgeSolution = -1)
    {
        if (smudgeCol >= 0 && smudgeRow >= 0)
        {
            List<string> newLines = new(lines);
            char current = newLines[smudgeRow][smudgeCol];
            string newLine = newLines[smudgeRow][..smudgeCol] + (current == '.' ? '#' : '.') + newLines[smudgeRow][(smudgeCol + 1)..];
            newLines[smudgeRow] = newLine;

            List<string> verticalSmudge = newLines;
            List<string> horizontalSmudge = RotateGrid(verticalSmudge);

            int verticalSolutionSmudge = Solve(verticalSmudge);
            int horizontalSolutionSmudge = Solve(horizontalSmudge);

            return verticalSolutionSmudge != -1 && verticalSolutionSmudge != noSmudgeSolution
                ? verticalSolutionSmudge
                : horizontalSolutionSmudge != -1 && 100 * horizontalSolutionSmudge != noSmudgeSolution
                ? 100 * horizontalSolutionSmudge
                : -1;
        }

        List<string> verticalNoSmudge = lines;
        List<string> horizontalNoSmudge = RotateGrid(verticalNoSmudge);

        int verticalSolutionNoSmudge = Solve(verticalNoSmudge);
        int horizontalSolutionNoSmudge = Solve(horizontalNoSmudge);

        return verticalSolutionNoSmudge != -1
            ? verticalSolutionNoSmudge
            : horizontalSolutionNoSmudge != -1
            ? 100 * horizontalSolutionNoSmudge
            : -1;
    }

    private static int Solve(List<string> vertical)
    {
        List<List<int>> mirrors = vertical
            .Select(line => GetPotentialMirrors(line))
            .ToList();

        List<int> candidates = mirrors[0];
        for (int i = 0; i < mirrors.Count; i++)
        {
            candidates = candidates
                .Where(candidate => mirrors[i].Any(x => x == candidate))
                .ToList();
        }
        return candidates.Count == 1 ? candidates.Min() : -1;
    }

    public static List<int> GetPotentialMirrors(string line)
    {
        List<int> mirrors = new();
        for (int i = 1; i < line.Length / 2 + 1; i++)
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
        for (int i = line.Length / 2 + 1; i < line.Length; i++)
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
        List<List<string>> grids = CreateGrids();

        return grids
            .Select(grid => SolveBForGrid(grid))
            .Sum()
            .ToString();
    }

    private static int SolveBForGrid(List<string> grid)
    {
        int noSmudgeSolution = SolveInput(grid);
        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[row].Length; col++)
            {
                int result = SolveInput(grid, row, col, noSmudgeSolution);
                if (result > 0)
                {
                    Console.WriteLine(result);
                    return result;
                }
            }
        }
        Console.WriteLine("!!!!");
        return -1;

        //low: 15938
    }
}
