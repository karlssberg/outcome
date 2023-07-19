using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a specification that evaluates to true if exactly one of its child specifications evaluates to true.
/// </summary>
/// <typeparam name="TModel">
///     The model type that is to be evaluated.
/// </typeparam>
/// <typeparam name="TOutcome">
///     The type of outcome produced by the specification.
/// </typeparam>
public class OneOfSpec<TModel, TOutcome> : ISpec<TModel, TOutcome>
{
    private readonly IEnumerable<ISpec<TModel, TOutcome>> _specs;

    /// <summary>
    ///     Creates a new one-of spec from a collection of specs.
    /// </summary>
    /// <param name="specs">
    ///     The spec collection.
    /// </param>
    public OneOfSpec(IEnumerable<ISpec<TModel, TOutcome>> specs)
    {
        ArgumentNullException.ThrowIfNull(specs, nameof(specs));
        _specs = specs;
    }

    /// <inheritdoc/>
    public IBooleanWithMultipleOutcomes<TOutcome> Evaluate(TModel model)
    {
        var specResults = _specs.Select(spec => spec.Evaluate(model));
        return new OneOfWithMultipleOutcomes<TOutcome>(specResults);
    }

    /// <inheritdoc cref="object.ToString"/>.
    public override string ToString() => $"oneOf({string.Join(", ", _specs)})";
}