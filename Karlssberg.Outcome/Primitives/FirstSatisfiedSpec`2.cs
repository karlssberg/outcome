using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     A switch is a collection of specs that are evaluated in order.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of the outcomes that this spec will produce.
/// </typeparam>
internal class FirstSatisfiedSpec<TModel, TOutcome> : ISpec<TModel, TOutcome>
{
    private readonly IEnumerable<ISpec<TModel, TOutcome>> _specs;

    /// <summary>
    ///     Returns a new switch spec from a collection of specs.
    /// </summary>
    /// <param name="specs">
    ///     The spec collection.
    /// </param>
    internal FirstSatisfiedSpec(IEnumerable<ISpec<TModel, TOutcome>> specs)
    {
        ArgumentNullException.ThrowIfNull(specs, nameof(specs));

        _specs = specs;
    }

    /// <summary>
    ///     Evaluates the spec against the given model.
    /// </summary>
    /// <param name="model">
    ///     The model.
    /// </param>
    /// <returns>
    ///     The result of the evaluation.
    /// </returns>
    public IBooleanWithMultipleOutcomes<TOutcome> Evaluate(TModel model)
    {
        var specResults = _specs.Select(spec => spec.Evaluate(model));

        return new FirstSatisfiedWithMultipleOutcomes<TOutcome>(specResults);
    }

    /// <summary>
    ///     Returns a string representation of the spec.
    /// </summary>
    /// <returns>
    ///     The string representation.
    /// </returns>
    public override string ToString()
    {
        var serializedSpecs = string.Join(", ", _specs);
        return $"switch({serializedSpecs})";
    }
}