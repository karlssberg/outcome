namespace Karlssberg.Outcome.Primitives;

/// <summary>
///     Represents a logical AND operation between two or more specs.
/// </summary>
/// <typeparam name="TModel">
///     The type of the model that this spec will be evaluated against.
/// </typeparam>
internal sealed class OrSpec<TModel> : OrSpec<TModel, string>, ISpec<TModel>
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
    internal OrSpec(ISpec<TModel, string> left, ISpec<TModel, string> right)
        : base(left, right)
    {
    }

    /// <summary>
    ///     Creates a new AndSpec from a collection of operands.
    /// </summary>
    /// <param name="operands">
    ///     The operands.
    /// </param>
    internal OrSpec(IEnumerable<ISpec<TModel, string>> operands)
        : base(operands)
    {
    }

    /// <summary>
    ///     Creates a new AndSpec from two operands.
    /// </summary>
    /// <param name="operands">
    ///     The operands.
    /// </param>
    internal OrSpec(IEnumerable<ISpec<TModel>> operands)
        : base(operands)
    {
    }
}