namespace Karlssberg.Outcome.Extensions;

public static class SpecExtensions
{

    /// <summary>
    ///     Evaluates whether or not the <paramref name="model" /> satisfies the <paramref name="spec" />.
    /// </summary>
    /// <param name="spec">
    ///     the <see cref="ISpec{TModel,TOutcome}" /> used to evaluate the <paramref name="model" />.
    /// </param>
    /// <param name="model">
    ///     the <typeparamref name="TModel" /> that is to be evaluated.
    /// </param>
    /// <typeparam name="TModel">
    ///     the model type that is to be logically interrogated.
    /// </typeparam>
    /// <typeparam name="TOutcome">
    ///     the type outputted by policies and aggregated by specs.
    /// </typeparam>
    /// <returns>
    ///     <c>true</c> if the <paramref name="model" /> satisfies the <paramref name="spec" />; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsSatisfiedBy<TModel, TOutcome>(
        this ISpec<TModel, TOutcome> spec,
        TModel model) =>
            spec.Evaluate(model).IsSatisfied;
}