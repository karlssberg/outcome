using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     The AndSpec is a composite spec that evaluates to true if all of its operands evaluate to true.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcomes that this spec will produce.
/// </typeparam>
internal class AndSpec<TModel, TOutcome> : ISpec<TModel, TOutcome>
{
    private readonly IEnumerable<ISpec<TModel, TOutcome>> _operands;

    /// <summary>
    ///     Creates a new AndSpec from two operands.
    /// </summary>
    /// <param name="left">
    ///     The left operand.
    /// </param>
    /// <param name="right">
    ///     The right operand.
    /// </param>
    internal AndSpec(ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right)
        : this(Enumerable
            .Empty<ISpec<TModel, TOutcome>>()
            .Append(left)
            .Append(right))
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));
    }

    /// <summary>
    ///     Creates a new AndSpec from a collection of operands.
    /// </summary>
    /// <param name="operands">
    ///     The operands.
    /// </param>
    internal AndSpec(IEnumerable<ISpec<TModel, TOutcome>> operands)
    {
        ArgumentNullException.ThrowIfNull(operands, nameof(operands));
        
        _operands = operands;
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
        var results = _operands.Select(spec => spec.Evaluate(model));

        return results.AndTogether();
    }

    /// <summary>
    ///     Returns a string representation of the spec.
    /// </summary>
    /// <returns>
    ///     A string representation of the spec.
    /// </returns>
    public override string ToString()
    {
        return string.Join(" & ", _operands.WrapInBrackets());
    }
}