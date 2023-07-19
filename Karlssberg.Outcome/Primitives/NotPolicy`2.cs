using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a policy that is the logical negation of another policy.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this policy will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcomes that this policy will produce.
/// </typeparam>
internal class NotPolicy<TModel, TOutcome> : IPolicy<TModel, TOutcome>
{
    private readonly IPolicy<TModel, TOutcome> _operand;

    /// <summary>
    ///     Creates a new NotPolicy from another policy.
    /// </summary>
    /// <param name="operand">
    ///     The operand to negate
    /// </param>
    internal NotPolicy(IPolicy<TModel, TOutcome> operand)
    {
        ArgumentNullException.ThrowIfNull(operand, nameof(operand));
        
        _operand = operand;
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
        var booleanOutcome = _operand.Evaluate(model);

        return !booleanOutcome;
    }

    /// <summary>
    ///     Returns a string representation of the policy.
    /// </summary>
    /// <returns>
    ///     A string representation of the policy.
    /// </returns>
    public override string ToString() => $"!({_operand})";

}