using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical XOR operation between two <see cref="ISpec{TModel,TOutcome}" />.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcomes that this spec will produce.
/// </typeparam>
internal class XOrSpec<TModel, TOutcome> : ISpec<TModel, TOutcome>
{
    private readonly ISpec<TModel, TOutcome> _left;
    private readonly ISpec<TModel, TOutcome> _right;

    /// <summary>
    ///     Creates a new XOrSpec from two operands.
    /// </summary>
    /// <param name="left">
    ///     The left operand.
    /// </param>
    /// <param name="right">
    ///     The right operand.
    /// </param>
    internal XOrSpec(ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        _left = left;
        _right = right;
    }

    /// <summary>
    ///     Evaluates the spec against the given model.
    /// </summary>
    /// <param name="model">
    ///     The model to be evaluated.
    /// </param>
    /// <returns>
    ///     The result of the evaluation.
    /// </returns>
    /// <remarks>
    ///     When the result is true (when only one operand is true), only the <c>true</c> <typeparamref name="TOutcome" /> is
    ///     chosen as a cause of the result.  However, when the result is false (when both operands are true or both operands
    ///     are false), both <typeparamref name="TOutcome" />s are chosen as causes of the result.
    /// </remarks>
    public IBooleanWithMultipleOutcomes<TOutcome> Evaluate(TModel model)
    {
        var leftResult = _left.Evaluate(model);
        var rightResult = _right.Evaluate(model);

        return new XOrWithMultipleOutcomes<TOutcome>(leftResult, rightResult);
    }

    /// <summary>
    ///     Returns a string representation of the spec.
    /// </summary>
    /// <returns>
    ///     A string representation of the spec.
    /// </returns>
    public override string ToString() => $"({_left}) ^ ({_right})";
}