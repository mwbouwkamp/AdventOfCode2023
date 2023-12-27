namespace AdventOfCode2023;

public class Day17State
{
    public RectangleGrid<int> Grid { get; set; }
    public Point Position { get; set; }
    public int Direction { get; set; } //trbl
    public int PreviousInDirection { get; set; }
    public int Heuristic { get; set; }
    public int HeatLoss { get; set; }
    private readonly int MaxInDirection = 3;

    public Day17State(RectangleGrid<int> grid, Point position, int direction, int previousInDirection, int heatLoss)
    {
        Grid = grid;
        Position = position;
        Direction = direction;
        PreviousInDirection = previousInDirection;
        HeatLoss = heatLoss;
        Heuristic = (grid.Width - position.col) + (grid.Height - position.row) + heatLoss;
    }

    public List<Day17State> GetChildren()
    {
        List<Day17State> childStates = new();
        if (PreviousInDirection < MaxInDirection)
        {
            try
            {
                Point? straightAhead = GetRelative(Direction);
                childStates.Add(new(Grid, (Point)straightAhead, Direction, PreviousInDirection + 1, HeatLoss + Grid.GetElement(Position.row, Position.col)));
            }
            catch { }
        }

        try
        {
            Point? leftTurn = GetRelative(((Direction - 1) % 4 + 4) % 4);
            childStates.Add(new(Grid, (Point)leftTurn, ((Direction - 1) % 4 + 4) % 4, 0, HeatLoss + Grid.GetElement(Position.row, Position.col)));
        } catch { }

        try
        {
            Point? rightTurn = GetRelative((Direction + 1) % 4);
            childStates.Add(new(Grid, (Point)rightTurn, (Direction + 1) % 4, 0, HeatLoss + Grid.GetElement(Position.row, Position.col)));
        }
        catch { }

        return childStates;
    }

    private Point? GetRelative(int direction)
    {
        switch (direction)
        {
            case 0:
                if (Position.row > 0) 
                    return (Position.row - 1, Position.col);
                break;
            case 1:
                if (Position.col < Grid.Width - 1)
                    return (Position.row, Position.col + 1);
                break;
            case 2:
                if (Position.row < Grid.Height - 1)
                    return (Position.row + 1, Position.col);
                break;
            case 3:
                if (Position.col > 0)
                    return (Position.row, Position.col - 1);
                break;
            default:
                throw new ArgumentException($"Illegal direction: {direction}");
        }
        throw new ArgumentException($"Illegal direction: {direction}");
    }
}

public record struct Point(int row, int col)
{
    public static implicit operator (int row, int col)(Point value)
    {
        return (value.row, value.col);
    }

    public static implicit operator Point((int row, int col) value)
    {
        return new Point(value.row, value.col);
    }
}