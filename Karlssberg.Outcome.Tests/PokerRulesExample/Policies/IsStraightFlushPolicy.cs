using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Policies.WinningHand;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsStraightFlushPolicy : IPolicy<Hand, WinningHand>
{
    private readonly IPolicy<Hand, WinningHand> _policy;
    public IsStraightFlushPolicy()
    {
        var isFlush = new IsFlushPolicy().AsPolicy();
        var isStraight = new IsStraightPolicy().AsPolicy();
        
        _policy = (isStraight & isFlush)
            .ToPolicy(StraightFlush, NoWin);
    }

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);
}