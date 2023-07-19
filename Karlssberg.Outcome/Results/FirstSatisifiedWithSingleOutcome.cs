using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome.Results;

internal record FirstSatisifiedWithSingleOutcome<TOutcome> : IBooleanWithSingleOutcome<TOutcome>
{
    private readonly IEnumerable<IBooleanWithSingleOutcome<TOutcome>> _operands;
    private readonly TOutcome _falseOutcome;

    internal FirstSatisifiedWithSingleOutcome(IEnumerable<IBooleanWithSingleOutcome<TOutcome>> operands, TOutcome falseOutcome)
    {
        ArgumentNullException.ThrowIfNull(operands, nameof(operands));

        _operands = operands;
        _falseOutcome = falseOutcome;
        var firstSatisfiedResult = operands.FirstOrDefault(operand => operand.IsSatisfied);

        if (firstSatisfiedResult is not null)
        {
            IsSatisfied = true;
            CausalResults = Enumerable.Empty<IBooleanWithMultipleOutcomes<TOutcome>>().Append(firstSatisfiedResult);
            Outcome = firstSatisfiedResult.Outcome;
            return;
        }

        IsSatisfied = false;
        CausalResults = operands.Select(operand => operand);
        Outcome = _falseOutcome;
    }

    public bool IsSatisfied { get; }
    public TOutcome Outcome { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }

    /// <inheritdoc cref="object.ToString"/>.
    public override string ToString() => $"switch({string.Join(",", _operands)})";
}