namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical switch operation with a collection of specs.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
internal class FirstSatisfiedSpec<TModel> : FirstSatisfiedSpec<TModel, string>, ISpec<TModel>
{
    /// <summary>
    ///     Returns a new switch spec from a collection of specs.
    /// </summary>
    /// <param name="specs">
    ///     The spec collection.
    /// </param>
    internal FirstSatisfiedSpec(IEnumerable<ISpec<TModel, string>> specs)
        : base(specs)
    {
    }
}