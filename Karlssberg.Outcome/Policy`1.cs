using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome;

/// <summary>
///     A policy is a spec that produces a single outcome. It is the most basic building block of a spec.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that the policy evaluates.
/// </typeparam>
public sealed record Policy<TModel>(Func<TModel, bool> Predicate)
    : IPolicy<TModel>
{

    /// <summary>
    ///     The outcome of the policy when the predicate is true.
    /// </summary>
    public required string TrueBecause { get; init; }

    /// <summary>
    ///     The outcome of the policy when the predicate is false.
    /// </summary>
    public required string FalseBecause { get; init; }

    /// <summary>
    ///     Evaluates the policy against the given model and returns the result that contains the boolean result and the <see cref="string"/>.
    /// </summary>
    /// <param name="model">
    ///     The model to evaluate the policy against.
    /// </param>
    /// <returns>
    ///     The result of the evaluation that contains the boolean result and the <see cref="string"/>.
    /// </returns>
    public IBooleanWithSingleOutcome<string> Evaluate(TModel model)
    {
        var value = Predicate(model);
        var outcome = value
            ? TrueBecause
            : FalseBecause;

        return new BooleanWithSingleOutcome<string>(value, outcome);
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
    public static ISpec<TModel> operator &(
        Policy<TModel> left,
        Policy<TModel> right)
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
    public static ISpec<TModel> operator |(Policy<TModel> left, Policy<TModel> right)
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
    public static IPolicy<TModel> operator !(Policy<TModel> policy)
        => policy.Not();

    /// <summary>
    ///     Serializes the policy to a string.
    /// </summary>
    /// <returns>
    ///     The policy serialized to a string.
    /// </returns>
    public override string ToString() => $"{TrueBecause}";
}