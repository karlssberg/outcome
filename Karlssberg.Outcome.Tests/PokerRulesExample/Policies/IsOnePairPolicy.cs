using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsOnePairPolicy : IPolicy<Hand, WinningHand>
{
    private readonly IPolicy<Hand, WinningHand> _policy;
    public IsOnePairPolicy()
    {
        _policy = new Policy<Hand, WinningHand>(IsOnePair)
        {
            TrueOutcome = WinningHand.OnePair,
            FalseOutcome = WinningHand.NoWin
        };
    }

    private static bool IsOnePair(Hand hand)
    {
        var pairCount = hand.Cards
            .GroupBy(card => card.Rank)
            .Count(IsAPair);
        
        return pairCount == 1;
    }
    
    private static bool IsAPair(IEnumerable<Card> group) => group.Count() == 2;

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);
}