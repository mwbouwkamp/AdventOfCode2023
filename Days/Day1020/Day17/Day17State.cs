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

    public Day17State(RectangleGrid<int> grid, Position position, int direction, int previousInDirection, int heatLoss, List<Position> path)
    {
        Grid = grid;
        Position = position;
        Direction = direction;
        PreviousInDirection = previousInDirection;
        HeatLoss = heatLoss;
        Heuristic = (grid.Width - position.col) + (grid.Height - position.row) + heatLoss;
        Path = new(path);
        path.Add(Position);
    }

    public List<Day17State> GetChildren()
    {
        List<Day17State> childStates = new();
        if (PreviousInDirection < MaxInDirection)
        {
            try
            {
                Position straightAhead = GetRelative(Direction);
                childStates.Add(new(Grid, straightAhead, Direction, PreviousInDirection + 1, HeatLoss + Grid.GetElement(Position.row, Position.col), Path));
            }
            catch { }
        }

        try
        {
            int direction = ((Direction - 1) % 4 + 4) % 4;
            Position leftTurn = GetRelative(direction);
            childStates.Add(new(Grid, leftTurn, direction, 0, HeatLoss + Grid.GetElement(Position.row, Position.col), Path));
        } catch { }

        try
        {
            int direction = (Direction + 1) % 4;
            Position rightTurn = GetRelative(direction);
            childStates.Add(new(Grid, rightTurn, direction, 0, HeatLoss + Grid.GetElement(Position.row, Position.col), Path));
        }
        catch { }

        return childStates;
    }

    private Position GetRelative(int direction)
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
        throw new ArgumentException($"Illegal direction: {direction}");
    }

    public void PrintPaths()
    {
        Path.ForEach(point => Console.WriteLine($"{point.row},{point.col}"));
    }
}
