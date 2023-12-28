using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023;

public class Day16 : Day
{
    public Day16(string input) : base(input) { }

    public override string ExecuteA()
    {
        RectangleGrid<char> tile = new FileUtils(input).GetCharGrid();

        return SolveForStarintPoint(tile, new State(0, 0, '>')).energized.DistinctBy(state => $"{state.Row},{state.Col}").ToList().Count.ToString();
    }

    private (List<State> energized, List<State> exits) SolveForStarintPoint(RectangleGrid<char> tile, State startingState)
    {
        List<State> energized = new();
        List<State> exits = new() { startingState };
        HashSet<State> fringe = new(new StateComparer()) { startingState };
        while (fringe.Count > 0)
        {
            State current = fringe.ElementAt(0);
            fringe.Remove(current);
            energized.Add(current);
            char instruction = tile.GetElement(current.Row, current.Col);
            List<char> newDirections = new();
            switch (instruction)
            {
                case '.':
                    newDirections.Add(current.Direction);
                    break;
                case '-':
                    if (current.Direction == '>' || current.Direction == '<')
                    {
                        newDirections.Add(current.Direction);
                    }
                    else
                    {
                        newDirections.Add('<');
                        newDirections.Add('>');
                    }
                    break;
                case '|':
                    if (current.Direction == '^' || current.Direction == 'v')
                    {
                        newDirections.Add(current.Direction);
                    }
                    else
                    {
                        newDirections.Add('^');
                        newDirections.Add('v');
                    }
                    break;
                case '\\':
                    if (current.Direction == '>')
                    {
                        newDirections.Add('v');
                    }
                    else if (current.Direction == 'v')
                    {
                        newDirections.Add('>');
                    }
                    else if (current.Direction == '<')
                    {
                        newDirections.Add('^');
                    }
                    else if (current.Direction == '^')
                    {
                        newDirections.Add('<');
                    }
                    break;
                case '/':
                    if (current.Direction == '>')
                    {
                        newDirections.Add('^');
                    }
                    else if (current.Direction == 'v')
                    {
                        newDirections.Add('<');
                    }
                    else if (current.Direction == '<')
                    {
                        newDirections.Add('v');
                    }
                    else if (current.Direction == '^')
                    {
                        newDirections.Add('>');
                    }
                    break;
                default:
                    throw new ArgumentException($"No such instruction: {instruction}");
            }
            newDirections.ForEach(newDirection =>
            {
                Position? newPosition = GetNewPosition(current, newDirection, tile, out bool isExit);
                if (newPosition != null)
                {
                    State newState = new(newPosition.row, newPosition.col, newDirection);
                    if (!energized.Where(state => state.ToString().Equals(newState.ToString())).Any())
                    {
                        fringe.Add(newState);
                    }
                }
                if (isExit)
                {
                    State exitState = new(current.Row, current.Col, current.Direction);
                    if (!exits.Where(state => state.ToString().Equals(exitState.ToString())).Any())
                    {
                        exits.Add(exitState);
                    }
                }
            });
        }
        return (energized, exits);
    }

    private Position? GetNewPosition(State state, char direction, RectangleGrid<char> grid, out bool isExit)
    {
        if (direction == '>' && state.Col < grid.Width - 1)
        {
            isExit = false;
            return new(state.Row, state.Col + 1);
        }
        if (direction == '<' && state.Col > 0)
        {
            isExit = false;
            return new(state.Row, state.Col - 1);
        }
        if (direction == 'v' && state.Row < grid.Height - 1)
        {
            isExit = false;
            return new(state.Row + 1, state.Col);
        }
        if (direction == '^' && state.Row > 0)
        {
            isExit = false;
            return new(state.Row - 1, state.Col);
        }
        isExit = true;
        return null;
    }

    public class State
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public char Direction { get; set; }
        public State(int row, int col, char direction)
        {
            Row = row;
            Col = col;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"({Row},{Col}) {Direction}";
        }
    }

    public class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State? x, State? y)
        {
            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode([DisallowNull] State obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    public override string ExecuteB()
    {
        RectangleGrid<char> tile = new FileUtils(input).GetCharGrid();
        List<State> startingStatesExplored = new();
        List<State> energizedStates = new();

        int highest = 0;

        State upperLeftRight = new(0, 0, '>');
        highest = Math.Max(highest, UpdateIfHigher(tile, upperLeftRight, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State upperLeftDown = new(0, 0, 'v');
        highest = Math.Max(highest, UpdateIfHigher(tile, upperLeftDown, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State upperRightLeft = new(0, tile.Width - 1, '<');
        highest = Math.Max(highest, UpdateIfHigher(tile, upperRightLeft, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State upperRightDown = new(0, tile.Width - 1, 'v');
        highest = Math.Max(highest, UpdateIfHigher(tile, upperRightDown, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State bottomLeftRight = new(tile.Height - 1, 0, '>');
        highest = Math.Max(highest, UpdateIfHigher(tile, bottomLeftRight, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State bottomLeftUp = new(tile.Height - 1, 0, '^');
        highest = Math.Max(highest, UpdateIfHigher(tile, bottomLeftUp, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State bottomRightLeft = new(tile.Height - 1, tile.Width - 1, '<');
        highest = Math.Max(highest, UpdateIfHigher(tile, bottomRightLeft, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        State bottomRightUp = new(tile.Height - 1, tile.Width - 1, '^');
        highest = Math.Max(highest, UpdateIfHigher(tile, bottomRightUp, startingStatesExplored).Count);
        Console.WriteLine("." + highest);

        for (int row = 1; row < tile.Height - 1; row++)
        {
            if (!startingStatesExplored.Where(state => state.Row == row && state.Col == 0).Any())
            {
                State start = new(row, 0, '>');
                highest = Math.Max(highest, UpdateIfHigher(tile, start, startingStatesExplored).Count   );
            }
            if (!startingStatesExplored.Where(state => state.Row == row && state.Col == tile.Width - 1).Any())
            {
                State start = new(row, tile.Width - 1, '<');
                highest = Math.Max(highest, UpdateIfHigher(tile, start, startingStatesExplored).Count);
            }
            Console.WriteLine(highest);
        }

        for (int col = 1; col < tile.Width - 1; col++)
        {
            if (!startingStatesExplored.Where(state => state.Col == col && state.Row == 0).Any())
            {
                State start = new(0, col, 'v');
                highest = Math.Max(highest, UpdateIfHigher(tile, start, startingStatesExplored).Count);
            }
            if (!startingStatesExplored.Where(state => state.Col == col && state.Row == tile.Height - 1).Any())
            {
                State start = new(tile.Height - 1, col, '^');
                highest = Math.Max(highest, UpdateIfHigher(tile, start, startingStatesExplored).Count);
            }
            Console.WriteLine(highest);

        }

        return highest.ToString();
    }

    private List<State> UpdateIfHigher(RectangleGrid<char> tile, State upperLeftRight, List<State> startingStatesExplored)
    {
        List<State> energizedStates = new();
        (List<State> energized, List<State> exits) = SolveForStarintPoint(tile, upperLeftRight);

        energizedStates.AddRange(energized.DistinctBy(state => $"{state.Row},{state.Col}"));
        
        startingStatesExplored.AddRange(exits);
        startingStatesExplored = startingStatesExplored.DistinctBy(state => $"{state.Row},{state.Col}").ToList();
        
        return energizedStates;
    }
}
