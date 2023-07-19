using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Models.Rank;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsRoyalFlushPolicy : IPolicy<Hand, WinningHand>
{
    private static readonly Rank[] Royalty = { Ten, Jack, Queen, King, Ace };

    private readonly IPolicy<Hand, WinningHand> _policy =
        new Policy<Hand, WinningHand>(IsRoyalFlush)
        {
            TrueOutcome = WinningHand.RoyalFlush,
            FalseOutcome = WinningHand.NoWin
        };

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);

    private static bool IsRoyalFlush(Hand hand)
    {
        var isFlush = hand.Suits.Count == 1;
        var isRoyal = Royalty.All(rank => hand.Ranks.Contains(rank));

        return isRoyal && isFlush;
    }
}