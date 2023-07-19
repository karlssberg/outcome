using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome;

/// <summary>
///     A spec is a boolean expression that evaluates to true or false. A spec is composed of policies and other specs and
///     can be combined using logical operators.  A spec can be evaluated against a model to produce a result that contains
///     the outcomes from the underlying causes of the boolean result.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
public interface ISpec<in TModel> : ISpec<TModel, string>
{
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
    static ISpec<TModel> operator &(ISpec<TModel> left, ISpec<TModel> right) =>
        left.And(right);

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
    static ISpec<TModel> operator &(ISpec<TModel, string> left, ISpec<TModel> right) =>
        left.And(right);

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
    static ISpec<TModel> operator &(ISpec<TModel> left, ISpec<TModel, string> right) =>
        left.And(right);

    /// <summary>
    ///     Returns a new spec that is the logical OR of this spec and the given spec.
    /// </summary>
    /// <param name="left">
    ///   The left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///   The right operand of the OR operation.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical OR of this spec and the given spec.
    /// </returns>
    static ISpec<TModel> operator |(ISpec<TModel> left, ISpec<TModel> right) =>
        left.Or(right);

    /// <summary>
    ///     Returns a new spec that is the logical OR of this spec and the given spec.
    /// </summary>
    /// <param name="left">
    ///   The left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///   The right operand of the OR operation.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical OR of this spec and the given spec.
    /// </returns>
    static ISpec<TModel> operator |(ISpec<TModel, string> left, ISpec<TModel> right) =>
        left.Or(right);

    /// <summary>
    ///     Returns a new spec that is the logical OR of this spec and the given spec.
    /// </summary>
    /// <param name="left">
    ///   The left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///   The right operand of the OR operation.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical OR of this spec and the given spec.
    /// </returns>
    static ISpec<TModel> operator |(ISpec<TModel> left, ISpec<TModel, string> right) =>
        left.Or(right);

    /// <summary>
    ///     Returns a new spec that is the logical NOT of this spec.
    /// </summary>
    /// <param name="spec">
    ///     The spec to NOT.
    /// </param>
    /// <returns>
    ///     A new spec that is the logical NOT of this spec.
    /// </returns>
    static ISpec<TModel> operator !(ISpec<TModel> spec) =>
        spec.Not();

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
    static ISpec<TModel> operator ^(ISpec<TModel> left, ISpec<TModel> right)
        => left.XOr(right);

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
    static ISpec<TModel> operator ^(ISpec<TModel, string> left, ISpec<TModel> right)
        => left.XOr(right);

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
    static ISpec<TModel> operator ^(ISpec<TModel> left, ISpec<TModel, string> right)
        => left.XOr(right);
}