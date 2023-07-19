namespace Karlssberg.Outcome.Tests.PokerRulesExample.Models;

public record Hand(IEnumerable<Card> Cards)
{
    public Hand(params Card[] cards)
        : this(cards.AsEnumerable())
    {
    }
    
    public ISet<Rank> Ranks { get; } = new HashSet<Rank>(Cards.Select(card => card.Rank));
    public ISet<Suit> Suits { get; } = new HashSet<Suit>(Cards.Select(card => card.Suit));
}