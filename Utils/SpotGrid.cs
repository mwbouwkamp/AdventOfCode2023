namespace AdventOfCode2023;

public class SpotGrid<T> : Grid<T>
{
    readonly Dictionary<(int row, int col), T> positions;

    public SpotGrid(T defaultValue) : base(defaultValue)
    {
        this.positions = new();
        this.isInfinite = true;
    }

    public override T GetElement(int row, int col)
    {
        return positions.TryGetValue((row, col), out T value) ? value : defaultValue;
    }

    public override void SetElement(int row, int col, T value)
    {
        positions[(row, col)] = value;
        SetMinMaxValues(row, col);
    }

    public override string ToString()
    {
        char[] rows = Enumerable
            .Repeat(new string(defaultValue.ToString().ToCharArray()[0], Width), Height)
            .Aggregate("", (a, b) => a + b + "\n")
            .ToCharArray();

        positions.Keys.ToList().ForEach(key =>
        {
            rows[(key.row - YMin) * (Width + 1) + (key.col - XMin)] = positions[(key.row, key.col)].ToString().ToCharArray()[0];
        });
        return new string(rows);
    }

}
