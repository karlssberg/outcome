namespace Karlssberg.Outcome.Results;

internal class NotWithSingleOutcome<TOutcome> : IBooleanWithSingleOutcome<TOutcome>
{
    private readonly IBooleanWithSingleOutcome<TOutcome> _operand;

    internal NotWithSingleOutcome(IBooleanWithSingleOutcome<TOutcome> operand)
    {
        ArgumentNullException.ThrowIfNull(operand, nameof(operand));
        _operand = operand;

        IsSatisfied = !operand.IsSatisfied;
        CausalResults = operand.CausalResults;
        Outcome = operand.Outcome;
    }

    public bool IsSatisfied { get; }
    public TOutcome Outcome { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }
    
    /// <inheritdoc cref="object.ToString"/>.
    public override string ToString()
    {
        return $"!({_operand})";
    }
}