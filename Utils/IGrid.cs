namespace AdventOfCode2023;

public abstract class IGrid<T>
{
    protected T defaultValue;
    protected bool isInfinite;

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

    protected void SetMinMaxValues(int row, int col)
    {
        XMin = Math.Min(XMin, col);
        XMax = Math.Max(XMax, col);
        YMin = Math.Min(YMin, row);
        YMax = Math.Max(YMax, row);
    }


    public abstract T? GetElement(int row, int col);

    public abstract void SetElement(int row, int col, T value);
}