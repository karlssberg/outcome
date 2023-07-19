namespace Karlssberg.Outcome.Results;

internal record FirstSatisfiedWithMultipleOutcomes<TOutcome> : IBooleanWithMultipleOutcomes<TOutcome>
{
    private readonly IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> _operands;

    internal FirstSatisfiedWithMultipleOutcomes(IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> operands)
    {
        ArgumentNullException.ThrowIfNull(operands, nameof(operands));
        _operands = operands;
        var firstSatisfiedResult = operands.FirstOrDefault(operand => operand.IsSatisfied);

        if (firstSatisfiedResult is not null)
        {
            IsSatisfied = true;
            CausalResults = Enumerable.Empty<IBooleanWithMultipleOutcomes<TOutcome>>().Append(firstSatisfiedResult);
            Outcomes = CausalResults.SelectMany(operand => operand.Outcomes);
            return;
        }

        IsSatisfied = false;
        CausalResults = operands;
        Outcomes = CausalResults.SelectMany(operand => operand.Outcomes);
    }

    public bool IsSatisfied { get; }
    public IEnumerable<TOutcome> Outcomes { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }

    /// <inheritdoc cref="object.ToString"/>.
    public override string ToString() => $"switch({string.Join(",", _operands)})";
}