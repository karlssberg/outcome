using FluentAssertions;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests;

public class BooleanWithSingleOutcomeTests
{
    [Theory]
    [AutoData<SpecFixture>]
    internal void Should_provide_a_single_outcome_when_used_as_a_SpecResult(BooleanWithSingleOutcome<string> booleanSingleOutcome)
    {
        var sut = booleanSingleOutcome as IBooleanWithMultipleOutcomes<string>;
        
        var act = sut.Outcomes;

        act.Should().HaveCount(1);
        act.Should().Contain(booleanSingleOutcome.Outcome);
    }

    [Theory]
    [AutoData<SpecFixture>]
    internal void Should_evaluate_ToString_method_using_values_found_in_the_Causes(
        Policy<object, string> policy, object model)
    {
        var sut = policy.Evaluate(model);
        var act = sut.ToString();

        act.Should().Be(sut.Outcome);
    }
}