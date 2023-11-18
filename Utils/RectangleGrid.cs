using System.Text;

namespace AdventOfCode2023;

public class RectangleGrid<T> : IGrid<T>
{
    private readonly Dictionary<Quadrant, List<List<T>>> quadrantDictionary;

    public RectangleGrid(List<List<T>> grid, T defaultValue = default(T), bool isInfinite = false) 
    {
        int firstLineCount = grid.FirstOrDefault()?.Count ?? 0;
        if (grid.Any(line => line.Count != firstLineCount)) 
            throw new Exception("Grid is not rectangular");

        this.quadrantDictionary = new();
        quadrantDictionary.Add(Quadrant.PP, grid);
        quadrantDictionary.Add(Quadrant.PM, new());
        quadrantDictionary.Add(Quadrant.MP, new());
        quadrantDictionary.Add(Quadrant.MM, new());

        XMin = 0;
        XMax = firstLineCount - 1;
        YMin = 0;
        YMax = grid.Count - 1;

        this.defaultValue = defaultValue;

        this.isInfinite = isInfinite;
    }

    /// <summary>Gets the element at a row and column.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The column.</param>
    /// <returns>
    ///   element at row and column.
    /// </returns>
    public override T? GetElement(int row, int col)
    {
        Quadrant quadrant = getQuadrant(row, col);
        int quadrantRow = Math.Abs(row);
        int quadrantCol = Math.Abs(col);
        return quadrant switch
        {
            Quadrant.PP => row <= YMax && col <= XMax
                ? quadrantDictionary[Quadrant.PP][quadrantRow][quadrantCol]
                : isInfinite ? defaultValue : throw new IndexOutOfRangeException($"Point is outside of grid: ({row},{col})"),
            Quadrant.MP => row >= YMin && col <= XMax
                ? quadrantDictionary[Quadrant.MP][quadrantRow][quadrantCol]
                : isInfinite ? defaultValue : throw new IndexOutOfRangeException($"Point is outside of grid: ({row},{col})"),
            Quadrant.PM => row <= YMax && col >= XMin
                ? quadrantDictionary[Quadrant.PM][quadrantRow][quadrantCol]
                : isInfinite ? defaultValue : throw new IndexOutOfRangeException($"Point is outside of grid: ({row},{col})"),
            Quadrant.MM => row >= YMin && col >= XMin
                ? quadrantDictionary[Quadrant.MM][quadrantRow][quadrantCol]
                : isInfinite ? defaultValue : throw new IndexOutOfRangeException($"Point is outside of grid: ({row},{col})"),
            _ => defaultValue,
        };
    }

    /// <summary>Sets the element at a row and column.</summary>
    /// <param name="row">The row.</param>
    /// <param name="col">The column.</param>
    /// <param name="value">The value to set.</param>
    public override void SetElement(int row, int col, T value)
    {
        Quadrant quadrant = getQuadrant(row, col);
        var canSetValue = quadrant switch
        {
            Quadrant.PP => (row <= YMax && col <= XMax),
            Quadrant.MP => (row >= YMax && col <= XMax),
            Quadrant.PM => row <= YMax && col >= XMax,
            Quadrant.MM => (row >= YMax && col >= XMax),
            _ => false,
        };
        if (canSetValue)
            quadrantDictionary[quadrant][Math.Abs(row)][Math.Abs(col)] = value;
        else if (isInfinite)
        {
            GrowGrid(row, col);
            quadrantDictionary[quadrant][Math.Abs(row)][Math.Abs(col)] = value;
        }
        else throw new IndexOutOfRangeException($"Point is outside of grid: ({row},{col})");

        SetMinMaxValues(row, col);
    }

    private void GrowGrid(int row, int col)
    {
        if (row > YMax || col > XMax)
        {
            GrowGrid(Quadrant.PP, Math.Max(row, YMax), Math.Max(col, XMax));
        }
        if (row > YMax || col < XMin)
        {
            GrowGrid(Quadrant.PM, Math.Max(row, YMax), Math.Min(col, XMin));
        }
        if (row < YMin || col > XMax)
        {
            GrowGrid(Quadrant.MP, Math.Min(row, YMin), Math.Max(col, XMax));
        }
        if (row < YMin || col < XMin)
        {
            GrowGrid(Quadrant.MM, Math.Min(row, YMin), Math.Min(col, XMin));
        }
    }
    private void GrowGrid(Quadrant quadrant, int row, int col)
    {
        List<List<T>> quadrantGrid = quadrantDictionary[quadrant];

        int currentCols = quadrantGrid.FirstOrDefault()?.Count ?? 0;
        int additionalCols = Math.Max(Math.Abs(col) - currentCols + 1, 0);
        int additionalRows = Math.Max(Math.Abs(row) - quadrantGrid.Count + 1, 0);

        List<List<T>> newQuadrantGrid = quadrantGrid
            .Select(row => row.Concat(Enumerable.Repeat(defaultValue, additionalCols)).ToList())
            .ToList();
        newQuadrantGrid.
            AddRange(Enumerable.Repeat(0, additionalRows)
                .Select(_ => new List<T>(Enumerable.Repeat(defaultValue, currentCols + additionalCols)))
                .ToList());

        quadrantDictionary[quadrant] = newQuadrantGrid;
    }

    private Quadrant getQuadrant(int row, int col)
    {
        if (row >= 0 && col >= 0)
            return Quadrant.PP;

        if (row < 0 && col >= 0)
            return Quadrant.MP;

        if (row >= 0 && col < 0)
            return Quadrant.PM;

        if (row < 0 && col < 0)
            return Quadrant.MM;

        throw new Exception("Quadrant could not be determined");
    }

    /// <summary>
    /// MM | MP
    /// ---+---
    /// PM | PP
    /// </summary>
    private enum Quadrant
    {
        PP, PM, MP, MM
    }
    public override string ToString()
    {
        List<string> rowsAsStringsPP = quadrantDictionary[Quadrant.PP].Select(row => ListToString(row, suffix: "\n")).ToList();
        if (XMin >= 0 && YMin >= 0)
            return ListToString(rowsAsStringsPP);

        List<string> rowsAsStringsPM = quadrantDictionary[Quadrant.PM]
            .Select(row => new String(ListToString(row).Reverse().ToArray()))
            .Select(rowAsString => rowAsString.Substring(0, rowAsString.Length - 1))
            .ToList();

        List<string> rowsAsStringsMP = quadrantDictionary[Quadrant.MP].Select(row => ListToString(row)).ToList();
        rowsAsStringsMP.RemoveAt(0);
        rowsAsStringsMP.Reverse();

        List<string> rowsAsStringsMM = quadrantDictionary[Quadrant.MM]
            .Select(row => new String(ListToString(row).Reverse().ToArray()))
            .Select(rowAsString => rowAsString.Substring(0, rowAsString.Length - 1))
            .ToList();
        rowsAsStringsMM.RemoveAt(0);
        rowsAsStringsMM.Reverse();

        List<string> minRows = rowsAsStringsMM.Select((row, index) => row + rowsAsStringsMP[index] + "\n").ToList();
        List<string> plusRows = rowsAsStringsPM.Select((row, index) => row + rowsAsStringsPP[index]).ToList();

        return ListToString(minRows.Concat(plusRows).ToList());
    }

    /// <summary>Lists to string, concatenating all values and adding optional prefix and suffix.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <param name="prefix">The prefix.</param>
    /// <param name="suffix">The suffix.</param>
    /// <returns>
    ///   String concatenating all values in the list and adding optional prefix and suffix.
    /// </returns>
    private string ListToString<T>(List<T> list, string prefix = "", string suffix = "")
    {
        return prefix + list.Aggregate(new StringBuilder(), (a, b) => a.Append(b)) + suffix;
    }
}
