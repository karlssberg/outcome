using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Extensions;

public static class BooleanWithSingleOutcomeExtensions
{

    /// <summary>
    ///     Finds the causes of a given <see cref="IBooleanWithSingleOutcome{TOutcome}" />.  This involves recursively finding the
    ///     underlying causes that led to the current <see cref="IBooleanWithSingleOutcome{TOutcome}" />. This is a convenience method
    ///     for the above method, since a policy result is not a collection of results, but a single result.
    /// </summary>
    /// <param name="booleanSingleOutcome">
    ///     The <see cref="IBooleanWithSingleOutcome{TOutcome}" /> to find the underlying causes of.
    /// </param>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithSingleOutcome{TOutcome}" />.
    /// </typeparam>
    /// <returns>
    ///     The underlying causes of the <see cref="IBooleanWithSingleOutcome{TOutcome}" />.
    /// </returns>
    internal static IEnumerable<Cause<TOutcome>> FindCauses<TOutcome>(this IBooleanWithSingleOutcome<TOutcome> booleanSingleOutcome)
    {
        var causalSpecResults = booleanSingleOutcome.CausalResults;
        return causalSpecResults.SelectMany(BooleanWithMultipleOutcomesExtensions.FindCauses);
    }

    /// <summary>
    ///     Returns a new <see cref="IBooleanWithSingleOutcome{TOutcome}" /> that represents the logical negation of the
    ///     specified <paramref name="result" />.
    /// </summary>
    /// <param name="result">
    ///     The <see cref="IBooleanWithSingleOutcome{TOutcome}" /> to negate.
    /// </param>
    /// <typeparam name="TOutcome">
    ///     The type of the outcome of the <see cref="IBooleanWithSingleOutcome{TOutcome}" />.
    /// </typeparam>
    /// <returns>
    ///     A new <see cref="IBooleanWithSingleOutcome{TOutcome}" /> that represents the logical negation of the
    ///     specified <paramref name="result" />.
    /// </returns>
    public static IBooleanWithSingleOutcome<TOutcome> Not<TOutcome>(this IBooleanWithSingleOutcome<TOutcome> result) =>
        new NotWithSingleOutcome<TOutcome>(result);
}