using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome.Results;

/// <summary>
///     A record of the outcome of a policy or spec, and the underlying causes of that outcome.
/// </summary>
/// <typeparam name="TOutcome"></typeparam>
public interface IBooleanWithMultipleOutcomes<TOutcome>
{
    /// <summary>
    ///     The outcome of the policy or spec.
    /// </summary>
    bool IsSatisfied { get; }

    /// <summary>
    ///     The <typeparamref name="TOutcome" /> from the underlying <see cref="IBooleanWithMultipleOutcomes{TOutcome}" />s that influenced the
    ///     final result.
    /// </summary>
    IEnumerable<TOutcome> Outcomes { get; }

    /// <summary>
    ///     The results of the operands that materially influenced the final result.
    /// </summary>
    IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }

    /// <summary>
    ///     The causes of this result.
    /// </summary>
    IEnumerable<Cause<TOutcome>> Causes => this.FindCauses();

    static IBooleanWithMultipleOutcomes<TOutcome> operator &(IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right) =>
        left.And(right);
    
    static IBooleanWithMultipleOutcomes<TOutcome> operator |(IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right) =>
        left.Or(right);
    
    static IBooleanWithMultipleOutcomes<TOutcome> operator !(IBooleanWithMultipleOutcomes<TOutcome> operand) =>
        operand.Not();
    
    static IBooleanWithMultipleOutcomes<TOutcome> operator ^(IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right) =>
        left.XOr(right);
}