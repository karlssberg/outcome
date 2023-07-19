using FluentAssertions;
using Karlssberg.Outcome.Tests.Fixtures;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using Karlssberg.Outcome.Tests.PokerRulesExample.Policies;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Policies.WinningHand;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Models.Rank;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Models.Suit;

namespace Karlssberg.Outcome.Tests.PokerRulesExample;

public class PokerGameTests
{
    private readonly Random _random = new();

    private TElement RandomInGroup<TKey, TElement>(IGrouping<TKey, TElement> grouping)
    {
        var randomIndex = _random.Next(0, grouping.Count());
        return grouping.ElementAt(randomIndex);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_a_flush(Dealer dealer, Suit suit, RulesPolicy sut)
    {
        var cards = dealer
            .Shuffle()
            .DealHand().Cards
            .Select(card => card with { Suit = suit })
            .ToList();

        var flushHand = new Hand(cards);

        var actual = sut.Evaluate(flushHand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(Flush);
    }


    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_a_straight(Random random, RulesPolicy sut)
    {
        var ranks = Enum.GetValues<Rank>().Order().ToList();
        var straightFullRank = ranks
            .Skip(random.Next(0, ranks.Count - 5))
            .Take(5)
            .ToList();

        var cards = straightFullRank.Select(
            (rank, i) =>
                new Card(rank, i % 2 == 0 ? Hearts : Clubs));

        var straightHand = new Hand(cards);

        var actual = sut.Evaluate(straightHand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(Straight);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_a_straight_flush(
        Dealer dealer,
        Suit suit,
        Random random,
        RulesPolicy sut)
    {
        var fullStraightFlush = dealer.Deck
            .GroupBy(card => card.Suit)
            .Where(grouping => grouping.Key == suit)
            .SelectMany(grouping => grouping)
            .OrderBy(card => card.Rank)
            .ToList();

        var randomStartIndex = random.Next(0, fullStraightFlush.Count - Dealer.HandSize);
        var cards = fullStraightFlush.GetRange(randomStartIndex, Dealer.HandSize);

        var fullStraightFlushHand = new Hand(cards);

        var actual = sut.Evaluate(fullStraightFlushHand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(StraightFlush);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_four_of_a_kind(Dealer dealer, Rank rank, RulesPolicy sut)
    {
        bool OfSameRank(Card card) => card.Rank == rank;
        var nOfAKindCards = dealer.DrawCards(4, OfSameRank);
        
        bool OfDifferentRank(Card card) => card.Rank != rank;
        var remainingCards = dealer.DrawCards(1, OfDifferentRank);
        var hand = new Hand(nOfAKindCards.Union(remainingCards));

        var actual = sut.Evaluate(hand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(FourOfAKind);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_three_of_a_kind(Dealer dealer, Rank rank, Suit suit, RulesPolicy sut)
    {
        bool OfSameRank(Card card) => card.Rank == rank;
        var nOfAKindCards = dealer.DrawCards(3, OfSameRank);
        
        bool OfDifferentRank(Card card) => card.Rank != rank && card.Suit == suit;
        var remainingCards = dealer.DrawCards(2, OfDifferentRank);
        var hand = new Hand(nOfAKindCards.Union(remainingCards));

        var actual = sut.Evaluate(hand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(ThreeOfAKind);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_one_pair_kind(Dealer dealer, Rank rank, RulesPolicy sut)
    {
        bool OfSameRank(Card card) => card.Rank == rank;
        var nOfAKindCards = dealer.DrawCards(2, OfSameRank);
        
        bool OfDifferentRank(Card card) => card.Rank != rank;
        var remainingCards = dealer.DrawCards(3, OfDifferentRank);
        var hand = new Hand(nOfAKindCards.Union(remainingCards));

        var actual = sut.Evaluate(hand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(OnePair);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_two_pair_kind(Dealer dealer, RulesPolicy sut)
    {
        var randomRanks = new Queue<Rank>(Enum.GetValues<Rank>().Order(new Shuffler<Rank>()));
        var firstRank = randomRanks.Dequeue();
        var secondRank = randomRanks.Dequeue();
        var thirdRank = randomRanks.Dequeue();
        
        bool OfFirstRank(Card card) => card.Rank == firstRank;
        bool OfSecondRank(Card card) => card.Rank == secondRank;
        bool OfThirdRank(Card card) => card.Rank == thirdRank;

        var firstPair = dealer.DrawCards(2, OfFirstRank);
        var secondPair = dealer.DrawCards(2, OfSecondRank);
        var lastCard = dealer.DrawCard(OfThirdRank);
        
        var hand = new Hand(firstPair.Concat(secondPair).Append(lastCard));

        var actual = sut.Evaluate(hand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(TwoPair);
    }
    
    [Theory]
    [InlineAutoData<SpecFixture>(Ace)]
    [InlineAutoData<SpecFixture>(Two)]
    [InlineAutoData<SpecFixture>(Three)]
    [InlineAutoData<SpecFixture>(Four)]
    [InlineAutoData<SpecFixture>(Five)]
    [InlineAutoData<SpecFixture>(Six)]
    [InlineAutoData<SpecFixture>(Seven)]
    [InlineAutoData<SpecFixture>(Eight)]
    [InlineAutoData<SpecFixture>(Nine)]
    [InlineAutoData<SpecFixture>(Ten)]
    [InlineAutoData<SpecFixture>(Jack)]
    [InlineAutoData<SpecFixture>(Queen)]
    [InlineAutoData<SpecFixture>(King)]
    public void Should_identify_full_house(Rank firstRank, Dealer dealer, RulesPolicy sut, Random random)
    {
        var ranks = Enum.GetValues<Rank>();
        var secondRank = ranks.Where(rank => rank != firstRank).Skip(random.Next(0, ranks.Length - 2)).First();
        bool OfSameRank(Card card)
        {
            return card.Rank == firstRank;
        }
        
        bool OfAlternativeSameRank(Card card)
        {
            return card.Rank == secondRank;
        }

        var nOfAKindCards = dealer.DrawCards(2, OfSameRank);
        var remainingCards = dealer.DrawCards(3, OfAlternativeSameRank);
        var hand = new Hand(nOfAKindCards.Union(remainingCards));

        var actual = sut.Evaluate(hand);

        actual.IsSatisfied.Should().BeTrue();
        actual.Outcome.Should().Be(FullHouse);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_identify_a_losing_hand(RulesPolicy sut)
    {
        var hand = new Hand(
            new Card(Five, Hearts),
            new Card(Two, Diamonds),
            new Card(Nine, Clubs),
            new Card(King, Hearts),
            new Card(Seven, Hearts));

        var actual = sut.Evaluate(hand);

        actual.IsSatisfied.Should().BeFalse();
        actual.Outcome.Should().Be(NoWin);
    }
}