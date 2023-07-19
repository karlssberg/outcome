using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Policies.WinningHand;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Models.Rank;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public record IsStraightPolicy : IPolicy<Hand, WinningHand>
{
    private const int HandSize = 5;

    private static readonly Rank[] StraightRanks =
        { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

    private static readonly Rank[] RoyaltyRanks =
        { Ten, Jack, Queen, King, Ace };

    private readonly IPolicy<Hand, WinningHand> _policy =
        new Policy<Hand, WinningHand>(IsNonRoyalStraight)
        {
            TrueOutcome = Straight,
            FalseOutcome = NoWin
        };

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);

    private static bool IsNonRoyalStraight(Hand hand)
    {
        var isFlush = hand.Suits.Count == 1;
        var isRoyal = RoyaltyRanks.All(rank => hand.Ranks.Contains(rank));
        var isStraight = IsSequential(hand);

        return (isStraight && !isRoyal) || (isStraight && !isFlush);
    }

    private static bool IsSequential(Hand hand)
    {
        var counter = 0;
        var potentialMatchingRankSequence = StraightRanks
            .SkipWhile(rank => !hand.Ranks.Contains(rank));

        foreach (var rank in potentialMatchingRankSequence)
        {
            if (!hand.Ranks.Contains(rank))
                return false;

            counter++;
            if (counter == HandSize)
                return true;
        }

        return false;
    }
}