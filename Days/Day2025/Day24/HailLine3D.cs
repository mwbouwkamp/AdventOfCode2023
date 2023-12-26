namespace AdventOfCode2023;

public class HailLine3D
{
    public (long x, long y, long z) StartingPoint { get; set; }
    public (long x, long y, long z) Velocities { get; set; }
    public List<(long x, long y, long z)> Vectors { get; set; }
    public HailLine3D(string input)
    {
        List<string> inputParts = input.Split(" @ ").ToList();
        long px = long.Parse(inputParts.First().Split(", ")[0]);
        long py = long.Parse(inputParts.First().Split(", ")[1]);
        long pz = long.Parse(inputParts.First().Split(", ")[2]);
        long vx = long.Parse(inputParts.Last().Split(", ")[0]);
        long vy = long.Parse(inputParts.Last().Split(", ")[1]);
        long vz = long.Parse(inputParts.Last().Split(", ")[2]);
        StartingPoint = (px, py, pz);
        Velocities = (vx, vy, vz);
        SetVectors();
    }

    public void SetVectors()
    {
        Vectors = new();
        Vectors.Add((Velocities.x, Velocities.y, Velocities.z));
        Vectors.Add((-Velocities.x, -Velocities.y, -Velocities.z));

        Vectors.Add((Velocities.y, -Velocities.x, Velocities.z));
        Vectors.Add((-Velocities.y, Velocities.x, Velocities.z));

        Vectors.Add((Velocities.z, Velocities.y, -Velocities.x));
        Vectors.Add((-Velocities.z, Velocities.y, Velocities.x));

        Vectors.Add((Velocities.x, -Velocities.z, Velocities.y));
        Vectors.Add((Velocities.x, Velocities.z, -Velocities.y));
    }
}
