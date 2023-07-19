using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome;

public static class FirstSatisfiedExtensions
{
    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that will <see cref="ISpec{TModel,TOutcome}.Evaluate" /> each
    ///     of the <paramref name="specs" /> and when it finds the first that satisfies its predicate it returns it.
    ///     Otherwise, it returns a new <see cref="ISpec{TModel,TOutcome}" /> instances that aggregates the results that did
    ///     not satisfy their predicates.
    /// </summary>
    /// <param name="specs">
    ///     the specs to linearly evaluate.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that when evaluated will choose the first
    ///     <see cref="ISpec{TModel,TOutcome}" /> that satisfies its predicate, otherwise it wraps the unsatisfied
    ///     <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects with a <see cref="SpecResult{TOutcome}" />.
    /// </returns>
    public static ISpec<TModel, TOutcome> FirstSatisfied<TModel, TOutcome>(this IEnumerable<ISpec<TModel, TOutcome>> specs) =>
        new FirstSatisfiedSpec<TModel, TOutcome>(specs);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that will <see cref="ISpec{TModel,TOutcome}.Evaluate" /> each
    ///     of the <paramref name="specs" /> and when it finds the first that satisfies its predicate it returns it.
    ///     Otherwise, it returns a new <see cref="ISpec{TModel,TOutcome}" /> instances that aggregates the results that did
    ///     not satisfy their predicates.
    /// </summary>
    /// <param name="specs">
    ///     the specs to linearly evaluate.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that when evaluated will choose the first
    ///     <see cref="ISpec{TModel,TOutcome}" /> that satisfies its predicate, otherwise it wraps the unsatisfied
    ///     <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects with a <see cref="SpecResult{TOutcome}" />.
    /// </returns>
    public static ISpec<TModel> FirstSatisfied<TModel>(this IEnumerable<ISpec<TModel, string>> specs) =>
        new FirstSatisfiedSpec<TModel>(specs);

    /// <summary>
    ///     Creates an <see cref="ISpec{TModel,TOutcome}" /> that will <see cref="ISpec{TModel,TOutcome}.Evaluate" /> each
    ///     of the <paramref name="specs" /> and when it finds the first that satisfies its predicate it returns it.
    ///     Otherwise, it returns a new <see cref="ISpec{TModel,TOutcome}" /> instances that aggregates the results that did
    ///     not satisfy their predicates.
    /// </summary>
    /// <param name="specs">
    ///     the specs to linearly evaluate.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="ISpec{TModel,TOutcome}" /> that when evaluated will choose the first
    ///     <see cref="ISpec{TModel,TOutcome}" /> that satisfies its predicate, otherwise it wraps the unsatisfied
    ///     <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects with a <see cref="SpecResult{TOutcome}" />.
    /// </returns>
    public static ISpec<TModel> FirstSatisfied<TModel>(this IEnumerable<ISpec<TModel>> specs) =>
        new FirstSatisfiedSpec<TModel>(specs);

    /// <summary>
    ///     Creates an <see cref="IPolicy{TModel}" /> that will generate a result for
    ///     each of the <paramref name="policies" /> and when it finds the first that satisfies its predicate it returns it.
    ///     Otherwise, it returns a new <see cref="IPolicy{TModel}" /> instances that aggregates the results that did
    ///     not satisfy their predicates, and supplies a <paramref name="falseOutcome" />.
    /// </summary>
    /// <param name="policies">
    ///     the specs to linearly evaluate.
    /// </param>
    /// <param name="falseBecause">
    ///     the reason why the result is false.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="IPolicy{TModel,TOutcome}" /> that when evaluated will choose the first
    ///     <see cref="IPolicy{TModel,TOutcome}" /> that satisfies its predicate, otherwise it wraps the unsatisfied
    ///     <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects with a <see cref="PolicyResult{TOutcome}" />.
    /// </returns>
    public static IPolicy<TModel, TOutcome> FirstSatisfied<TModel, TOutcome>(this IEnumerable<IPolicy<TModel, TOutcome>> policies, TOutcome falseOutcome) =>
        new FirstSatisfiedPolicy<TModel, TOutcome>(policies, falseOutcome);

    /// <summary>
    ///     Creates an <see cref="IPolicy{TModel}" /> that will generate a result for
    ///     each of the <paramref name="policies" /> and when it finds the first that satisfies its predicate it returns it.
    ///     Otherwise, it returns a new <see cref="IPolicy{TModel}" /> instances that aggregates the results that did
    ///     not satisfy their predicates,, and supplies a <paramref name="falseOutcome" />.
    /// </summary>
    /// <param name="policies">
    ///     the specs to linearly evaluate.
    /// </param>
    /// <param name="falseBecause">
    ///     the reason why the result is false.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="IPolicy{TModel}" /> that when evaluated will choose the first
    ///     <see cref="IPolicy{TModel}" /> that satisfies its predicate, otherwise it wraps the unsatisfied
    ///     reasons in a result using <paramref name="falseBecause" />
    /// </returns>
    public static IPolicy<TModel> FirstSatisfied<TModel>(this IEnumerable<IPolicy<TModel, string>> policies, string falseBecause) =>
        new FirstSatisfiedPolicy<TModel>(policies, falseBecause);

    /// <summary>
    ///     Creates an <see cref="IPolicy{TModel}" /> that will generate a result for
    ///     each of the <paramref name="policies" /> and when it finds the first that satisfies its predicate it returns it.
    ///     Otherwise, it returns a new <see cref="IPolicy{TModel}" /> instances that aggregates the results that did
    ///     not satisfy their predicates, and supplies a <paramref name="falseOutcome" />.
    /// </summary>
    /// <param name="policies">
    ///     the specs to linearly evaluate.
    /// </param>
    /// <param name="falseBecause">
    ///     the reason why the result is false.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <returns>
    ///     an <see cref="IPolicy{TModel}" /> that when evaluated will choose the first
    ///     <see cref="IPolicy{TModel}" /> that satisfies its predicate, otherwise it wraps the unsatisfied
    ///     reasons in a result using <paramref name="falseBecause" />
    /// </returns>
    public static IPolicy<TModel> FirstSatisfied<TModel>(this IEnumerable<IPolicy<TModel>> policies, string falseBecause) =>
        new FirstSatisfiedPolicy<TModel>(policies, falseBecause);
}
