namespace AdventOfCode2023;

public class Day23State : SpotGrid<char>
{
    public List<Position> Path { get; set; }
    public Position Position { get; set; }
    public int Heuristic { get; set; }
    public Dictionary<Position, List<Position>> NeighboursDictionary;

    public Day23State(Position position) : base('.') 
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

    public Day23State(Day23State previousState, Position position) : base('.')
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
        List<Position> childPositions = NeighboursDictionary[Position];
        //List<Point> childPositions = NeighboursDictionary[NeighboursDictionary.Keys
        //    .First(key => key.row == Position.row && key.col == Position.col)];
            

        return childPositions
            .Where(child => !Path.Any(position => position.row == child.row && position.col == child.col))
            .Select(child => new Day23State(this, child))
            .ToList();
    }

    private List<Position> GetAllNeighboursA(Position position)
    {
        List<Position> neighbours = new();
        if (position.row > 0 && GetElement(position.row - 1, position.col) != '#' && GetElement(position.row - 1, position.col) != 'v')
            neighbours.Add(new(position.row - 1, position.col));
        if (position.row < Height - 1 && GetElement(position.row + 1, position.col) != '#' && GetElement(position.row + 1, position.col) != '^')
            neighbours.Add(new(position.row + 1, position.col));
        if (position.col > 0 && GetElement(position.row, position.col - 1) != '#' && GetElement(position.row, position.col - 1) != '>')
            neighbours.Add(new(position.row, position.col - 1));
        if (position.col < Width - 1 && GetElement(position.row, position.col + 1) != '#' && GetElement(position.row, position.col + 1) != '<')
            neighbours.Add(new(position.row, position.col + 1));
        return neighbours;
    }
    private List<Position> GetAllNeighboursB(Position position)
    {
        List<Position> neighbours = new();
        if (position.row > 0 && GetElement(position.row - 1, position.col) != '#')
            neighbours.Add(new(position.row - 1, position.col));
        if (position.row < Height - 1 && GetElement(position.row + 1, position.col) != '#')
            neighbours.Add(new(position.row + 1, position.col));
        if (position.col > 0 && GetElement(position.row, position.col - 1) != '#')
            neighbours.Add(new(position.row, position.col - 1));
        if (position.col < Width - 1 && GetElement(position.row, position.col + 1) != '#')
            neighbours.Add(new(position.row, position.col + 1));
        return neighbours;
    }
}
