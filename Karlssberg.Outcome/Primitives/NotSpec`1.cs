namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical NOT operation on a spec.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
internal sealed class NotSpec<TModel> : NotSpec<TModel, string>, ISpec<TModel>
{
    /// <summary>
    ///     Creates a new NotSpec from another spec.
    /// </summary>
    /// <param name="operand">
    ///     The operand to negate
    /// </param>
    internal NotSpec(ISpec<TModel, string> operand)
        : base(operand)
    {
    }
}