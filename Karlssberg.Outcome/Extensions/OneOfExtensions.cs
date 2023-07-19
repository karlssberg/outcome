using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome;

public static class OneOfExtensions
{
    /// <summary>
    /// Creates an <see cref="ISpec{TModel,TOutcome}"/> that performs a logical XOR operation on all the supplied <paramref name="specs"/> operands.
    /// </summary>
    /// <param name="specs">The <see cref="ISpec{TModel,TOutcome}"/> instances to XOR together.</param>
    /// <typeparam name="TModel">The model type that is to be logically interrogated.</typeparam>
    /// <typeparam name="TOutcome">The type outputted by policies and aggregated by specs.</typeparam>
    /// <returns>An <see cref="ISpec{TModel,TOutcome}"/> that (when <see cref="ISpec{TModel,TOutcome}.Evaluate"/> is invoked) performs XOR logic on the result of its operands, and collates the causes.</returns>
    public static ISpec<TModel, TOutcome> OneOf<TModel, TOutcome>(this IEnumerable<ISpec<TModel, TOutcome>> specs) =>
        new OneOfSpec<TModel, TOutcome>(specs);

    /// <summary>
    /// Creates an <see cref="ISpec{TModel}"/> that performs a logical XOR operation on all the supplied <paramref name="specs"/> operands.
    /// </summary>
    /// <param name="specs">The <see cref="ISpec{TModel}"/> instances to XOR together.</param>
    /// <typeparam name="TModel">The model type that is to be logically interrogated.</typeparam>
    /// <returns>An <see cref="ISpec{TModel}"/> that (when <see cref="ISpec{TModel}.Evaluate"/> is invoked) performs XOR logic on the result of its operands, and collates the causes.</returns>
    public static ISpec<TModel> OneOf<TModel>(this IEnumerable<ISpec<TModel, string>> specs) =>
        new OneOfSpec<TModel>(specs);

    /// <summary>
    /// Creates an <see cref="ISpec{TModel}"/> that performs a logical XOR operation on all the supplied <paramref name="specs"/> operands.
    /// </summary>
    /// <param name="specs">The <see cref="ISpec{TModel}"/> instances to XOR together.</param>
    /// <typeparam name="TModel">The model type that is to be logically interrogated.</typeparam>
    /// <returns>An <see cref="ISpec{TModel}"/> that (when <see cref="ISpec{TModel}.Evaluate"/> is invoked) performs XOR logic on the result of its operands, and collates the causes.</returns>
    public static ISpec<TModel> OneOf<TModel>(this IEnumerable<ISpec<TModel>> specs) =>
        new OneOfSpec<TModel>(specs);

    /// <summary>
    /// Creates an <see cref="IPolicy{TModel,TOutcome}"/> that performs a logical XOR operation on all the supplied <paramref name="policies"/> operands.
    /// </summary>
    /// <param name="policies">The <see cref="IPolicy{TModel,TOutcome}"/> instances to XOR together.</param>
    /// <param name="falseOutcome">The outcome to return if none of the supplied <paramref name="policies"/> evaluate to true.</param>
    /// <typeparam name="TModel">The model type that is to be logically interrogated.</typeparam>
    /// <typeparam name="TOutcome">The type outputted by policies and aggregated by specs.</typeparam>
    /// <returns>An <see cref="IPolicy{TModel,TOutcome}"/> that (when <see cref="IPolicy{TModel,TOutcome}.Evaluate"/> is invoked) performs XOR logic on the result of its operands, and returns the supplied <paramref name="falseOutcome"/> if none of the operands evaluate to true.</returns>
    public static IPolicy<TModel, TOutcome> OneOf<TModel, TOutcome>(this IEnumerable<IPolicy<TModel, TOutcome>> policies, TOutcome falseOutcome) =>
        new OneOfPolicy<TModel, TOutcome>(policies, falseOutcome);

    /// <summary>
    /// Creates an <see cref="IPolicy{TModel}"/> that performs a logical XOR operation on all the supplied <paramref name="policies"/> operands.
    /// </summary>
    /// <param name="policies">The <see cref="IPolicy{TModel,TOutcome}"/> instances to XOR together.</param>
    /// <param name="falseBecause">The reason to return if none of the supplied <paramref name="policies"/> evaluate to true.</param>
    /// <typeparam name="TModel">The model type that is to be logically interrogated.</typeparam>
    /// <returns>An <see cref="IPolicy{TModel}"/> that (when <see cref="IPolicy{TModel}.Evaluate"/> is invoked) performs XOR logic on the result of its operands, and returns the supplied <paramref name="falseBecause"/> if none of the operands evaluate to true.</returns>
    public static IPolicy<TModel> OneOf<TModel>(this IEnumerable<IPolicy<TModel, string>> policies, string falseBecause) =>
        new OneOfPolicy<TModel>(policies, falseBecause);

    /// <summary>
    /// Creates an <see cref="IPolicy{TModel}"/> that performs a logical XOR operation on all the supplied <paramref name="policies"/> operands.
    /// </summary>
    /// <param name="policies">The <see cref="IPolicy{TModel,TOutcome}"/> instances to XOR together.</param>
    /// <param name="falseBecause">The reason to return if none of the supplied <paramref name="policies"/> evaluate to true.</param>
    /// <typeparam name="TModel">The model type that is to be logically interrogated.</typeparam>
    /// <returns>An <see cref="IPolicy{TModel}"/> that (when <see cref="IPolicy{TModel}.Evaluate"/> is invoked) performs XOR logic on the result of its operands, and returns the supplied <paramref name="falseBecause"/> if none of the operands evaluate to true.</returns>
    public static IPolicy<TModel> OneOf<TModel>(this IEnumerable<IPolicy<TModel>> policies, string falseBecause) =>
        new OneOfPolicy<TModel>(policies, falseBecause);
}
