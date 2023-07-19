using FluentAssertions;
using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class NotPolicyTests
{

    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_evaluate_policy_result(bool value, Policy<object, string> policy, object model)
    {
        var sut = policy with { Predicate = _ => value };

        var actual = sut.Evaluate(model);

        var expectedOutcome = actual.IsSatisfied ? policy.TrueOutcome : policy.FalseOutcome;
        actual.IsSatisfied.Should().Be(value);
        actual.Outcome.Should().Be(expectedOutcome);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_evaluate_NOT_spec_result(
        bool initialValue,
        Policy<object, string> policy,
        object model)
    {
        policy = policy with { Predicate = _ => initialValue };
        var sut = policy.Not();

        var actual = sut.Evaluate(model);


        actual.IsSatisfied.Should().Be(!initialValue);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_have_NOT_operator_that_behaves_the_same_as_the_Not_method(
        bool initialValue,
        Policy<object, string> policy,
        object model)
    {
        policy = policy with { Predicate = _ => initialValue };
        var sut = !policy;

        var actual = sut.Evaluate(model);

        actual.IsSatisfied.Should().Be(!initialValue);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_allow_NOT_to_provide_alternative_true_and_false_causes(
        bool value,
        Policy<object, string> policy,
        string trueBecause,
        string falseBecause,
        object model)
    {
        policy = policy with { Predicate = _ => value };
        var sut = policy
            .Not()
            .ToPolicy(trueBecause, falseBecause);

        var act = sut.Evaluate(model);

        var expectedOutcome = act.IsSatisfied ? trueBecause : falseBecause;
        act.Outcome.Should().Be(expectedOutcome);
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_a_Not_policy_when_calling_Not_method_with_specification(
        IPolicy<object, string> policy)
    {
        var sut = policy.Not();

        sut.Should().BeAssignableTo<IPolicy<object, string>>();
        sut.Should().BeOfType<NotPolicy<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_Not_spec_when_using_the_Not_operator_on_a_policy(
        IPolicy<object> policy)
    {
        var act = !policy;

        act.Should().BeAssignableTo<IPolicy<object>>();
        act.Should().BeOfType<NotPolicy<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_Not_spec_when_using_the_Not_operator_on_a_policy_with_string_outcome(
        IPolicy<object, string> policy)
    {
        var act = !policy;

        act.Should().BeAssignableTo<IPolicy<object, string>>();
        act.Should().BeOfType<NotPolicy<object, string>>();
    }
}