namespace AdventOfCode2023;

public class SpotGrid<T> : Grid<T>
{
    public Dictionary<(int row, int col), T> Positions { get; set; }

    public int Count { get { return Positions.Count; } }

    public SpotGrid(T defaultValue) : base(defaultValue)
    {
        this.Positions = new();
        this.isInfinite = true;
    }

    public override T GetElement(int row, int col)
    {
        return Positions.TryGetValue((row, col), out T value) ? value : defaultValue;
    }

    public override void SetElement(int row, int col, T value)
    {
        Positions[(row, col)] = value;
        SetMinMaxValues(row, col);
    }

    public override string ToString()
    {
        char[] rows = Enumerable
            .Repeat(new string(defaultValue.ToString().ToCharArray()[0], Width), Height)
            .Aggregate("", (a, b) => a + b + "\n")
            .ToCharArray();

        Positions.Keys.ToList().ForEach(key =>
        {
            rows[(key.row - YMin) * (Width + 1) + (key.col - XMin)] = Positions[(key.row, key.col)].ToString().ToCharArray()[0];
        });
        return new string(rows);
    }

}
