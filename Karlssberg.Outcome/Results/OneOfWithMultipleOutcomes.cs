namespace Karlssberg.Outcome.Results;

public record OneOfWithMultipleOutcomes<TOutcome> : IBooleanWithMultipleOutcomes<TOutcome>
{
    public OneOfWithMultipleOutcomes(IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> specResults)
    {
        ArgumentNullException.ThrowIfNull(specResults, nameof(specResults));

        var results = specResults.ToList();
        if (results.Count == 0)
        {
            throw new ArgumentException("Must have at least one spec result", nameof(specResults));
        }

        var satisfiedResults = results.Where(result => result.IsSatisfied).ToList();

        IsSatisfied = satisfiedResults.Count == 1;
        CausalResults = IsSatisfied ? satisfiedResults : results;
        Outcomes = CausalResults.SelectMany(result => result.Outcomes);
    }

    public bool IsSatisfied { get; }
    public IEnumerable<TOutcome> Outcomes { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }
}