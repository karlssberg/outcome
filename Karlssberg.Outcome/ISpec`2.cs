using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome;

/// <summary>
///     A spec is a boolean expression that evaluates to true or false. A spec is composed of policies and other specs and
///     can be combined using logical operators.  A spec can be evaluated against a model to produce a result that contains
///     the outcomes from the underlying causes of the boolean result.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcome that this spec will produce.
/// </typeparam>
public interface ISpec<in TModel, TOutcome>
{
    /// <summary>
    ///     Returns a <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> that is the logical evaluation of this spec against the given model.
    ///     The result will contain the outcomes from the underlying causes of the boolean result.
    /// </summary>
    /// <param name="model">
    ///     The model to evaluate the spec against.
    /// </param>
    /// <returns>
    ///     The result of the evaluation containing the outcomes from the underlying causes of the boolean result.
    /// </returns>
    IBooleanWithMultipleOutcomes<TOutcome> Evaluate(TModel model);

    /// <summary>
    ///     Returns a new spec that is the logical AND of this spec and the given spec.
    /// </summary>
    /// <param name="left">
    ///     The left operand of the AND operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the AND operation.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical AND of this spec and the given spec.
    /// </returns>
    static ISpec<TModel, TOutcome> operator &(ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right)
        => left.And(right);

    /// <summary>
    ///     Returns a new spec that is the logical OR of this spec and the given spec.
    /// </summary>
    /// <param name="left">
    ///  The left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///   The right operand of the OR operation.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical OR of this spec and the given spec.
    ///     /returns>
    static ISpec<TModel, TOutcome> operator |(ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right)
        => left.Or(right);

    /// <summary>
    ///     Returns a new spec that is the logical NOT of this spec.
    /// </summary>
    /// <param name="spec">
    ///     The spec to NOT.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical NOT of this spec.
    /// </returns>
    static ISpec<TModel, TOutcome> operator !(ISpec<TModel, TOutcome> spec)
        => spec.Not();

    /// <summary>
    ///    Returns a new spec that is the logical XOR of this spec and the given spec.
    /// </summary>
    /// <param name="left">
    ///     The left operand of the XOR operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the XOR operation.
    /// </param>
    /// <returns></returns>
    static ISpec<TModel, TOutcome> operator ^(ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right)
        => left.XOr(right);
}