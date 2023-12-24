namespace AdventOfCode2023;

public class Day23State : SpotGrid<char>
{
    public List<(int row, int col)> Path { get; set; }
    public (int row, int col) Position { get; set; }
    public int Heuristic { get; set; }
    public Dictionary<(int row, int col), List<(int row, int col)>> NeighboursDictionary;

    public Day23State((int row, int col) position) : base('.') 
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

    public Day23State(Day23State previousState, (int row, int col) position) : base('.')
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
        List<(int row, int col)> childPositions = NeighboursDictionary[Position];

        return childPositions
            .Where(child => !Path.Any(position => position.row == child.row && position.col == child.col))
            .Select(child => new Day23State(this, child))
            .ToList();
    }

    private List<(int row, int col)> GetAllNeighboursA((int row, int col) position)
    {
        List<(int row, int col)> neighbours = new();
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
    private List<(int row, int col)> GetAllNeighboursB((int row, int col) position)
    {
        List<(int row, int col)> neighbours = new();
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
