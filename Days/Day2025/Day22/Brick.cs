namespace AdventOfCode2023;

public class Brick
{
    public Coordinate MinCoordinate { get; set; }
    public Coordinate MaxCoordinate { get; set; }
    public Brick(string input)
    {
        string start = input.Split('~').First();
        string end = input.Split('~').Last();

        string[] startParts = start.Split(',');
        int startX = int.Parse(startParts[0]);
        int startY = int.Parse(startParts[1]);
        int startZ = int.Parse(startParts[2]);

        string[] endParts = end.Split(',');
        int endX = int.Parse(endParts[0]);
        int endY = int.Parse(endParts[1]);
        int endZ = int.Parse(endParts[2]);

        MinCoordinate = new Coordinate(Math.Min(startX, endX), Math.Min(startY, endY), Math.Min(startZ, endZ));
        MaxCoordinate = new Coordinate(Math.Max(startX, endX), Math.Max(startY, endY), Math.Max(startZ, endZ));
    }

    public bool Fall(List<Brick> bricks)
    {
        List<int> distancesToFall = bricks
            .Where(brick => brick != this)
            .Where(brick => HasOverlap(brick))
            .Where(brick => IsBelow(brick))
            .Select(brick => DistanceBelow(brick))
            .ToList();

        if (distancesToFall.Count == 0)
            return false;

        int distanceToFall = distancesToFall.Min() - 1;

        if (distanceToFall > 0)
        {
            MinCoordinate.z -= distanceToFall;
            MaxCoordinate.z -= distanceToFall;
            return true;
        }

        return false;
    }
    
    public bool IsBelow(Brick brick)
    {
        return this.MinCoordinate.z - brick.MaxCoordinate.z > 0;
    }
    public int DistanceBelow(Brick brick)
    {
        return Math.Max(0, this.MinCoordinate.z - brick.MaxCoordinate.z);
    }
    public bool HasOverlap(Brick brick)
    {
        bool hasXOverlap = brick.MinCoordinate.x <= this.MaxCoordinate.x && brick.MaxCoordinate.x >= this.MinCoordinate.x;
        bool hasYOverlap = brick.MinCoordinate.y <= this.MaxCoordinate.y && brick.MaxCoordinate.y >= this.MinCoordinate.y;

        return hasXOverlap && hasYOverlap;
    }
    public class Coordinate
    {
        public int x; 
        public int y; 
        public int z;

        public Coordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
