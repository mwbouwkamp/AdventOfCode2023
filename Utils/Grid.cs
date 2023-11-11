namespace AdventOfCode2023;

public class Grid<T>
{
    private readonly List<List<T>> grid;
    private readonly int width;
    public int Width
    {
        get
        {
            if (width == int.MinValue)
                throw new InvalidOperationException("Not all lines in grid are equal");
            return width;
        }
    }
    public int Height { get; }


    public Grid(List<List<T>> grid) 
    {
        this.grid = grid;
        this.width = CalculateWidth(grid);
        Height = grid.Count;
    }

    /// <summary>Calculates the width.</summary>
    /// <param name="grid">The grid.</param>
    /// <returns>
    ///   Width of the grid or int.Minvalue if all lines in the grid are not equal.
    /// </returns>
    private static int CalculateWidth(List<List<T>> grid)
    {
        int firstLineCount = grid.FirstOrDefault()?.Count ?? 0;
        return grid.Any(line => line.Count != firstLineCount) ? int.MinValue : firstLineCount;
    }

    /// <summary>Gets the element at a row and column.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The column.</param>
    /// <returns>
    ///   element at row and column.
    /// </returns>
    public T GetElement(int row, int col)
    {
        return grid[row][col];
    }

    /// <summary>Sets the element at a row and column.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The column.</param>
    /// <param name="value">The value to set.</param>
    public void SetElement(int row, int col, T value) {
        grid[row][col] = value;
    }
}
