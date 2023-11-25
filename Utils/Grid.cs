namespace AdventOfCode2023;

public abstract class Grid<T>
{
    protected readonly T defaultValue;
    protected  bool isInfinite;

    protected int XMin { get; set; }
    protected int XMax { get; set; }
    protected int YMin { get; set; }
    protected int YMax { get; set; }

    public int Width
    {
        get
        {
            return XMax - XMin + 1;
        }
    }
    public int Height
    {
        get
        {
            return YMax - YMin + 1;
        }
    }

    public Grid(T defaultValue)
    {
        if (defaultValue == null)
            throw new ArgumentNullException(nameof(defaultValue), "defaultValue cannot be null");
        this.defaultValue = defaultValue;
    }

    /// <summary>Sets the minimum maximum values based on current values and row and col value.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The col.</param>
    protected void SetMinMaxValues(int row, int col)
    {
        XMin = Math.Min(XMin, col);
        XMax = Math.Max(XMax, col);
        YMin = Math.Min(YMin, row);
        YMax = Math.Max(YMax, row);
    }

    /// <summary>Gets the element at a row and column.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The column.</param>
    /// <returns>
    ///   element at row and column.
    /// </returns>
    public abstract T GetElement(int row, int col);

    /// <summary>Sets the element at a row and column.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The column.</param>
    /// <param name="value">The value to set.</param>
    public abstract void SetElement(int row, int col, T value);
}