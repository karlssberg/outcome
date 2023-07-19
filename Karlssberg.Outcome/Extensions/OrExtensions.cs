using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome;

public static class OrExtensions
{
    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical OR operation using
    ///     <paramref name="left" /> and <paramref name="right" /> as its binary operands.
    /// </summary>
    /// <param name="left">
    ///     the left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///     the right operand of the OR operation.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that (when <see cref="IPolicy{TModel,TOutcome}.Evaluate" /> is invoked)
    ///     performs OR logic on the result of its operands, and collates the causes.
    /// </returns>
    public static ISpec<TModel, TOutcome> Or<TModel, TOutcome>(this ISpec<TModel, TOutcome> left, ISpec<TModel, TOutcome> right) =>
        new OrSpec<TModel, TOutcome>(left, right);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel}" /> that performs a logical OR operation using
    ///     <paramref name="left" /> and <paramref name="right" /> as its binary operands.
    /// </summary>
    /// <param name="left">
    ///     The left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the OR operation.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="ISpec{TModel}" /> that (when <see cref="IPolicy{TModel}.Evaluate" /> is invoked)
    ///     performs OR logic on the result of its operands, and collates the causes.
    /// </returns>
    public static ISpec<TModel> Or<TModel>(this ISpec<TModel, string> left, ISpec<TModel, string> right) =>
        new OrSpec<TModel>(left, right);
}
