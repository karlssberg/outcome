using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     A specification that negates the result of another specification.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcomes that this spec will produce.
/// </typeparam>
internal class NotSpec<TModel, TOutcome> : ISpec<TModel, TOutcome>
{
    private readonly ISpec<TModel, TOutcome> _operand;

    /// <summary>
    ///     Creates a new NotSpec from another spec.
    /// </summary>
    /// <param name="operand">
    ///     The operand to negate
    /// </param>
    internal NotSpec(ISpec<TModel, TOutcome> operand)
    {
        ArgumentNullException.ThrowIfNull(operand, nameof(operand));
        
        _operand = operand;
    }

    /// <summary>
    ///     Evaluates the spec against the given model.
    /// </summary>
    /// <param name="model">
    ///     The model.
    /// </param>
    /// <returns>
    ///     The result of the evaluation.
    /// </returns>
    public IBooleanWithMultipleOutcomes<TOutcome> Evaluate(TModel model)
    {
        var result = _operand.Evaluate(model);
        return new NotWithMultipleOutcomes<TOutcome>(result);
    }

    /// <summary>
    ///     Returns a string representation of the spec.
    /// </summary>
    /// <returns>
    ///     A string representation of the spec.
    /// </returns>
    public override string ToString() => $"!({_operand})";
}