namespace Karlssberg.Outcome.Tests.PokerRulesExample.Models;

internal class Shuffler<T> : IComparer<T>
{
    private readonly Random _random = new();
    public int Compare(T? x, T? y) => _random.Next(-1, 1);
}