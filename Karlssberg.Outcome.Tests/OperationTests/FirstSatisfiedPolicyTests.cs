using FluentAssertions;
using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class FirstSatisfiedPolicyTests
{
    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_a_policy_when_calling_FirstSatisfied_method(
        IEnumerable<Policy<object, string>> policies,
        string defaultOutcome)
    {
        var sut = policies.FirstSatisfied(defaultOutcome);

        sut.Should().BeAssignableTo<IPolicy<object, string>>();
        sut.Should().BeOfType<FirstSatisfiedPolicy<object, string>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_a_FirstSatisfied_policy_when_calling_Switch_method_with_specification(
        IEnumerable<Policy<object, string>> policies,
        string defaultOutcome)
    {
        var sut = policies.FirstSatisfied(defaultOutcome);

        sut.Should().BeAssignableTo<IPolicy<object, string>>();
        sut.Should().BeOfType<FirstSatisfiedPolicy<object, string>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_a_default_false_outcome_when_evaluating_a_firstOrSatisfied_policy(
        IEnumerable<Policy<object, string>> policies,
        string defaultOutcome,
        object model)
    {
        var falsePolicy = policies.Select(policy => policy with { Predicate = _ => false });

        var sut = policies.FirstSatisfied(defaultOutcome);
        var act = sut.Evaluate(model);

        act.IsSatisfied.Should().BeFalse();
        act.Outcome.Should().Be(defaultOutcome);
        act.SubOutcomes.Should().BeEquivalentTo(falsePolicy.Select(policy => policy.FalseOutcome));
    }
}