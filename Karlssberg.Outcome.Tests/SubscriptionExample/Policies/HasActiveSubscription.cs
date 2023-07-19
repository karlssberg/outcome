using Karlssberg.Outcome.Results;

namespace Karlssberg.Outcome.Tests.SubscriptionExample.Policies;

public class HasActiveSubscription : IPolicy<Account>
{
    public HasActiveSubscription(DateTimeOffset now)
    {
        Now = now;
    }

    public DateTimeOffset Now { get; }
    public IBooleanWithSingleOutcome<string> Evaluate(Account model) => throw new NotImplementedException();
}

public class User : IEntity
{
    public Guid Id { get; init; }
}

public class Account : IEntity
{
    public Guid Id { get; init; }
    public IEnumerable<Subscription> Subscriptions { get; init; } = Enumerable.Empty<Subscription>();
}
public class Subscription : IEntity
{
    public Guid Id { get; init; }
    public Guid AccountId { get; init; }
    public DateTimeOffset Start { get; init; }
    public DateOnly End { get; init; }
}

public interface IEntity
{
    Guid Id { get; }
}