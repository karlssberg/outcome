namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical NOT operation on a spec.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
internal sealed class NotPolicy<TModel> : NotPolicy<TModel, string>, IPolicy<TModel>
{
    /// <summary>
    ///     Creates a new NotPolicy from another policy.
    /// </summary>
    /// <param name="operand">
    ///     The operand to negate
    /// </param>
    internal NotPolicy(IPolicy<TModel, string> operand)
        : base(operand)
    {
    }
}