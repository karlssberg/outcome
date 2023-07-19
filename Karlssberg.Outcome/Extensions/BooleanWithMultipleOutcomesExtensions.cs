using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Extensions;

public static class BooleanWithMultipleOutcomesExtensions
{
    /// <summary>
    ///     Finds the causes of a given <see cref="IBooleanWithMultipleOutcomes{TOutcome}" />.  This involves recursively
    ///     finding the underlying
    ///     causes that led to the current <see cref="IBooleanWithMultipleOutcomes{TOutcome}" />.  A new Cause is created for
    ///     each policy result
    ///     encountered and its underlying causes are flattened into a single collection.
    /// </summary>
    /// <param name="booleanMultipleOutcomes">
    ///     The <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> to find the causes of.
    /// </param>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" />.
    /// </typeparam>
    /// <returns>
    ///     The causes of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" />.
    /// </returns>
    internal static IEnumerable<Cause<TOutcome>> FindCauses<TOutcome>(this IBooleanWithMultipleOutcomes<TOutcome> booleanMultipleOutcomes)
    {
        var underlyingCauses = booleanMultipleOutcomes.CausalResults.SelectMany(FindCauses);

        if (booleanMultipleOutcomes is IBooleanWithSingleOutcome<TOutcome> policyResult)
        {
            yield return new Cause<TOutcome>(policyResult.Outcome, underlyingCauses);
            yield break;
        }

        foreach (var cause in underlyingCauses)
            yield return cause;
    }

    /// <summary>
    ///     Combines two <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects using a logical AND operation.
    /// </summary>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects.
    /// </typeparam>
    /// <param name="left">
    ///     The left operand of the AND operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the AND operation.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object representing the result of the AND operation.
    /// </returns>
    public static IBooleanWithMultipleOutcomes<TOutcome> And<TOutcome>(this IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right) =>
        new AndWithMultipleOutcomes<TOutcome>(left, right);


    /// <summary>
    ///     Combines multiple <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects using a logical AND operation.
    /// </summary>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects.
    /// </typeparam>
    /// <param name="operands">
    ///     The <see cref="IEnumerable{T}" /> of <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects to combine.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object representing the result of the AND operation.
    /// </returns>
    public static IBooleanWithMultipleOutcomes<TOutcome> AndTogether<TOutcome>(this IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> operands) =>
        new AndWithMultipleOutcomes<TOutcome>(operands);

    /// <summary>
    ///     Combines two <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects using a logical OR operation.
    /// </summary>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects.
    /// </typeparam>
    /// <param name="left">
    ///     The left operand of the OR operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the OR operation.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object representing the result of the OR operation.
    /// </returns>
    public static IBooleanWithMultipleOutcomes<TOutcome> Or<TOutcome>(this IBooleanWithMultipleOutcomes<TOutcome> left,
        IBooleanWithMultipleOutcomes<TOutcome> right) =>
        new OrWithMultipleOutcomes<TOutcome>(left, right);

    /// <summary>
    ///     Negates a <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object using a logical NOT operation.
    /// </summary>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object.
    /// </typeparam>
    /// <param name="spec">
    ///     The <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object to negate.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object representing the negated result.
    /// </returns>
    public static IBooleanWithMultipleOutcomes<TOutcome> Not<TOutcome>(this IBooleanWithMultipleOutcomes<TOutcome> spec) =>
        new NotWithMultipleOutcomes<TOutcome>(spec);

    /// <summary>
    ///     Combines multiple <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects using a logical OR operation.
    /// </summary>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects.
    /// </typeparam>
    /// <param name="operands">
    ///     The <see cref="IEnumerable{T}" /> of <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects to combine.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object representing the result of the OR operation.
    /// </returns>
    public static IBooleanWithMultipleOutcomes<TOutcome> OrTogether<TOutcome>(this IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> operands) =>
        new OrWithMultipleOutcomes<TOutcome>(operands);

    /// <summary>
    ///     Combines two <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects using a logical XOR operation.
    /// </summary>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> objects.
    /// </typeparam>
    /// <param name="left">
    ///     The left operand of the XOR operation.
    /// </param>
    /// <param name="right">
    ///     The right operand of the XOR operation.
    /// </param>
    /// <returns>
    ///     A new <see cref="IBooleanWithMultipleOutcomes{TOutcome}" /> object representing the result of the XOR operation.
    /// </returns>
    public static IBooleanWithMultipleOutcomes<TOutcome> XOr<TOutcome>(this IBooleanWithMultipleOutcomes<TOutcome> left, IBooleanWithMultipleOutcomes<TOutcome> right) =>
        new XOrWithMultipleOutcomes<TOutcome>(left, right);
}