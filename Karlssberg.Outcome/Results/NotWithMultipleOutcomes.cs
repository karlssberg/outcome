namespace Karlssberg.Outcome.Results;

internal class NotWithMultipleOutcomes<TOutcome> : IBooleanWithMultipleOutcomes<TOutcome>
{
    private readonly IBooleanWithMultipleOutcomes<TOutcome> _operand;

    internal NotWithMultipleOutcomes(IBooleanWithMultipleOutcomes<TOutcome> operand)
    {
        ArgumentNullException.ThrowIfNull(operand, nameof(operand));
        _operand = operand;

        IsSatisfied = !operand.IsSatisfied;
        CausalResults = operand.CausalResults;
        Outcomes = operand.Outcomes;
    }

    public bool IsSatisfied { get; }
    public IEnumerable<TOutcome> Outcomes { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }

    /// <inheritdoc cref="object.ToString"/>.
    override public string ToString()
    {
        return $"!({_operand})";
    }
}