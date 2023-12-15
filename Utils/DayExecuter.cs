﻿namespace AdventOfCode2023;

public class DayExecuter
{
    private readonly Day day;
    public DayExecuter(string day, string input)
    {
        this.day = day switch
        {
            "Day01" => new Day01(input),
            "Day02" => new Day02(input),
            "Day03" => new Day03(input),
            "Day04" => new Day04(input),
            "Day05" => new Day05(input),
            "Day06" => new Day06(input),
            "Day07" => new Day07(input),
            "Day08" => new Day08(input),
            "Day09" => new Day09(input),
            "Day10" => new Day10(input),
            "Day11" => new Day11(input),
            "Day12" => new Day12(input),
            "Day13" => new Day13(input),
            "Day14" => new Day14(input),
            "Day15" => new Day15(input),
            _ => throw new NotImplementedException($"Day not yet implemented: {day}"),
        };
    }

    public string ExecuteA()
    {
        return day.ExecuteA();
    }

    public string ExecuteB()
    {
        return day.ExecuteB();
    }
}
