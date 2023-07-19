using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome;

/// <summary>
///     A policy is a spec that produces a single outcome. It is the most basic building block of a spec.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that the policy evaluates.
/// </typeparam>
public interface IPolicy<in TModel> : IPolicy<TModel, string>, ISpec<TModel>
{
    /// <summary>
    ///     Returns a new policy that is the logical AND of this policy and the given policy.
    /// </summary>
    /// <param name="policy">
    ///     The policy to AND with this policy.
    /// </param>
    /// <returns>
    ///     A new policy that is the logical AND of this policy and the given policy.
    /// </returns>
    static IPolicy<TModel> operator !(IPolicy<TModel> policy) =>
        policy.Not();
}