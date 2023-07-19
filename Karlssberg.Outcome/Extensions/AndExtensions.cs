using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome;

public static class AndExtensions
{


    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical AND operation using
    ///     <paramref name="left" /> and <paramref name="right" /> as its binary operands.
    /// </summary>
    /// <param name="left">
    ///     the left operand of the AND operation.
    /// </param>
    /// <param name="right">
    ///     the right operand of the AND operation.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that (when <see cref="IPolicy{TModel,TOutcome}.Evaluate" /> is invoked)
    ///     performs AND logic on the result of its operands, and collates the causes.
    /// </returns>
    public static ISpec<TModel, TOutcome> And<TModel, TOutcome>(this ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right) =>
        new AndSpec<TModel, TOutcome>(left, right);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical AND operation using
    ///     <paramref name="left" /> and <paramref name="right" /> as its binary operands.
    /// </summary>
    /// <param name="left">
    ///     the left operand of the AND operation.
    /// </param>
    /// <param name="right">
    ///     the right operand of the AND operation.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that (when <see cref="IPolicy{TModel,TOutcome}.Evaluate" /> is invoked)
    ///     performs AND logic on the result of its operands, and collates the causes.
    /// </returns>
    public static ISpec<TModel> And<TModel>(this ISpec<TModel, string> left, ISpec<TModel, string> right) =>
        new AndSpec<TModel>(left, right);
}
