namespace Karlssberg.Outcome.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Wraps each string in the supplied <paramref name="strings"/> collection in brackets.
    /// </summary>
    /// <param name="strings">The collection of strings to wrap in brackets.</param>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <returns>A new collection of strings, where each string in the original collection is wrapped in brackets.</returns>
    public static IEnumerable<string> WrapInBrackets<T>(this IEnumerable<T> strings)
    {
        return strings.Select(@string => $"({@string})");
    }
}