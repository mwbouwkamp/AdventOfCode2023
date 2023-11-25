namespace AdventOfCode2023;

public class FileUtils
{
    private static readonly String rootFolder = @"C:\Users\Marco.Bouwkamp\Documents\High code\AdventOfCode2023\Resources\";
    private readonly string path;

    public FileUtils(String fileName)
    {
        path = rootFolder + fileName;
        if (!File.Exists(path))
            throw new FileNotFoundException($"File does not exist: {path}");
    }

    /// <summary>Gets lines.</summary>
    /// <returns>
    ///   List of lines.
    /// </returns>
    public List<String> GetLines()
    {
        return File.ReadLines(path).ToList();
    }

    /// <summary>Gets strings, using a delimiter.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   List of strings.
    /// </returns>
    public String GetString()
    {
        return File.ReadAllText(path);
    }

    /// <summary>Gets characters as string, using a delimiter.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   List of characters as string values.
    /// </returns>
    public List<String> GetCharactersAsString(String? delimiter = "")
    {
        return File.ReadAllText(path)
            .Split(delimiter)
            .ToList();
    }

    /// <summary>Gets characters as char, using a delimiter.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   List of characters as char values.
    /// </returns>
    public List<char> GetCharactersAsChar(String delimiter = "")
    {
        return GetParsedElements(delimiter, char.Parse);
    }

    /// <summary>Gets integers, using a delimiter.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   List of integers.
    /// </returns>
    public List<int> GetIntegers(String delimiter)
    {
        return GetParsedElements(delimiter, int.Parse);
    }

    /// <summary>Gets integers, using a delimiter.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   List of doubles.
    /// </returns>
    public List<double> GetDoubles(String delimiter)
    {
        return GetParsedElements(delimiter, double.Parse);
    }


    /// <summary>Gets parsed elements, using a delimiter.</summary>
    /// <typeparam name="T">The type to parse to</typeparam>
    /// <param name="delimiter">The delimiter.</param>
    /// <param name="parser">The parser.</param>
    /// <returns>
    ///   List of parsed elements of type T.
    /// </returns>
    private List<T> GetParsedElements<T>(String delimiter, Func<string, T> parser)
    {
        return File.ReadAllText(path)
            .Split(delimiter)
            .Select(element => parser(element))
            .ToList();
    }

    /// <summary>Gets character grid.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   Grid of type char.
    /// </returns>
    public RectangleGrid<char> GetCharGrid(String delimiter = "")
    {
        return new RectangleGrid<char>(Get2DList(delimiter, char.Parse), ' ');
    }

    /// <summary>Gets integer grid.</summary>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>
    ///   Grid of type integer.
    /// </returns>
    public RectangleGrid<int> GetIntGrid(String delimiter = "")
    {
        return new RectangleGrid<int>(Get2DList(delimiter, int.Parse), ' ');
    }

    /// <summary>Gets a list of lists of elmements.</summary>
    /// <typeparam name="T">The type to parse to</typeparam>
    /// <param name="delimiter">The delimiter.</param>
    /// <param name="parser">The parser.</param>
    /// <returns>
    ///   List of lists of elements of type T.
    /// </returns>
    private List<List<T>> Get2DList<T>(String delimiter, Func<string, T> parser)
    {
        return GetLines()
            .Select(line => line
                .Split(delimiter)
                .Select(parser)
                .ToList())
            .ToList();
    }
}
