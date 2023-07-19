using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome.Results;

internal record AndWithMultipleOutcomes<TOutcome> : IBooleanWithMultipleOutcomes<TOutcome>
{
    private readonly IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> _operands;

    internal AndWithMultipleOutcomes(IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right)
        : this( Enumerable
            .Empty<IBooleanWithMultipleOutcomes<TOutcome>>()
            .Append(left)
            .Append(right))
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));
    }
    
    internal AndWithMultipleOutcomes(IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> operands)
    {
        ArgumentNullException.ThrowIfNull(operands, nameof(operands));
        _operands = operands;

        IsSatisfied = operands.All(operand => operand.IsSatisfied);
        CausalResults = operands.Where(operand => operand.IsSatisfied == IsSatisfied);
        Outcomes = CausalResults.SelectMany(operand => operand.Outcomes);
    }
    
    public bool IsSatisfied { get; }
    public IEnumerable<TOutcome> Outcomes { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }

    public override string ToString()
    {
        return string.Join(" & ", _operands.WrapInBrackets());
    }
}