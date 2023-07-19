namespace Karlssberg.Outcome.Results;

public record OneOfWithSingleOutcome<TOutcome> : IBooleanWithSingleOutcome<TOutcome>
{
    public OneOfWithSingleOutcome(IEnumerable<IBooleanWithSingleOutcome<TOutcome>> policyResults, TOutcome falseOutcome)
    {
        ArgumentNullException.ThrowIfNull(policyResults, nameof(policyResults));
        
        var results = policyResults.ToList();
        if (results.Count == 0)
        {
            throw new ArgumentException("Must have at least one policy result", nameof(policyResults));
        }
        
        var satisfiedResults = results.Where(result => result.IsSatisfied).ToList();

        IsSatisfied = satisfiedResults.Count == 1;
        CausalResults = satisfiedResults.Any() ? satisfiedResults : results;
        Outcome = IsSatisfied ? satisfiedResults.Single().Outcome : falseOutcome;
    }

    public bool IsSatisfied { get; }
    public TOutcome Outcome { get; }
    public IEnumerable<IBooleanWithMultipleOutcomes<TOutcome>> CausalResults { get; }
}