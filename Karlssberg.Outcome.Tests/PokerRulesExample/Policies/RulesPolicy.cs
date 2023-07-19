using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.PokerRulesExample.Models;
using static Karlssberg.Outcome.Tests.PokerRulesExample.Policies.WinningHand;

namespace Karlssberg.Outcome.Tests.PokerRulesExample.Policies;

public abstract class RulesPolicy : IPolicy<Hand, WinningHand>
{
    private readonly IPolicy<Hand, WinningHand> _policy;
    private readonly IPolicy<Hand, WinningHand> _isRoyalFlush = new IsRoyalFlushPolicy();
    private readonly IPolicy<Hand, WinningHand> _isStraight = new IsStraightPolicy();
    private readonly IPolicy<Hand, WinningHand> _isFlush = new IsFlushPolicy();
    private readonly IPolicy<Hand, WinningHand> _isFourOfAKind = new IsFourOfAKindPolicy();
    private readonly IPolicy<Hand, WinningHand> _isThreeOfAKind = new IsThreeOfAKindPolicy();
    private readonly IPolicy<Hand, WinningHand> _isOnePair = new IsOnePairPolicy();
    private readonly IPolicy<Hand, WinningHand> _isTwoPair = new IsTwoPairPolicy();
    private readonly IPolicy<Hand, WinningHand> _isStraightFlush = new IsStraightFlushPolicy();
    private readonly IPolicy<Hand, WinningHand> _isFullHouse = new IsFullHousePolicy();

    public RulesPolicy()
    {
        _policy = new[]
            {
                _isRoyalFlush,
                _isStraightFlush,
                _isFourOfAKind,
                _isFullHouse,
                _isFlush,
                _isStraight,
                _isThreeOfAKind,
                _isTwoPair,
                _isOnePair
            }
            .FirstSatisfied(NoWin);
    }

    public IBooleanWithSingleOutcome<WinningHand> Evaluate(Hand model)
    {
        return _policy.Evaluate(model);
    }
}