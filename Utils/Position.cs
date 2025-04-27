namespace AdventOfCode2023;

//public record struct Point(int row, int col)
//{
//    public static implicit operator (int row, int col)(Point value)
//    {
//        return (value.row, value.col);
//    }

//    public static implicit operator Point((int row, int col) value)
//    {
//        return new Point(value.row, value.col);
//    }
//}

public class Position
{
    public int row { get; set; }
    public int col { get; set; }
    public Position(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Position other = (Position)obj;

        return col == other.col && row == other.row;
    }

    public override int GetHashCode()
    {
        return $"{row},{col}".GetHashCode();
    }

    public override string ToString()
    {
        return $"{row},{col}";
    }

}

//public class PositionComparer : IEqualityComparer<Position>
//{
//    public bool Equals(Position? x, Position? y)
//    {
//        return x.col == y.col && x.row == y.row;
//    }

//    public int GetHashCode(Position obj)
//    {
//        return $"{obj.row},{obj.col}".GetHashCode();
//    }
//}
