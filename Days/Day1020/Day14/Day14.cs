namespace AdventOfCode2023;

public class Day14 : Day
{
    public RectangleGrid<char> Grid { get; set; }
    public Day14(string input) : base(input) { }

    public override string ExecuteA()
    {
        Grid = new FileUtils(input).GetCharGrid();
        MoveNorth();

        return Calculate().ToString();
    }

    private int Calculate()
    {
        int result = 0;
        for (int row = 0; row < Grid.Height; row++)
        {
            for (int col = 0; col < Grid.Width; col++)
            {
                if (Grid.GetElement(row, col) == 'O')
                    result += (Grid.Height - row);
            }
        }

        return result;
    }

    private void MoveNorth()
    {
        for (int col = 0; col < Grid.Width; col++)
        {
            bool moved = true;
            while (moved)
            {
                moved = false;
                for (int row = 1; row < Grid.Height; row++)
                {
                    if (Grid.GetElement(row, col) == 'O' && Grid.GetElement(row - 1, col) == '.')
                    {
                        moved = true;
                        Grid.SetElement(row, col, '.');
                        Grid.SetElement(row - 1, col, 'O');
                    }
                }
            }
        }
    }

    private void MoveSouth()
    {
        for (int col = 0; col < Grid.Width; col++)
        {
            bool moved = true;
            while (moved)
            {
                moved = false;
                for (int row = 0; row < Grid.Height - 1; row++)
                {
                    if (Grid.GetElement(row, col) == 'O' && Grid.GetElement(row + 1, col) == '.')
                    {
                        moved = true;
                        Grid.SetElement(row, col, '.');
                        Grid.SetElement(row + 1, col, 'O');
                    }
                }
            }
        }
    }

    private void MoveEast()
    {
        for (int row = 0; row < Grid.Height; row++)
        {
            bool moved = true;
            while (moved)
            {
                moved = false;
                for (int col = 0; col < Grid.Width - 1; col++)
                {
                    if (Grid.GetElement(row, col) == 'O' && Grid.GetElement(row, col + 1) == '.')
                    {
                        moved = true;
                        Grid.SetElement(row, col, '.');
                        Grid.SetElement(row, col + 1, 'O');
                    }
                }
            }
        }
    }

    private void MoveWest()
    {
        for (int row = 0; row < Grid.Height; row++)
        {
            bool moved = true;
            while (moved)
            {
                moved = false;
                for (int col = 1; col < Grid.Width; col++)
                {
                    if (Grid.GetElement(row, col) == 'O' && Grid.GetElement(row, col - 1) == '.')
                    {
                        moved = true;
                        Grid.SetElement(row, col, '.');
                        Grid.SetElement(row, col - 1, 'O');
                    }
                }
            }
        }
    }

    public override string ExecuteB()
    {
        Grid = new FileUtils(input).GetCharGrid();

        int resultN = 0;
        int resultW = 0;
        int resultS = 0;
        int resultE = 0;
        bool stable = false;
        long i = 0;
        while(!stable)
        {
            MoveNorth();
            //Console.WriteLine(Grid);
            //Console.WriteLine(" ");
            int resultNNew = Calculate();
            MoveWest();
            //Console.WriteLine(Grid);
            //Console.WriteLine(" ");
            int resultWNew = Calculate();
            MoveSouth();
            //Console.WriteLine(Grid);
            //Console.WriteLine(" ");
            int resultSNew = Calculate();
            MoveEast();
            //Console.WriteLine(Grid);
            //Console.WriteLine(" ");
            int resultENew = Calculate();
            Console.WriteLine($"{i++},{resultENew}");
            //Console.WriteLine("=====");
            stable = resultN == resultNNew && resultW == resultWNew && resultS == resultSNew && resultE == resultENew;
            resultN = resultNNew;
            resultW = resultWNew;
            resultS = resultSNew;
            resultE = resultENew;

        }

        return resultE.ToString();
    }
}
