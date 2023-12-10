using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day03 : Day
{
    public Day03(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();

        List<EnginePart> engineParts = GetEngineParts(lines);

        List<List<char>> gridInput = lines.Select(line => line.ToCharArray().ToList()).ToList();
        Grid<char> rectangleGriud = new RectangleGrid<char>(gridInput, '.', true);

        return engineParts
            .Where(position => position.GetNeighbour(rectangleGriud) != '.')
            .Select(position => int.Parse(position.Id))
            .Sum()
            .ToString();
    }

    private static List<EnginePart> GetEngineParts(List<string> lines)
    {
        List<EnginePart> engineParts = new();

        for (int i = 0; i < lines.Count; i++)
        {
            Regex.Matches(lines[i], "\\d+")
                .Select(match => (match.Index, match.Value))
                .ToList()
                .ForEach(numbers => engineParts.Add(new(numbers.Index, i, numbers.Item2)));
        }

        return engineParts;
    }

    public override string ExecuteB()
    {
        List<string> lines = new FileUtils(input).GetLines();

        List<List<char>> gridInput = lines.Select(line => line.ToCharArray().ToList()).ToList();
        Grid<char> rectangleGrid = new RectangleGrid<char>(gridInput, '.', true);

        List<EnginePart> gearParts = GetEngineParts(lines)
            .Where(enginePart => enginePart.GetNeighbour(rectangleGrid) == '*')
            .ToList();

        gearParts
            .ForEach(gearPart => gearPart.SetNeighbour(rectangleGrid));

        int sum = 0;
        for (int row = 0; row < gridInput.Count; row++)
        {
            for (int col = 0; col < gridInput[0].Count; col++)
            {
                if (rectangleGrid.GetElement(row, col) == '*')
                {
                    List<int> gearPartIds = gearParts
                        .Where(gearPart => gearPart.Neighbour.row == row && gearPart.Neighbour.col == col)
                        .Select(gearPart => int.Parse(gearPart.Id))
                        .ToList();
                    if (gearPartIds.Count ==2) 
                        sum += gearPartIds[0] * gearPartIds[1];
                }
            }
        }
        return sum.ToString();
    }
}
