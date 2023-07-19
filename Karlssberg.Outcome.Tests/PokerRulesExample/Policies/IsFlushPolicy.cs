using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Policies.WinningHand;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsFlushPolicy : IPolicy<Hand, WinningHand>
{
    private readonly IPolicy<Hand, WinningHand> _policy =
        new Policy<Hand, WinningHand>(IsFlush)
        {
            TrueOutcome = Flush,
            FalseOutcome = NoWin
        };

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item)
        => _policy.Evaluate(item);

    private static bool IsFlush(Hand hand)
    {
        return hand.Cards
            .All(card => card.Suit == hand.Cards.Select(card => card.Suit).FirstOrDefault());
    }
}