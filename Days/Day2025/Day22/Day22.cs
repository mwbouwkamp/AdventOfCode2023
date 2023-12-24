namespace AdventOfCode2023;

public class Day22 : Day
{
    public Day22(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<Brick> bricks = GetBricks();

        int minX = bricks
            .Select(brick => Math.Min(brick.MinCoordinate.x, brick.MaxCoordinate.x))
            .Min();
        int maxX = bricks
            .Select(brick => Math.Max(brick.MinCoordinate.x, brick.MaxCoordinate.x))
            .Max();
        int minY = bricks
            .Select(brick => Math.Min(brick.MinCoordinate.y, brick.MaxCoordinate.y))
            .Min();
        int maxY = bricks
            .Select(brick => Math.Max(brick.MinCoordinate.y, brick.MaxCoordinate.y))
            .Max();

        Brick ground = new($"{minX},{minY},0~{maxX},{maxY},0");

        bricks.Add(ground);

        List<Brick> lowest = new() { ground };

        bricks.Sort((a, b) => a.MinCoordinate.z - b.MinCoordinate.z);
        SettleBricksLoop(bricks);

        // 301 - 626

        return (bricks
            .Where(brick =>
            {
                if (lowest.Contains(brick))
                {
                    return true;
                }
                List<Brick> brickRemoved = new(bricks);
                brickRemoved.Remove(brick);
                bool canBeRemoved = !SettleBricks(brickRemoved);
                return canBeRemoved;
            })
            .Count() - 1)
            .ToString();
    }

    private static void SettleBricksLoop(List<Brick> bricks)
    {
        Boolean falling = true;
        while (falling)
        {
            falling = SettleBricks(bricks);
        }
    }

    private static bool SettleBricks(List<Brick> bricks)
    {
        bool falling = false;
        foreach (Brick brick in bricks)
            falling = brick.Fall(bricks) || falling;
        return falling;
    }

    private List<Brick> GetBricks()
    {
        return new FileUtils(input)
            .GetLines()
            .Select(line => new Brick(line))
            .ToList();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
