using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome;

/// <summary>
///     A policy is a spec that produces a single outcome. It is the most basic building block of a spec.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that the policy evaluates.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcome that the policy produces.
/// </typeparam>
public interface IPolicy<in TModel, TOutcome> : ISpec<TModel, TOutcome>
{
    /// <summary>
    ///     Evaluates the policy against the given model.
    /// </summary>
    /// <param name="model">
    ///     The model to evaluate the policy against.
    /// </param>
    /// <returns>
    ///     The result of the evaluation.
    /// </returns>
    IBooleanWithMultipleOutcomes<TOutcome> ISpec<TModel, TOutcome>.Evaluate(TModel model) =>
        Evaluate(model);

    /// <summary>
    ///     Evaluates the policy against the given model.
    /// </summary>
    /// <param name="model">
    ///     The model to evaluate the policy against.
    /// </param>
    /// <returns>
    ///     The result of the evaluation.
    /// </returns>
    new IBooleanWithSingleOutcome<TOutcome> Evaluate(TModel model);

    /// <summary>
    ///     Returns a new policy that is the logical AND of this policy and the given policy.
    /// </summary>
    /// <param name="policy">
    ///     The policy to AND with this policy.
    /// </param>
    /// <returns>
    ///     A new policy that is the logical AND of this policy and the given policy.
    /// </returns>
    static IPolicy<TModel, TOutcome> operator !(IPolicy<TModel, TOutcome> policy) =>
        policy.Not();
}