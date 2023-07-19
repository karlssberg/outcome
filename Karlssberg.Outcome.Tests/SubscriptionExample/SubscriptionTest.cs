using FluentAssertions;
using System.Threading.Tasks;
using Karlssberg.Outcome.Tests.Fixtures;
using Karlssberg.Outcome.Tests.SubscriptionExample.Policies;
using Xunit;

namespace Karlssberg.Outcome.Tests.SubscriptionExample;

public class SubscriptionTest
{
    [AutoData<SpecFixture>]
    [Theory]
    public void Should_not_be_active_when_outside_subscription(
        IPolicy<Account> sut)
    {
    }
}