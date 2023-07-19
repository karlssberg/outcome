namespace Karlssberg.Outcome.Results;

/// <summary>
///     A Cause is a record of the outcome of a policy or spec, and the underlying causes of that outcome.
/// </summary>
/// <param name="Outcome">
///     the non-boolean result of the policy or spec.
/// </param>
/// <param name="UnderlyingCauses">
///     the ecapsulating non-boolean results of the predicates used to arrive at the current
///     <paramref name="Outcome" />.
/// </param>
/// <typeparam name="TOutcome">
///     the type outputted by policies and aggregated by specs.
/// </typeparam>
public record Cause<TOutcome>(TOutcome Outcome, IEnumerable<Cause<TOutcome>> UnderlyingCauses)
{
    /// <summary>
    ///     Serializes the <see cref="Outcome" /> to a string.
    /// </summary>
    /// <returns>
    ///     The <see cref="Outcome" /> serialized to a string.
    /// </returns>
    public override string ToString() => Outcome?.ToString() ?? "";
}