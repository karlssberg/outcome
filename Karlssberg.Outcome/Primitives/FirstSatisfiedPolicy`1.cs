namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical switch operation with a collection of policies.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this policy will be evaluated against.
/// </typeparam>
internal class FirstSatisfiedPolicy<TModel> : FirstSatisfiedPolicy<TModel, string>, IPolicy<TModel>
{
    /// <summary>
    ///     Creates a new SwitchPolicy from a collection of policies.
    /// </summary>
    /// <param name="policies">
    ///     The policies.
    /// </param>
    /// <param name="defaultOutcome">
    ///     The default outcome.
    /// </param>
    internal FirstSatisfiedPolicy(IEnumerable<IPolicy<TModel, string>> policies, string defaultOutcome)
        : base(policies, defaultOutcome)
    {
    }
}