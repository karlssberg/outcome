namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical XOR operation between two <see cref="ISpec{TModel}" />.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
internal class XOrSpec<TModel> : XOrSpec<TModel, string>, ISpec<TModel>
{
    /// <summary>
    ///    Creates a new XOrSpec from two operands.
    /// </summary>
    /// <param name="left">
    ///    The left operand of the XOR operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the XOR operation.
    /// </param>
    public XOrSpec(ISpec<TModel, string> left, ISpec<TModel, string> right)
        : base(left, right)
    {
    }
}