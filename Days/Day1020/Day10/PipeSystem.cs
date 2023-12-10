using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class PipeSystem : RectangleGrid<char>
{
    public Segment Start { get; set; }
    public List<Segment> Segments { get; set; }
    public PipeSystem(List<List<char>> grid, (int row, int col) start) : base(grid, '.', true)
    {
        Start = new(start.row, start.col, 'S');

        Segments = new();

        List<State> fringe = new() { new State(Start, new()) };

        bool hasJustStarted = true;
        while (fringe.Count > 0)
        {
            State current = fringe[^1];
            fringe.Remove(current);

            if (current.Segment.Shape == 'S' && !hasJustStarted)
            {
                Segments = current.Path;
                return;
            }

            GetConnected(current.Segment)
                .Where(segment => (segment.Shape == 'S' && current.Path.Count > 1) || !current.Path.Any(existing => segment.Row == existing.Row && segment.Col == existing.Col && segment.Shape == existing.Shape))
                .ToList()
                .ForEach(segment => 
                {
                    List<Segment> newPath = new(current.Path);
                    newPath.Add(current.Segment);
                    State newState = new State(segment, newPath);
                    fringe.Add(newState);
                });

            hasJustStarted = false;
        }
    }

//| is a vertical pipe connecting north and south.
//- is a horizontal pipe connecting east and west.
//L is a 90-degree bend connecting north and east.
//J is a 90-degree bend connecting north and west.
//7 is a 90-degree bend connecting south and west.
//F is a 90-degree bend connecting south and east.
//. is ground; there is no pipe in this tile.
    public List<Segment> GetConnected(Segment segment)
    {
        List<Segment> connected = new();

        char shape = segment.Shape;
        if (shape == '-' || shape == 'J' || shape == '7' || shape == 'S')
        {
            char west = GetElement(segment.Row, segment.Col - 1);
            if (west == '-' || west == 'L' || west == 'F' || west == 'S')
                connected.Add(new Segment(segment.Row, segment.Col - 1, west));
        }

        if (shape == '-' || shape == 'L' || shape == 'F' || shape == 'S')
        {
            char east = GetElement(segment.Row, segment.Col + 1);
            if (east == '-' || east == 'J' || east == '7' || east == 'S')
                connected.Add(new Segment(segment.Row, segment.Col + 1, east));
        }

        if (shape == '|' || shape == 'L' || shape == 'J' || shape == 'S')
        {
            char north = GetElement(segment.Row - 1, segment.Col);
            if (north == '|' || north == 'F' || north == '7' || north == 'S')
                connected.Add(new Segment(segment.Row - 1, segment.Col, north));
        }

        if (shape == '|' || shape == 'F' || shape == '7' || shape == 'S')
        {
            char south = GetElement(segment.Row + 1, segment.Col);
            if (south == '|' || south == 'L' || south == 'J' || south == 'S')
                connected.Add(new Segment(segment.Row + 1, segment.Col, south));
        }

        return connected;
    }

    public string PipeSystemString()
    {
        SpotGrid<char> grid = new('.');
        Segments
            .ToList()
            .ForEach(segment => grid.SetElement(segment.Row, segment.Col, segment.Shape));
        return grid.ToString();
    }
    public class Segment
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public char Shape { get; set; }

        public Segment(int row, int col, char shape)
        {
            this.Row = row;
            this.Col = col;
            this.Shape = shape;
        }
    }

    public class State
    {
        public Segment Segment { get; set; }
        public List<Segment> Path { get; set; }
        public State(Segment segment, List<Segment> path)
        {
            Segment = segment;
            Path = path;
        }

        public override string ToString()
        {
            return $"({Segment.Row},{Segment.Col}) - {Segment.Shape}";
        }
    }

    class SegmentComparer : EqualityComparer<Segment>
    {
        public override bool Equals(Segment first, Segment second)
        {
            return first.Row == second.Row && first.Col == second.Col && first.Shape == second.Shape;
        }

        public override int GetHashCode(Segment segment)
        {
            return ($"{segment.Row}.{segment.Col},{segment.Shape}").GetHashCode();
        }
    }
}
