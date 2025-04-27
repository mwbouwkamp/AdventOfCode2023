namespace AdventOfCode2023;

public class Day17State
{
    public RectangleGrid<int> Grid { get; set; }
    public Position Position { get; set; }
    public int Direction { get; set; } //trbl
    public int PreviousInDirection { get; set; }
    public int Heuristic { get; set; }
    public int HeatLoss { get; set; }
    private readonly int MaxInDirection = 3;
    List<Position> Path { get; set; }
    private readonly int Threashold = 1739;

    public Day17State(RectangleGrid<int> grid, Position position, int direction, int previousInDirection, int heatLoss, List<Position> path)
    {
        Grid = grid;
        Position = position;
        Direction = direction;
        PreviousInDirection = previousInDirection;
        HeatLoss = heatLoss;
        Heuristic = (grid.Width - position.col) + (grid.Height - position.row) + heatLoss;
        Path = new();
        Path.AddRange(path);
        Path.Add(position);
    }

    public List<Day17State> GetChildren()
    {
        List<Day17State> childStates = new();
        if (PreviousInDirection < MaxInDirection)
        {
            Position? straightAhead = GetRelative(Direction);
            if (straightAhead != null)
            {
                childStates.Add(new(Grid, straightAhead, Direction, PreviousInDirection + 1, HeatLoss + Grid.GetElement(Position.row, Position.col), new(Path)));
            }
        }

        int direction = (Direction + 3) % 4;

        Position? leftTurn = GetRelative(direction);
        if (leftTurn != null)
        {
            childStates.Add(new(Grid, leftTurn, direction, 0, HeatLoss + Grid.GetElement(Position.row, Position.col), new(Path)));
        }

        direction = (Direction + 1) % 4;
        Position? rightTurn = GetRelative(direction);
        if (rightTurn != null)
        {
            childStates.Add(new(Grid, rightTurn, direction, 0, HeatLoss + Grid.GetElement(Position.row, Position.col), new(Path)));
        }

        Console.WriteLine($"Current: {Position} ({Direction}). Children: {childStates.Aggregate("", (a, b) => $"{a} {b.Position} ({b.Direction})")}");
        if (childStates.Select(child => child.Position).Where(position => Math.Abs(position.row - Position.row) > 1 || Math.Abs(position.col - Position.col) > 1).Any())
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        return childStates
            .Where(child => child.Heuristic < Threashold)
            .Where(child => child.HasNoDoublesInPath())
            .ToList();
    }

    private bool HasNoDoublesInPath()
    {
        bool toReturn = Path
            .Where(position => position.Equals(Position)).ToList().Count < 2;
        return toReturn;
    }

    private Position? GetRelative(int direction)
    {
        switch (direction)
        {
            case 0:
                if (Position.row > 0) 
                    return new(Position.row - 1, Position.col);
                break;
            case 1:
                if (Position.col < Grid.Width - 1)
                    return new(Position.row, Position.col + 1);
                break;
            case 2:
                if (Position.row < Grid.Height - 1)
                    return new(Position.row + 1, Position.col);
                break;
            case 3:
                if (Position.col > 0)
                    return new(Position.row, Position.col - 1);
                break;
            default:
                throw new ArgumentException($"Illegal direction: {direction}");
        }
        return null;
    }

    public void PrintPaths()
    {
        Path.ForEach(position => Console.WriteLine($"{position.row},{position.col}"));
    }
}
