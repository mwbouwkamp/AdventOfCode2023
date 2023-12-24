namespace AdventOfCode2023;

public class HailLine2D
{
    public (double x, double y) StartingPoint { get; set; }
    public (double x, double y) Velocities { get; set; }
    public double Coefficient { get; set; }
    public double Alpha { get; set; }
    public HailLine2D(string input)
    {
        List<string> inputParts = input.Split(" @ ").ToList();
        long px = long.Parse(inputParts.First().Split(", ")[0]);
        long py = long.Parse(inputParts.First().Split(", ")[1]);
        long vx = long.Parse(inputParts.Last().Split(", ")[0]);
        long vy = long.Parse(inputParts.Last().Split(", ")[1]);
        StartingPoint = ((double)px, (double)py);
        Velocities = ((double)vx, (double)vy);
        Coefficient = (double) vy / (double) vx;
        Alpha = (double)py - Coefficient * (double)px;
    }
}
