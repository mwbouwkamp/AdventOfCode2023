using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023;

public class Day17 : Day
{
    public Day17(string input) : base(input) { }

    public override string ExecuteA()
    {
        RectangleGrid<int> grid = new FileUtils(input).GetIntGrid();
        Day17State first = new Day17State(grid, new(0, 0), 1, 0, 0, new());
        Dictionary<Day17State, int> fringe = new()
        {
            { first, first.Heuristic }
        };
        Dictionary<Position, int> lowestSeen = new();
        Position target = new(first.Grid.Height - 1, first.Grid.Width - 1);
        while (fringe.Count > 0)
        {
            int lowestHeuristic = fringe.Values.Min();
            Day17State current = fringe.Keys
                .First(key => fringe[key] == lowestHeuristic);
            fringe.Remove(current);

            Console.WriteLine($"{current.HeatLoss} - {current.Position.row},{current.Position.col}");

            lowestSeen.TryAdd(current.Position, current.Heuristic);
            if (lowestSeen.ContainsKey(current.Position))
            {
                if (lowestSeen[current.Position] > current.HeatLoss)
                {
                    lowestSeen[current.Position] = current.HeatLoss;
                } else
                {
                    continue;
                }
            }
            else
            {
                lowestSeen.Add(current.Position, current.HeatLoss);
            }


            if (current.Position.Equals(target))
            {
                current.PrintPaths();
                return (current.HeatLoss + current.Grid.GetElement(current.Grid.Height - 1, current.Grid.Width - 1)).ToString();
            }
            List<Day17State> children = current.GetChildren();
            children
                .ForEach(child =>
                {
                    if (!lowestSeen.ContainsKey(child.Position))
                    {
                        fringe.Add(child, child.Heuristic);
                    }
                    else if (child.HeatLoss < lowestSeen[child.Position])
                    {
                        fringe.Add(child, child.Heuristic);
                    }
                });
        }
        //729 low  not 737, 738
        return "Error";
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
