namespace AdventOfCode2023;

public class Day18 : Day
{
    public Day18(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();
        SpotGrid<string> grid = new("#000000");
        (int row, int col) currentPosition = (0, 0);
        grid.SetElement(currentPosition.row, currentPosition.col, "#70c710");
        lines.ForEach(line =>
        {
            string[] parts = line.Split(' ');
            string color = parts[2].Replace("(", "").Replace(")", "");
            switch (parts[0])
            {
                case "R":
                    for (int i = 0; i < int.Parse(parts[1]); i++)
                    {
                        currentPosition = (currentPosition.row, currentPosition.col + 1);
                        grid.SetElement(currentPosition.row, currentPosition.col, color);
                    }
                    break;
                case "L":
                    for (int i = 0; i < int.Parse(parts[1]); i++)
                    {
                        currentPosition = (currentPosition.row, currentPosition.col - 1);
                        grid.SetElement(currentPosition.row, currentPosition.col, color);
                    }
                    break;
                case "U":
                    for (int i = 0; i < int.Parse(parts[1]); i++)
                    {
                        currentPosition = (currentPosition.row - 1, currentPosition.col);
                        grid.SetElement(currentPosition.row, currentPosition.col, color);
                    }
                    break;
                case "D":
                    for (int i = 0; i < int.Parse(parts[1]); i++)
                    {
                        currentPosition = (currentPosition.row + 1, currentPosition.col);
                        grid.SetElement(currentPosition.row, currentPosition.col, color);
                    }
                    break;
                default:
                    throw new ArgumentException($"Unsupported direction : {parts[0]}");
            }
        });

        for (int row = grid.YMin; row <= grid.YMax; row++)
        {
            bool isDigging = grid.GetElement(row, grid.XMin) != "#000000";
            for (int col = grid.XMin + 1; col <= grid.XMax; col++)
            {
                string current = grid.GetElement(row, col);
                if (current.Equals("#000000") && isDigging)
                {
                    grid.SetElement(row, col, "#ffffff");
                }
                if (isDigging)
                {
                    if (!current.Equals("#000000") && grid.GetElement(row, col - 1).Equals("#ffffff"))
                    {
                        isDigging = false;
                    } 
                }
                else if (!current.Equals("#000000") )
                {
                    isDigging = true;
                }
                Console.Write(grid.GetElement(row, col).Equals("#ffffff") ? "." : "#");
            }
            Console.WriteLine(" ");
        }


        //HIGH 55853

        return grid.Positions
            .Where(position => !position.Equals("#000000"))
            .Count()
            .ToString();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
