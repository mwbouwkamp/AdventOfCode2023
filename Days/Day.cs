namespace AdventOfCode2023;

public abstract class Day
{
    protected readonly string input;

    public Day(string input)
    {
        this.input = input;
    }

    public abstract string ExecuteA();

    public abstract string ExecuteB();
}
