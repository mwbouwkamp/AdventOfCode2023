namespace AdventOfCode2023;

public class Day23 : Day
{
    public Day23(string input) : base(input) { }

    public override string ExecuteA()
    {
        return solve('a');
    }

    private string solve(char dayPart)
    {
        Day23State startingState = GetStartingState();
        if (dayPart == 'a')
            startingState.SetChildrenA();
        else
            startingState.SetChildrenB();

        List<Day23State> fringe = new() { startingState };
        Day23State longest = startingState;
        while (fringe.Count > 0)
        {
            //Day23State current = fringe.Last();
            Day23State current = GetWithHeighestHeuristic(fringe);
            fringe.Remove(current);
            if (current.Position.row == current.Height - 1)
            {
                if (current.Path.Count > longest.Path.Count)
                {
                    longest = current;
                    Console.WriteLine(longest.Path.Count);
                    return (longest.Path.Count - 1).ToString();
                }
            }
            else
            {
                fringe.AddRange(current.GetChildren());
            }
        }
        return (longest.Path.Count - 1).ToString();
    }

    private Day23State GetWithHeighestHeuristic(List<Day23State> states) 
    {
        return states
            .Aggregate((a, b) => a.Heuristic > b.Heuristic ? a : b);
    }
    private Day23State GetStartingState()
    {
        List<string> lines = new FileUtils(input).GetLines();

        RectangleGrid<char> inputGrid = new(lines.Select(line => line.ToCharArray().ToList()).ToList(), '.');
        (int row, int col) start = (0, lines[0].IndexOf('.'));
        Day23State startingState = new(start);
        for (int row = 0; row < inputGrid.Height; row++)
        {
            for (int col = 0; col < inputGrid.Width; col++)
            {
                startingState.SetElement(row, col, inputGrid.GetElement(row, col));
            }
        }

        return startingState;
    }

    public override string ExecuteB()
    {
        return solve('b');
    }
}
