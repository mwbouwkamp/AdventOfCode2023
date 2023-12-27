using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023;

public class Day17 : Day
{
    public Day17(string input) : base(input) { }

    public override string ExecuteA()
    {
        RectangleGrid<int> grid = new FileUtils(input).GetIntGrid();
        Day17State first = new Day17State(grid, (0, 0), 1, 0, 0);
        Dictionary<Day17State, int> fringe = new()
        {
            { first, first.Heuristic }
        };
        Dictionary<Point, int> visited = new(new PositionComparer());
        Point target = (first.Grid.Height - 1, first.Grid.Width - 1);
        while (fringe.Count > 0)
        {
            int lowestHeuristic = fringe.Values.Min();
            Day17State current = fringe.Keys
                .First(key => fringe[key] == lowestHeuristic);
            fringe.Remove(current);
            if (visited.ContainsKey(current.Position))
            {
                if (visited[current.Position] > current.HeatLoss)
                {
                    visited[current.Position] = current.HeatLoss;
                }
            }
            else
            {
                visited.Add(current.Position, current.HeatLoss);
            }
            if (current.Position == target)
                return (current.HeatLoss + current.Grid.GetElement(current.Grid.Height - 1, current.Grid.Width - 1)).ToString();

            current.GetChildren()
                .Where(child => !visited.ContainsKey(child.Position) || child.HeatLoss < visited[child.Position])
                .ToList()
                .ForEach(child => fringe.Add(child, child.Heuristic));
        }
        return "Error";
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }

    public class PositionComparer : IEqualityComparer<Point> 
    {
        public bool Equals(Point x, Point y)
        {
            return x.col == y.col && x.row == y.row;
        }

        public int GetHashCode([DisallowNull] Point obj)
        {
            return $"{obj.row},{obj.col}".GetHashCode();
        }
    }
}
