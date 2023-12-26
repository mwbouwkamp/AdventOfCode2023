using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023;

public class Day24 : Day
{
    public Day24(string input) : base(input) { }

    public override string ExecuteA()
    {
        // 290 - 24265

        return SolveA(200000000000000, 400000000000000).Count().ToString();
    }

    public HashSet<(double x, double y)> SolveA(double min, double max)
    {
        List<HailLine2D> lines = new FileUtils(input).GetLines()
            .Select(line => new HailLine2D(line))
            .ToList();

        HashSet<(double x, double y)> intersections = new(new PositionEqualityComparer());
        HashSet<HailLine2D> passed = new();
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = i + 1; j < lines.Count; j++)
            {
                double intersectionX = (lines[j].Alpha - lines[i].Alpha) / (lines[i].Coefficient - lines[j].Coefficient);
                double intersectionY = lines[i].Coefficient * intersectionX + lines[i].Alpha;
                if (intersectionX >= min && intersectionX <= max && intersectionY >= min && intersectionY <= max)
                {
                    if (InFuture(lines[i], intersectionX, intersectionY) && InFuture(lines[j], intersectionX, intersectionY))
                    {
                        intersections.Add((intersectionX, intersectionY));
                        passed.Add(lines[i]);
                        passed.Add(lines[j]);

                    }
                }
            }
        }
        return intersections;
    }

    private bool InFuture(HailLine2D line, double intersectionX, double intersectionY)
    {
        return Math.Sign(intersectionX - line.StartingPoint.x) == Math.Sign(line.Velocities.x) &&
            Math.Sign(intersectionY - line.StartingPoint.y) == Math.Sign(line.Velocities.y);
    }
    public override string ExecuteB()
    {
        List<HailLine3D> lines = new FileUtils(input).GetLines()
            .Select(line => new HailLine3D(line))
            .ToList();

        List<(long x, long y, long z)> starts = new();

        lines.ForEach(line =>
        {
            List<(long x, long y, long z)> potentialStarts = new();
            for (int i = 0; i < 20; i++)
            {
                (long x, long y, long z) intersection = (
                    line.StartingPoint.x + i * line.Velocities.x,
                    line.StartingPoint.y + i * line.Velocities.y,
                    line.StartingPoint.z + i * line.Velocities.z);
                potentialStarts.AddRange(line.Vectors.Select(vector => (intersection.x + i * vector.x, intersection.y + i * vector.y, intersection.z + i * vector.z)));
            }
            if (starts.Count == 0 ) 
            {
                starts = potentialStarts;
            }
            else
            {
                starts = starts.Intersect(potentialStarts, new Position3DComparer()).ToList();
            }
        });
        return "";

    }
}

internal class Position3DComparer : IEqualityComparer<(long x, long y, long z)>
{
    public bool Equals((long x, long y, long z) x, (long x, long y, long z) y)
    {
        return x.x == y.x && x.y == y.y && x.z == y.z;
    }

    public int GetHashCode([DisallowNull] (long x, long y, long z) obj)
    {
        return $"{obj.x},{obj.y},{obj.z}".GetHashCode();
    }
}

public class PositionEqualityComparer : IEqualityComparer<(double x, double y)>
{
    public bool Equals((double x, double y) x, (double x, double y) y)
    {
        return x.x == y.x && x.y == y.y;
    }

    public int GetHashCode([DisallowNull] (double x, double y) obj)
    {
        return $"{obj.x},{obj.y}".GetHashCode();
    }
}