namespace AdventOfCode2023;

public class Day23State : SpotGrid<char>
{
    public List<Point> Path { get; set; }
    public Point Position { get; set; }
    public int Heuristic { get; set; }
    public Dictionary<Point, List<Point>> NeighboursDictionary;

    public Day23State(Point position) : base('.') 
    {
        Path = new() { position };
        Position = position;
        Heuristic = 0;
        NeighboursDictionary = new();
    }

    public void SetChildrenA()
    {
        NeighboursDictionary = Positions.Keys
            .ToDictionary(position => position, position => GetAllNeighboursA(position));
    }

    public void SetChildrenB()
    {
        NeighboursDictionary = Positions.Keys
            .ToDictionary(position => position, position => GetAllNeighboursB(position));
    }

    public Day23State(Day23State previousState, Point position) : base('.')
    {
        Positions = previousState.Positions;
        Position = position;
        Path = new(previousState.Path)
        {
            position
        };
        NeighboursDictionary = previousState.NeighboursDictionary;

        SetMinMaxValues(Positions.Keys.Select(key => key.row).Max(), Positions.Keys.Select(key => key.col).Max());
        Heuristic = Path.Count + (Height - position.row) * (Height - position.row) + (Width - position.col) * (Width - position.col); 
    }

    public List<Day23State> GetChildren()
    {
        List<Point> childPositions = NeighboursDictionary[Position];

        return childPositions
            .Where(child => !Path.Any(position => position.row == child.row && position.col == child.col))
            .Select(child => new Day23State(this, child))
            .ToList();
    }

    private List<Point> GetAllNeighboursA(Point position)
    {
        List<Point> neighbours = new();
        if (position.row > 0 && GetElement(position.row - 1, position.col) != '#' && GetElement(position.row - 1, position.col) != 'v')
            neighbours.Add((position.row - 1, position.col));
        if (position.row < Height - 1 && GetElement(position.row + 1, position.col) != '#' && GetElement(position.row + 1, position.col) != '^')
            neighbours.Add((position.row + 1, position.col));
        if (position.col > 0 && GetElement(position.row, position.col - 1) != '#' && GetElement(position.row, position.col - 1) != '>')
            neighbours.Add((position.row, position.col - 1));
        if (position.col < Width - 1 && GetElement(position.row, position.col + 1) != '#' && GetElement(position.row, position.col + 1) != '<')
            neighbours.Add((position.row, position.col + 1));
        return neighbours;
    }
    private List<Point> GetAllNeighboursB(Point position)
    {
        List<Point> neighbours = new();
        if (position.row > 0 && GetElement(position.row - 1, position.col) != '#')
            neighbours.Add((position.row - 1, position.col));
        if (position.row < Height - 1 && GetElement(position.row + 1, position.col) != '#')
            neighbours.Add((position.row + 1, position.col));
        if (position.col > 0 && GetElement(position.row, position.col - 1) != '#')
            neighbours.Add((position.row, position.col - 1));
        if (position.col < Width - 1 && GetElement(position.row, position.col + 1) != '#')
            neighbours.Add((position.row, position.col + 1));
        return neighbours;
    }
}
