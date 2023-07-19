using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Provides a way to combine multiple policies into a single policy that returns a boolean outcome
///     that is true if the first satisfied policy is true. This can be useful in scenarios where you need to
///     evaluate multiple conditions and take action based on the first condition that is true.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this policy will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcomes that this policy will produce.
/// </typeparam>
internal class FirstSatisfiedPolicy<TModel, TOutcome> : IPolicy<TModel, TOutcome>
{
    private readonly TOutcome _defaultOutcome;
    private readonly IEnumerable<IPolicy<TModel, TOutcome>> _policies;

    /// <summary>
    ///     Creates a new SwitchPolicy from two policies.
    /// </summary>
    /// <param name="policies">
    ///     The policies.
    /// </param>
    /// <param name="defaultOutcome">
    ///     The default outcome.
    /// </param>
    internal FirstSatisfiedPolicy(IEnumerable<IPolicy<TModel, TOutcome>> policies, TOutcome defaultOutcome)
    {
        ArgumentNullException.ThrowIfNull(policies, nameof(policies));

        _policies = policies;
        _defaultOutcome = defaultOutcome;
    }

    /// <summary>
    ///     Evaluates the policy against the given model.
    /// </summary>
    /// <param name="model">
    ///     The model.
    /// </param>
    /// <returns>
    ///     The result of the evaluation.
    /// </returns>
    public IBooleanWithSingleOutcome<TOutcome> Evaluate(TModel model)
    {
        var results = _policies
            .Select(policy => policy.Evaluate(model));

        return new FirstSatisifiedWithSingleOutcome<TOutcome>(results, _defaultOutcome);
    }

    /// <summary>
    ///     Returns a string representation of the policy.
    /// </summary>
    /// <returns>
    ///     A string representation of the policy.
    /// </returns>
    public override string ToString()
    {
        var serializedPolicies = string.Join(", ", _policies);
        return $"switch({serializedPolicies})";
    }
}