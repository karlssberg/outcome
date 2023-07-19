using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome;

/// <summary>
///     A policy is a spec that produces a single outcome. It is the most basic building block of the specs
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that the policy evaluates.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcome that the policy produces.
/// </typeparam>
public sealed record Policy<TModel, TOutcome>(Func<TModel, bool> Predicate)
    : IPolicy<TModel, TOutcome>
{
    /// <summary>
    ///     The outcome of the policy when the predicate is true.
    /// </summary>
    public required TOutcome TrueOutcome { get; init; }

    /// <summary>
    ///     The outcome of the policy when the predicate is false.
    /// </summary>
    public required TOutcome FalseOutcome { get; init; }

    /// <summary>
    ///     Evaluates the policy against the given model and returns the result that contains the boolean result and the
    ///     <typeparamref name="TOutcome" />.
    /// </summary>
    /// <param name="model">
    ///     The model to evaluate the policy against.
    /// </param>
    /// <returns>
    ///     The result of the evaluation that contains the boolean result and the <typeparamref name="TOutcome" />.
    /// </returns>
    public IBooleanWithSingleOutcome<TOutcome> Evaluate(TModel model)
    {
        var value = Predicate(model);
        var outcome = value
            ? TrueOutcome
            : FalseOutcome;

        return new BooleanWithSingleOutcome<TOutcome>(value, outcome);
    }

    /// <summary>
    ///     Returns a <see cref="ISpec{TModel,TOutcome}" /> that is the logical AND of the left policy and the right policy.
    /// </summary>
    /// <param name="left">
    ///     The policy to AND with the right operand.
    /// </param>
    /// <param name="right">
    ///     The policy to AND with the left operand.
    /// </param>
    /// <returns>
    ///     A <see cref="ISpec{TModel,TOutcome}" /> that is the logical AND of left policy and the right policy.
    /// </returns>
    public static ISpec<TModel, TOutcome> operator &(Policy<TModel, TOutcome> left, Policy<TModel, TOutcome> right)
        => left.And(right);

    /// <summary>
    ///     Returns a <see cref="ISpec{TModel,TOutcome}" /> that is the logical OR of the left policy and the right policy.
    /// </summary>
    /// <param name="left">
    ///     The policy to OR with the right operand.
    /// </param>
    /// <param name="right">
    ///     The policy to OR with the left operand.
    /// </param>
    /// <returns>
    ///     A <see cref="ISpec{TModel,TOutcome}" /> that is the logical OR of left policy and the right policy.
    /// </returns>
    public static ISpec<TModel, TOutcome> operator |(Policy<TModel, TOutcome> left, Policy<TModel, TOutcome> right)
        => left.Or(right);

    /// <summary>
    ///     Returns a <see cref="ISpec{TModel,TOutcome}" /> that is the logical NOT of the given policy.
    /// </summary>
    /// <param name="policy">
    ///     The policy to NOT.
    /// </param>
    /// <returns>
    ///     A <see cref="ISpec{TModel,TOutcome}" /> that is the logical NOT of the given policy.
    /// </returns>
    public static IPolicy<TModel, TOutcome> operator !(Policy<TModel, TOutcome> policy)
        => policy.Not();


    /// <summary>
    ///     Serializes the policy to a string.
    /// </summary>
    /// <returns>
    ///     The policy serialized to a string.
    /// </returns>
    public override string ToString() => $"{TrueOutcome}";
}