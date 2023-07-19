using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome;

public static class AndTogetherExtensions
{
    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that performs a logical AND operation on all the supplied
    ///     <paramref name="specs" /> operands.
    /// </summary>
    /// <param name="specs">
    ///     the specs to AND together.
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
    public static ISpec<TModel, TOutcome> AndTogether<TModel, TOutcome>(this IEnumerable<ISpec<TModel, TOutcome>> specs) =>
        new AndSpec<TModel, TOutcome>(specs);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel}" /> that performs a logical AND operation on all the supplied
    ///     <paramref name="specs" /> operands.
    /// </summary>
    /// <param name="specs">
    ///     The <see cref="ISpec{TModel}" /> instances to AND together.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="ISpec{TModel}" /> that (when <see cref="IPolicy{TModel}.Evaluate" /> is invoked)
    ///     performs AND logic on the result of its operands, and collates the causes.
    /// </returns>
    public static ISpec<TModel> AndTogether<TModel>(this IEnumerable<ISpec<TModel>> specs) =>
        new AndSpec<TModel>(specs);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel}" /> that performs a logical AND operation on all the supplied
    ///     <paramref name="specs" /> operands.
    /// </summary>
    /// <param name="specs">
    ///     The <see cref="ISpec{TModel}" /> instances to AND together.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="ISpec{TModel}" /> that (when <see cref="IPolicy{TModel}.Evaluate" /> is invoked)
    ///     performs AND logic on the result of its operands, and collates the causes.
    /// </returns>
    public static ISpec<TModel> AndTogether<TModel>(this IEnumerable<ISpec<TModel, string>> specs) =>
        new AndSpec<TModel>(specs);
}
