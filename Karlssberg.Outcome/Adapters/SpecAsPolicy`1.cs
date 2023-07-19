namespace Karlssberg.Outcome.Adapters;

/// <summary>
///     Adapts an <see cref="ISpec{TModel, string}" /> to an <see cref="IPolicy{TModel}" /> by mapping the outcome of the
///     specification to a dichotomy of string outcomes.
/// </summary>
/// <typeparam name="TModel">
///     The model type that is to be evaluated.
/// </typeparam>
internal class SpecAsPolicy<TModel> : SpecAsPolicy<TModel, string>, IPolicy<TModel>
{
    public SpecAsPolicy(
        ISpec<TModel, string> spec,
        string trueOutcome,
        string falseOutcome)
        : base(spec, trueOutcome, falseOutcome)
    {
    }
}