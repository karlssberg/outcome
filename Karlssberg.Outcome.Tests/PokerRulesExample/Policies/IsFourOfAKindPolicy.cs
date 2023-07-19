using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Policies.WinningHand;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsFourOfAKindPolicy : IPolicy<Hand, WinningHand>
{
    private readonly IPolicy<Hand, WinningHand> _policy;

    public IsFourOfAKindPolicy()
    {
        _policy = new Policy<Hand, WinningHand>(IsFourOfAKind)
        {
            TrueOutcome = FourOfAKind,
            FalseOutcome = NoWin
        };
    }

    private static bool IsFourOfAKind(Hand hand)
    {
        return hand.Cards.GroupBy(card => card.Rank)
            .Any(group => group.Count() == 4);
    }

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);
}