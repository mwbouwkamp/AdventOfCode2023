using System.Text;

namespace AdventOfCode2023;

public class Day11 : Day
{

    public Day11(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();

        List<int> emptyRows = lines
            .Select((line, index) => line.Replace(".", "").Length == 0 ? index : -1)
            .Where(index => index > 0)
            .ToList();

        RectangleGrid<char> initialGrid = new(lines.Select(line => line.ToCharArray().ToList()).ToList(), '.');
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

        lines = lines
            .Select(line =>
            {
                StringBuilder builder = new(line);
                for (int col = 0; col < emptyCols.Count; col++)
                {
                    builder.Insert(emptyCols[col] + col, '.');
                }
                return builder.ToString();
            })
            .ToList();

        int newWidth = lines[0].Length;
        for (int row = 0; row < emptyRows.Count; row++)
        {
            string newLine = new('.', newWidth);
            lines.Insert(emptyRows[row] + row, newLine);
        }

        RectangleGrid<char> adaptedGrid = new(lines.Select(line => line.ToCharArray().ToList()).ToList(), '.');

        SpotGrid<char> spotGrid = new('.');
        for (int row = 0; row < adaptedGrid.Height; row++)
        {
            for (int col = 0; col < adaptedGrid.Width; col++)
            {
                if (adaptedGrid.GetElement(row, col) == '#')
                    spotGrid.SetElement(row, col, '#');
            }
        }

        int lengths = 0;
        List<(int row, int col)> positions = spotGrid.Positions.Keys.ToList();
        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i + 1; j< positions.Count; j++)
            {
                lengths += Math.Abs(positions[i].row - positions[j].row) + Math.Abs(positions[i].col - positions[j].col);
            }
        }

        //High: 1490641232
        return lengths.ToString();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
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
