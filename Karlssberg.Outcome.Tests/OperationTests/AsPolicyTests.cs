using FluentAssertions;
using Karlssberg.Outcome.Adapters;
using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class AsPolicyTests
{
    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_a_policy_adapter_when_calling_ToPolicy_method(
        ISpec<object> spec,
        string trueOutcome,
        string falseOutcome)
    {
        var sut = spec.ToPolicy(trueOutcome, falseOutcome);

        sut.Should().BeAssignableTo<IPolicy<object>>();
        sut.Should().BeOfType<SpecAsPolicy<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_supply_correct_policy_outcomes_upon_evaluation(
        ISpec<object> spec,
        string trueOutcome,
        string falseOutcome,
        object model)
    {
        var sut = spec.ToPolicy(trueOutcome, falseOutcome);

        var act = sut.Evaluate(model);

        act.Outcome.Should().Be(act.IsSatisfied ? trueOutcome : falseOutcome);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_create_appropriate_sub_cause_when_nesting_polices(
        bool value,
        Policy<object> policy,
        string trueOutcomeA,
        string falseOutcomeA,
        string trueOutcomeB,
        string falseOutcomeB,
        object model)
    {
        policy = policy with { Predicate = _ => value };
        var sut = policy
            .Not()
            .ToPolicy(trueOutcomeA, falseOutcomeA)
            .ToPolicy(trueOutcomeB, falseOutcomeB);

        var act = sut.Evaluate(model);

        var expectedOutcome = act.IsSatisfied ? trueOutcomeA : falseOutcomeA;
        var expectedSubOutcomes = act.IsSatisfied ? trueOutcomeB : falseOutcomeB;

        act.Outcome.Should().Be(expectedOutcome);
        act.SubOutcomes
            .Should()
            .ContainSingle(outcome => outcome == expectedSubOutcomes);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_allow_triple_nested_policy(
        bool value,
        Policy<object> policy,
        string trueOutcomeA,
        string falseOutcomeA,
        string trueOutcomeB,
        string falseOutcomeB,
        string trueOutcomeC,
        string falseOutcomeC,
        object model)
    {
        policy = policy with { Predicate = _ => value };

        var sut = policy
            .Not()
            .ToPolicy(trueOutcomeA, falseOutcomeA)
            .ToPolicy(trueOutcomeB, falseOutcomeB)
            .ToPolicy(trueOutcomeC, falseOutcomeC);

        var act = sut.Evaluate(model);

        var expectedOutcome = act.IsSatisfied ? trueOutcomeC : falseOutcomeC;
        var expectedSubOutcome = act.IsSatisfied ? trueOutcomeB : falseOutcomeB;
        var expectedSubSubOutcome = act.IsSatisfied ? trueOutcomeA : falseOutcomeA;

        act.Outcome.Should().Be(expectedOutcome);

        act.CausalResults.Should().AllBeAssignableTo<IBooleanWithSingleOutcome<string>>();
        act.CausalResults.Should().HaveCount(1);
        act.SubOutcomes.Should().AllBeEquivalentTo(expectedSubOutcome);
        act.CausalResults.SelectMany(result => result.Outcomes).Should().AllBeEquivalentTo(expectedSubOutcome);

        var causalSubSubResults = act.CausalResults.SelectMany(result => result.CausalResults);
        causalSubSubResults.Should().AllBeAssignableTo<IBooleanWithSingleOutcome<string>>();
        causalSubSubResults.Should().HaveCount(1);
        causalSubSubResults.SelectMany(result => result.Outcomes).Should().AllBeEquivalentTo(expectedSubSubOutcome);
    }
}