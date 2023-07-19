namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a specification that evaluates to true if exactly one of its child specifications evaluates to true.
/// </summary>
/// <typeparam name="TModel">
///     The model type that is to be evaluated.
/// </typeparam>
public class OneOfSpec<TModel> : OneOfSpec<TModel, string>, ISpec<TModel>
{
    /// <summary>
    ///     Creates a new one-of specification from a collection of specifications.
    /// </summary>
    /// <param name="specs">
    ///     The specification collection.
    /// </param>
    public OneOfSpec(IEnumerable<ISpec<TModel, string>> specs)
        : base(specs)
    {
    }
}
