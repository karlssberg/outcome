using FluentAssertions;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests;

public class BooleanWithMultipleOutcomesTests
{
    [Theory]
    [AutoData<SpecFixture>]
    internal void Should_evaluate_GetOutcomes_method_using_values_found_in_the_Causes(
        IBooleanWithMultipleOutcomes<string> sut)
    {
        var act = sut.Outcomes;

        act.Should().BeEquivalentTo(sut.Outcomes);
    }

    [Theory]
    [AutoData<SpecFixture>]
    internal void Should_evaluate_ToString_method_using_values_found_in_the_Causes(
        Policy<object> policyA,
        Policy<object> policyB,
        string model)
    {
        policyA = policyA with { Predicate = _ => true };
        policyB = policyB with { Predicate = _ => true };
        
        var spec = policyA & policyB;
        var sut = spec.Evaluate(model);
        var act = sut.ToString();

        act.Should().Be($"({policyA}) & ({policyB})");
    }
}