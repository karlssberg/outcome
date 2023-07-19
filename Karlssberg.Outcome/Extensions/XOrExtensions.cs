using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome;

/// <summary>
///    Represents a specification that evaluates to true if exactly one of its child specifications evaluates to true.1
/// </summary>
public static class XOrExtensions
{
    /// <summary>
    ///    Creates a new one-of specification from a collection of specifications.
    /// </summary>
    /// <param name="left">
    ///   The left operand.
    /// </param>
    /// <param name="right">
    ///  The right operand.
    /// </param>
    /// <typeparam name="TModel">
    /// <typeparam name="TModel">
    ///    The model type that is to be evaluated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     The outcome type that is to be produced.
    /// </typeparam>
    /// <returns></returns>
    public static ISpec<TModel, TOutcome> XOr<TModel, TOutcome>(this ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right) =>
        new XOrSpec<TModel, TOutcome>(left, right);

    public static ISpec<TModel> XOr<TModel>(this ISpec<TModel, string> left, ISpec<TModel, string> right) =>
        new XOrSpec<TModel>(left, right);
}
