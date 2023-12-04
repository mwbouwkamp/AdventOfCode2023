using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class EnginePart
{
    private int Col { get; }
    private int Row { get; }
    public string Id { get; }
    public (int row, int col, char partType) Neighbour { get; set; }

    public EnginePart(int col, int row, string id)
    {
        Col = col;
        Row = row;
        Id = id;
    }

    public char GetNeighbour(Grid<char> grid)
    {
        for (int col = Col - 1; col <= Col + Id.Length; col++)
        {
            if (grid.GetElement(Row - 1, col) != '.')
                return grid.GetElement(Row - 1, col);
            if (grid.GetElement(Row + 1, col) != '.')
                return grid.GetElement(Row + 1, col);
        }
        if (grid.GetElement(Row, Col - 1) != '.')
            return grid.GetElement(Row, Col - 1);
        if (grid.GetElement(Row, Col + Id.Length) != '.')
            return grid.GetElement(Row, Col + Id.Length);
        return '.';
    }

    public char SetNeighbour(Grid<char> grid)
    {
        for (int col = Col - 1; col <= Col + Id.Length; col++)
        {
            if (grid.GetElement(Row - 1, col) != '.')
                Neighbour = (Row - 1, col, grid.GetElement(Row - 1, col));
            if (grid.GetElement(Row + 1, col) != '.')
                Neighbour = (Row + 1, col, grid.GetElement(Row + 1, col));
        }
        if (grid.GetElement(Row, Col - 1) != '.')
            Neighbour = (Row, Col - 1, grid.GetElement(Row, Col - 1));
        if (grid.GetElement(Row, Col + Id.Length) != '.')
            Neighbour = (Row, Col + Id.Length, grid.GetElement(Row, Col + Id.Length));
        return '.';
    }
}
