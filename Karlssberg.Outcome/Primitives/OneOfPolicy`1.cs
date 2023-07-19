namespace Karlssberg.Outcome.Primitives;

public class OneOfPolicy<TModel> : OneOfPolicy<TModel, string>, IPolicy<TModel>
{
    /// <summary>
    ///     Creates a new one-of spec from a collection of specs.
    /// </summary>
    /// <param name="policies">
    ///     The spec collection.
    /// </param>
    public OneOfPolicy(IEnumerable<IPolicy<TModel, string>> policies, string falseOutcome)
        : base(policies, falseOutcome)
    {
    }
}