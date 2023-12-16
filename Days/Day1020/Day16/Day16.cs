using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023;

public class Day16 : Day
{
    public Day16(string input) : base(input) { }

    public override string ExecuteA()
    {
        RectangleGrid<char> tile = new FileUtils(input).GetCharGrid();

        List<State> energized = new();
        HashSet<State> fringe = new(new StateComparer()) { new State(0, 0, '>') };
        while (fringe.Count > 0)
        {
            State current = fringe.ElementAt(0);
            Console.WriteLine(current);
            fringe.Remove(current);
            energized.Add(current);
            char instruction = tile.GetElement(current.Row, current.Col);
            (int row, int col) newPosition;
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
                    } else
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
                try
                {
                    newPosition = GetNewPosition(current, newDirection, tile);
                    State newState = new(newPosition.row, newPosition.col, newDirection);
                    if (!energized.Where(state => state.ToString().Equals(newState.ToString())).Any()) 
                    {
                        fringe.Add(newState);
                    }
                }
                catch { };
            });
        }
        energized = energized.DistinctBy(state => $"{state.Row},{state.Col}").ToList();
        return energized.Count.ToString();
    }

    private (int row, int col) GetNewPosition(State state, char direction, RectangleGrid<char> grid)
    {
        if (direction == '>' && state.Col < grid.Width - 1)
            return (state.Row, state.Col + 1);
        if (direction == '<' && state.Col > 0)
            return (state.Row, state.Col - 1);
        if (direction == 'v' && state.Row < grid.Height - 1)
            return (state.Row + 1, state.Col);
        if (direction == '^' && state.Row > 0)
            return (state.Row - 1, state.Col);
        throw new ArgumentException($"Direction not allowed: {direction}");
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
        throw new NotImplementedException();
    }
}
