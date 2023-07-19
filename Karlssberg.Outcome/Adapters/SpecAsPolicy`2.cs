using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Adapters;

/// <summary>
///     Adapts an <see cref="ISpec{TModel, TOutcome}" /> to an <see cref="IPolicy{TModel, TOutcome}" /> by mapping the
///     outcome of the specification to a dichotomy of outcomes.
/// </summary>
/// <typeparam name="TModel">
///     The model type that is to be evaluated.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of outcome produced by the policy.
/// </typeparam>
internal class SpecAsPolicy<TModel, TOutcome> : IPolicy<TModel, TOutcome>
{
    private readonly ISpec<TModel, TOutcome> _spec;
    private readonly TOutcome _trueOutcome;
    private readonly TOutcome _falseOutcome;

    internal SpecAsPolicy(
        ISpec<TModel, TOutcome> spec,
        TOutcome trueOutcome,
        TOutcome falseOutcome)
    {
        _spec = spec;
        _trueOutcome = trueOutcome;
        _falseOutcome = falseOutcome;
    }

    /// <summary>
    ///    Returns a result that is an evaluation of <see cref="_spec" /> against the given model using <see cref="_trueOutcome" /> if the underlying predicate is <c>true</c> or <see cref="_falseOutcome" /> as the outcome.
    /// </summary>
    /// <param name="model">
    ///     The model to evaluate the policy against.
    /// </param>
    /// <returns>
    ///    
    /// </returns>
    public IBooleanWithSingleOutcome<TOutcome> Evaluate(TModel model)
    {
        var result = _spec.Evaluate(model);
        var outcome = result.IsSatisfied
            ? _trueOutcome
            : _falseOutcome;

        return new BooleanWithSingleOutcome<TOutcome>(result.IsSatisfied, outcome)
        {
            CausalResults = Enumerable
                .Empty<IBooleanWithMultipleOutcomes<TOutcome>>()
                .Append(result)
        };
    }
}