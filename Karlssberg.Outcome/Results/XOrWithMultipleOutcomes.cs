namespace Karlssberg.Outcome.Results;

internal class XOrWithMultipleOutcomes<TOutcome> : IBooleanWithMultipleOutcomes<TOutcome>
{
    private readonly IBooleanWithMultipleOutcomes<TOutcome> _left;
    private readonly IBooleanWithMultipleOutcomes<TOutcome> _right;

    internal XOrWithMultipleOutcomes(IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));
        _left = left;
        _right = right;

        IsSatisfied = left.IsSatisfied ^ right.IsSatisfied;
        CausalResults = Enumerable
            .Empty<IBooleanWithMultipleOutcomes<TOutcome>>()
            .Append(left)
            .Append(right);

        Outcomes = CausalResults.SelectMany(operand => operand.Outcomes);
    }

    /// <summary>
    ///     The <see cref="bool" /> result of the operation.
    /// </summary>
    public bool IsSatisfied { get; }

    /// <summary>
    ///   The outcomes of the operation.
    /// </summary>
    public IEnumerable<TOutcome> Outcomes { get; }

    /// <summary>
    ///     The operand results that caused the result be in the state it is in.
    /// </summary>
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }

    /// <inheritdoc cref="object.ToString" />
    public override string ToString() => $"{_left} ^ {_right}";
}