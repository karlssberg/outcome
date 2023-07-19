using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome;

public static class NotExtensions
{
    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical NOT operation on the supplied
    ///     <paramref name="spec" /> operand.
    /// </summary>
    /// <param name="spec">
    ///     the <see cref="ISpec{TModel,TOutcome}" /> that is to have its result negated.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that (when <see cref="ISpec{TModel,TOutcome}.Evaluate" /> is invoked)
    ///     negates the result of its operand.
    /// </returns>
    public static ISpec<TModel, TOutcome> Not<TModel, TOutcome>(this ISpec<TModel, TOutcome> spec) =>
        new NotSpec<TModel, TOutcome>(spec);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical NOT operation on the supplied
    ///     <paramref name="spec" /> operand.
    /// </summary>
    /// <param name="spec">
    ///     the <see cref="ISpec{TModel,TOutcome}" /> that is to have its result negated.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that (when <see cref="ISpec{TModel,TOutcome}.Evaluate" /> is invoked)
    ///     negates the result of its operand.
    /// </returns>
    public static ISpec<TModel> Not<TModel>(this ISpec<TModel, string> spec) =>
        new NotSpec<TModel>(spec);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical NOT operation on the supplied
    ///     <paramref name="policy" /> operand.
    /// </summary>
    /// <param name="policy">
    ///     the <see cref="IPolicy{TModel,TOutcome}" /> that is to have its result negated.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="IPolicy{TModel,TOutcome}" /> that (when <see cref="IPolicy{TModel,TOutcome}.Evaluate" /> is invoked)
    ///     negates the result of its operand.
    /// </returns>
    public static IPolicy<TModel, TOutcome> Not<TModel, TOutcome>(this IPolicy<TModel, TOutcome> policy) =>
        new NotPolicy<TModel, TOutcome>(policy);

    /// <summary>
    ///     Creates an <see cref="IPolicy{TModel}" /> that performs a logical NOT operation on the supplied
    ///     <paramref name="policy" /> operand.
    /// </summary>
    /// <param name="policy">
    ///     The <see cref="IPolicy{TModel,TOutcome}" /> that is to have its result negated.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="IPolicy{TModel}" /> that (when <see cref="IPolicy{TModel}.Evaluate" /> is invoked)
    ///     negates the result of its operand.
    /// </returns>
    public static IPolicy<TModel> Not<TModel>(this IPolicy<TModel, string> policy) =>
        new NotPolicy<TModel>(policy);
}
