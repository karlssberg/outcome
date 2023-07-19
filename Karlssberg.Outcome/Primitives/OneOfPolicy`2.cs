using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a policy that evaluates to true if exactly one of its child policies evaluates to true.
/// </summary>
/// <typeparam name="TModel">
///     The model type that is to be evaluated.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of outcome produced by the policy.
/// </typeparam>
public class OneOfPolicy<TModel, TOutcome> : IPolicy<TModel, TOutcome>
{
    private readonly IEnumerable<IPolicy<TModel, TOutcome>> _policies;
    private readonly TOutcome _falseOutcome;

    /// <summary>
    ///     Creates a new one-of spec from a collection of specs.
    /// </summary>
    /// <param name="policies">
    ///     The spec collection.
    /// </param>
    public OneOfPolicy(IEnumerable<IPolicy<TModel, TOutcome>> policies, TOutcome falseOutcome)
    {
        ArgumentNullException.ThrowIfNull(policies, nameof(policies));
        _policies = policies;
        _falseOutcome = falseOutcome;
    }

    /// <inheritdoc/>
    public IBooleanWithSingleOutcome<TOutcome> Evaluate(TModel model)
    {
        var policyResults = _policies.Select(policy => policy.Evaluate(model));
        return new OneOfWithSingleOutcome<TOutcome>(policyResults, _falseOutcome);
    }

    /// <inheritdoc cref="object.ToString"/>.
    public override string ToString() => $"oneOf({string.Join(", ", _policies)})";
}