using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public class IsFullHousePolicy : IPolicy<Hand, WinningHand>
{
    
    private readonly IPolicy<Hand, WinningHand> _policy;

    public IsFullHousePolicy()
    {
        var isThreeOfAKind = new IsThreeOfAKindPolicy().AsPolicy();
        var isOnePair = new IsOnePairPolicy().AsPolicy();
        
        _policy =  (isThreeOfAKind & isOnePair)
            .ToPolicy(WinningHand.FullHouse, WinningHand.NoWin);
    }
    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand item) => _policy.Evaluate(item);
}