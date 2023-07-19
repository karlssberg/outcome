using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome.Results;

/// <summary>
///     A policy result is a spec result that has a single outcome.
/// </summary>
/// <typeparam name="TOutcome">
///     The type of the outcome of the policy.
/// </typeparam>
public interface IBooleanWithSingleOutcome<TOutcome> : IBooleanWithMultipleOutcomes<TOutcome>
{
    /// <summary>
    ///     The single outcome arrived at after evaluating the policy.
    /// </summary>
    TOutcome Outcome { get; }


    IEnumerable<TOutcome> IBooleanWithMultipleOutcomes<TOutcome>.Outcomes => Enumerable.Empty<TOutcome>().Append(Outcome);

    /// <summary>
    ///     The outcomes from the results of operand that materially influenced final result.
    /// </summary>
    IEnumerable<TOutcome> SubOutcomes => CausalResults.SelectMany(result => result.Outcomes);

    /// <summary>
    ///     The hierarchy of causes and their the underlying sub-causes.  It recursively walks the tree flattening it into
    ///     tiers of causes that were created by policy results.
    /// </summary>
    new IEnumerable<Cause<TOutcome>> Causes => this.FindCauses();
    
    static IBooleanWithSingleOutcome<TOutcome> operator !(IBooleanWithSingleOutcome<TOutcome> operand) =>
        operand.Not();
}