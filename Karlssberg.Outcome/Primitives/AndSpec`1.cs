namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical AND operation between two or more specs.
/// </summary>
/// <typeparam name="TModel"></typeparam>
internal sealed class AndSpec<TModel> : AndSpec<TModel, string>, ISpec<TModel>
{
    /// <summary>
    ///     Creates a new AndSpec from two operands.
    /// </summary>
    /// <param name="left">
    ///     The left operand of the AND operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the AND operation.
    /// </param>
    internal AndSpec(ISpec<TModel, string> left, ISpec<TModel, string> right)
        : base(left, right)
    {
    }

    /// <summary>
    ///     Creates a new AndSpec from a collection of operands.
    /// </summary>
    /// <param name="operands">
    ///     The operands.
    /// </param>
    internal AndSpec(IEnumerable<ISpec<TModel, string>> operands)
        : base(operands)
    {
    }
}