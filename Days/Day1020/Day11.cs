using System.Text;

namespace AdventOfCode2023;

public class Day11 : Day
{

    public Day11(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();

        List<int> emptyRows = GetEmptyRowIndexes(lines);

        RectangleGrid<char> initialGrid = new(lines.Select(line => line.ToCharArray().ToList()).ToList(), '.');
        List<int> emptyCols = GetEmptyColIndexes(initialGrid);

        long NUMTOINSERT = 2;
        GalaxyMap galaxyMap = GetGalaxyMap(emptyRows, initialGrid, emptyCols, NUMTOINSERT);

        return GetLengths(galaxyMap).ToString();
    }

    private static long GetLengths(GalaxyMap galaxyMap)
    {
        long lengths = 0;
        List<(long row, long col)> positions = galaxyMap.Positions;
        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                lengths += Math.Abs(positions[i].row - positions[j].row) + Math.Abs(positions[i].col - positions[j].col);
            }
        }

        return lengths;
    }

    private static GalaxyMap GetGalaxyMap(List<int> emptyRows, RectangleGrid<char> initialGrid, List<int> emptyCols, long NUMTOINSERT)
    {
        GalaxyMap galaxyMap = new();
        for (int row = 0; row < initialGrid.Height; row++)
        {
            for (int col = 0; col < initialGrid.Width; col++)
            {
                if (initialGrid.GetElement(row, col) == '#')
                {
                    int rowInserts = emptyRows
                        .Where(emptyRow => emptyRow < row)
                        .Count();
                    int colInserts = emptyCols
                        .Where(emptyCol => emptyCol < col)
                        .Count();
                    galaxyMap.SetElement(row + rowInserts * (NUMTOINSERT - 1), col + colInserts * (NUMTOINSERT - 1));
                }
            }
        }

        return galaxyMap;
    }

    private static List<int> GetEmptyColIndexes(RectangleGrid<char> initialGrid)
    {
        List<int> emptyCols = new();
        for (int col = 0; col < initialGrid.Width; col++)
        {
            bool isEmpty = true;
            for (int row = 0; row < initialGrid.Height; row++)
            {
                if (initialGrid.GetElement(row, col) != '.')
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
                emptyCols.Add(col);
        }

        return emptyCols;
    }

    private static List<int> GetEmptyRowIndexes(List<string> lines)
    {
        return lines
            .Select((line, index) => line.Replace(".", "").Length == 0 ? index : -1)
            .Where(index => index > 0)
            .ToList();
    }

    public override string ExecuteB()
    {
        List<string> lines = new FileUtils(input).GetLines();

        List<int> emptyRows = GetEmptyRowIndexes(lines);

        RectangleGrid<char> initialGrid = new(lines.Select(line => line.ToCharArray().ToList()).ToList(), '.');
        List<int> emptyCols = GetEmptyColIndexes(initialGrid);

        long NUMTOINSERT = 1000000;
        GalaxyMap galaxyMap = GetGalaxyMap(emptyRows, initialGrid, emptyCols, NUMTOINSERT);

        return GetLengths(galaxyMap).ToString();
    }

    public class GalaxyMap
    {
        public List<(long row, long col)> Positions { get; set; }

        public GalaxyMap()
        {
            this.Positions = new();
        }

        public void SetElement(long row, long col)
        {
            Positions.Add((row, col));
        }
    }
}
