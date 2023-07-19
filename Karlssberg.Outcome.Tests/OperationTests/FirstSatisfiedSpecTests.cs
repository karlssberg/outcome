using FluentAssertions;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class FirstSatisfiedSpecTests
{
    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_first_satisfied_spec(Policy<object> policy1, Policy<object> policy2, Policy<object> policy3)
    {
        policy1 = policy1 with { Predicate = _ => false };
        policy2 = policy2 with { Predicate = _ => true };
        policy3 = policy3 with { Predicate = _ => true };
        var policies = new[]
        {
            policy1,
            policy2,
            policy3
        };

        var sut = policies.FirstSatisfied("false");

        var result = sut.Evaluate(new object());
        result.IsSatisfied.Should().BeTrue();
        result.CausalResults.Should().HaveCount(1);
        result.CausalResults.Should().Contain(policy2.Evaluate(new object()));
    }

}