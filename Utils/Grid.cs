namespace AdventOfCode2023;

public class Grid<T>
{
    private List<List<T>> grid;

    public Grid(List<List<T>> grid) 
    {
        this.grid = grid;
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
