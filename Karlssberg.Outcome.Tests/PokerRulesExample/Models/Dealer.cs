namespace Karlssberg.Outcome.Tests.PokerRulesExample.Models;

public class Dealer
{
    public const int HandSize = 5;
    private readonly IComparer<Card> _shuffler = new Shuffler<Card>();
    private Stack<Card> _deck = new();

    public Dealer()
    {
        RefreshDeck();
        Shuffle();
    }

    public IEnumerable<Card> Deck => _deck;

    public Dealer Shuffle()
    {
        _deck = new Stack<Card>(_deck.Order(_shuffler));
        return this;
    }

    public Hand DealHand()
    {
        if (_deck.Count <= HandSize)
            RefreshDeck();

        var cards = Enumerable
            .Range(0, HandSize)
            .Select(_ => DrawCard());

        return new Hand(cards);
    }

    public void RefreshDeck()
    {
        IEnumerable<Card> GenerateDeck()
        {
            return
                from suit in Enum.GetValues<Suit>()
                from rank in Enum.GetValues<Rank>()
                select new Card(rank, suit);
        }

        _deck = new Stack<Card>(GenerateDeck());
    }

    public Card DrawCard() => _deck.Pop();

    public Card DrawCard(Func<Card, bool> predicate)
    {
        var chosenCard = _deck.First(predicate);

        _deck = new Stack<Card>(_deck.Where(card => card != chosenCard));

        return chosenCard;
    }

    public ISet<Card> DrawCards(int quantity = 5) => Enumerable
        .Range(0, quantity)
        .Select(_ => DrawCard())
        .ToHashSet();

    public ISet<Card> DrawCards(int quantity, Func<Card, bool> predicate) => Enumerable
        .Range(0, quantity)
        .Select(i => DrawCard(predicate))
        .ToHashSet();
}