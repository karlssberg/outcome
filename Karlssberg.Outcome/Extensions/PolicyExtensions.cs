using Karlssberg.Outcome.Adapters;
namespace Karlssberg.Outcome.Extensions;

/// <summary>
///     Contains extension methods for converting boolean functions and specifications into policies.
/// </summary>
public static class PolicyExtensions
{

    /// <summary>
    ///     Encapsulates a boolean function as an <see cref="IPolicy{TModel,TOutcome}" /> with the specified true and false outcomes.
    /// </summary>
    /// <param name="predicate">
    ///     The boolean function to encapsulate as an <see cref="IPolicy{TModel,TOutcome}" />.
    /// </param>
    /// <param name="trueOutcome">
    ///     The outcome to return when the <paramref name="predicate" /> returns <c>true</c>.
    /// </param>
    /// <param name="falseOutcome">
    ///     The outcome to return when the <paramref name="predicate" /> returns <c>false</c>.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     The type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="IPolicy{TModel,TOutcome}" /> that encapsulates the <paramref name="predicate" /> with the specified true and false outcomes.
    /// </returns>
    public static IPolicy<TModel, TOutcome> ToPolicy<TModel, TOutcome>(this Func<TModel, bool> predicate, TOutcome trueOutcome, TOutcome falseOutcome) =>
        new Policy<TModel, TOutcome>(predicate)
        {
            TrueOutcome = trueOutcome,
            FalseOutcome = falseOutcome
        };

    /// <summary>
    ///     Encapsulates an <see cref="ISpec{TModel,TOutcome}" /> as an <see cref="IPolicy{TModel,TOutcome}" /> with the specified true and false outcomes.
    /// </summary>
    /// <param name="spec">
    ///     The <see cref="ISpec{TModel,TOutcome}" /> that is to be converted into an <see cref="IPolicy{TModel,TOutcome}" />.
    /// </param>
    /// <param name="trueOutcome">
    ///     The outcome to return when the <paramref name="spec" /> evaluates to <c>true</c>.
    /// </param>
    /// <param name="falseOutcome">
    ///     The outcome to return when the <paramref name="spec" /> evaluates to <c>false</c>.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     The type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="IPolicy{TModel,TOutcome}" /> that encapsulates the <paramref name="spec" /> with the specified true and false outcomes.
    /// </returns>
    public static IPolicy<TModel, TOutcome> ToPolicy<TModel, TOutcome>(this ISpec<TModel, TOutcome> spec, TOutcome trueOutcome, TOutcome falseOutcome) =>
        new SpecAsPolicy<TModel, TOutcome>(spec, trueOutcome, falseOutcome);

    /// <summary>
    ///     Converts a vanilla <paramref name="predicate" /> into an <see cref="IPolicy{TModel,TOutcome}" />.
    /// </summary>
    /// <param name="predicate">
    ///     The boolean function to convert into an <see cref="IPolicy{TModel,TOutcome}" />.
    /// </param>
    /// <param name="trueBecause">
    ///     The reason or explanation for why the policy evaluates to <c>true</c>.
    /// </param>
    /// <param name="falseBecause">
    ///     The reason or explanation for why the policy evaluates to <c>false</c>.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="IPolicy{TModel,TOutcome}" /> that binds the <paramref name="predicate" /> boolean result with the
    ///     specified <paramref name="trueBecause" /> and <paramref name="falseBecause" /> explanations.
    /// </returns>
    public static IPolicy<TModel> ToPolicy<TModel>(this Func<TModel, bool> predicate, string trueBecause, string falseBecause) =>
        new Policy<TModel>(predicate)
        {
            TrueBecause = trueBecause,
            FalseBecause = falseBecause
        };

    /// <summary>
    ///     Encapsulates an <see cref="ISpec{TModel,TOutcome}" /> as an <see cref="IPolicy{TModel}" />.
    /// </summary>
    /// <param name="spec">
    ///     The <see cref="ISpec{TModel,TOutcome}" /> that is to be converted into an
    ///     <see cref="IPolicy{TModel}" />.
    /// </param>
    /// <param name="trueBecause">
    ///     The reason or explanation for why the policy evaluates to <c>true</c>.
    /// </param>
    /// <param name="falseBecause">
    ///     The reason or explanation for why the policy evaluates to <c>false</c>.
    /// </param>
    /// <typeparam name="TModel">
    ///     The model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     An <see cref="IPolicy{TModel}" /> that encapsulates the <paramref name="spec" />.
    /// </returns>
    public static IPolicy<TModel> ToPolicy<TModel>(this ISpec<TModel, string> spec, string trueBecause, string falseBecause) =>
        new SpecAsPolicy<TModel>(spec, trueBecause, falseBecause);

    public static IPolicy<TModel, TOutcome> AsPolicy<TModel, TOutcome>(this IPolicy<TModel, TOutcome> policy) =>
        policy;
    
    public static IPolicy<TModel> AsPolicy<TModel>(this IPolicy<TModel> policy) =>
        policy;
}
