using Karlssberg.Outcome.Extensions;

namespace Karlssberg.Outcome.Results;

internal record BooleanWithSingleOutcome<TOutcome>(bool IsSatisfied, TOutcome Outcome) : IBooleanWithSingleOutcome<TOutcome>
{
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; init; } = Enumerable.Empty<IBooleanWithMultipleOutcomes<TOutcome>>();
    IEnumerable<Cause<TOutcome>> Causes => this.FindCauses();

    public override string ToString() => Outcome.ToString();
}