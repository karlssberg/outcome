using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsThreeOfAKindPolicy : IPolicy<Hand, WinningHand>
{
    private readonly IPolicy<Hand, WinningHand> _policy;

    public IsThreeOfAKindPolicy()
    {
        _policy = new Policy<Hand, WinningHand>(IsThreeOfAKind)
        {
            TrueOutcome = WinningHand.ThreeOfAKind,
            FalseOutcome = WinningHand.NoWin
        };
    }

    private static bool IsThreeOfAKind(Hand hand)
    {
        return hand.Cards.GroupBy(card => card.Rank)
            .Any(group => group.Count() == 3);
    }

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);
}