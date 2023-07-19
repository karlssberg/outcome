using FluentAssertions;
using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class AndSpecTests
{
    [Theory]
    [InlineAutoData<SpecFixture>(true, true, true)]
    [InlineAutoData<SpecFixture>(true, false, false)]
    [InlineAutoData<SpecFixture>(false, true, false)]
    [InlineAutoData<SpecFixture>(false, false, false)]
    public void Should_evaluate_AND_spec_result(
        bool left,
        bool right,
        bool expected,
        Policy<object> leftPolicy,
        Policy<object> rightPolicy,
        object model)
    {
        leftPolicy = leftPolicy with { Predicate = _ => left };
        rightPolicy = rightPolicy with { Predicate = _ => right };
        var andSpec = leftPolicy.And(rightPolicy);

        var actual = andSpec.Evaluate(model);

        actual.IsSatisfied.Should().Be(expected);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true, true, true)]
    [InlineAutoData<SpecFixture>(true, false, false)]
    [InlineAutoData<SpecFixture>(false, true, false)]
    [InlineAutoData<SpecFixture>(false, false, false)]
    public void Should_have_AND_operator_that_behaves_the_same_as_the_And_method(
        bool left,
        bool right,
        bool expected,
        Policy<object> leftPolicy,
        Policy<object> rightPolicy,
        object model)
    {
        leftPolicy = leftPolicy with { Predicate = _ => left };
        rightPolicy = rightPolicy with { Predicate = _ => right };

        var sut = leftPolicy & rightPolicy;

        var act = sut.Evaluate(model);

        act.IsSatisfied.Should().Be(expected);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true, true)]
    [InlineAutoData<SpecFixture>(true, false)]
    [InlineAutoData<SpecFixture>(false, true)]
    [InlineAutoData<SpecFixture>(false, false)]
    public void Should_allow_AND_to_provide_alternative_true_and_false_causes(
        bool left,
        bool right,
        Policy<object> leftPolicy,
        Policy<object> rightPolicy,
        string trueBecause,
        string falseBecause,
        object model)
    {
        leftPolicy = leftPolicy with { Predicate = _ => left };
        rightPolicy = rightPolicy with { Predicate = _ => right };

        var sut = leftPolicy
            .And(rightPolicy)
            .ToPolicy(trueBecause, falseBecause);

        var act = sut.Evaluate(model);

        var expectedOutcome = act.IsSatisfied ? trueBecause : falseBecause;

        act.Outcome.Should().Be(expectedOutcome);
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_And_spec_when_using_the_And_operator_with_policy_interfaces(
        IPolicy<object> left,
        IPolicy<object> right)
    {
        var act = left & right;

        act.Should().BeAssignableTo<ISpec<object>>();
        act.Should().BeOfType<AndSpec<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_And_spec_when_using_the_And_operator_with_policies(
        Policy<object> left,
        Policy<object> right)
    {
        var act = left & right;

        act.Should().BeAssignableTo<ISpec<object>>();
        act.Should().BeOfType<AndSpec<object>>();
    }

    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    [Theory]
    public void Should_return_And_spec_when_using_the_And_operator_with_mixture_of_policy_interfaces_and_policies(
        bool areOperandsReversed,
        IPolicy<object> left,
        Policy<object> right)
    {
        var act = areOperandsReversed
            ? right & left
            : left & right;

        act.Should().BeAssignableTo<ISpec<object>>();
        act.Should().BeOfType<AndSpec<object>>();
    }

    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    [Theory]
    public void Should_return_And_spec_when_using_the_And_operator_with_mixture_of_spec_interfaces_and_policies(
        bool areOperandsReversed,
        ISpec<object> left,
        Policy<object> right)
    {
        var act = areOperandsReversed
            ? right & left
            : left & right;

        act.Should().BeAssignableTo<ISpec<object>>();
        act.Should().BeOfType<AndSpec<object>>();
    }

    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    [Theory]
    public void Should_return_And_spec_when_using_the_And_operator_with_mixture_of_policy_interfaces_and_spec_interfaces(
        bool areOperandsReversed,
        ISpec<object> left,
        IPolicy<object> right)
    {
        var act = areOperandsReversed
            ? right & left
            : left & right;

        act.Should().BeAssignableTo<ISpec<object>>();
        act.Should().BeOfType<AndSpec<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_an_And_specification_when_calling_And_method_with_specs(
        ISpec<object> left,
        ISpec<object> right)
    {
        var sut = left.And(right);

        sut.Should().BeOfType<AndSpec<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_an_And_specification_when_calling_And_operator_overload_with_specs(
        ISpec<object> left,
        ISpec<object> right)
    {
        var sut = left & right;

        sut.Should().BeOfType<AndSpec<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_an_And_specification_when_calling_And_method_with_policies(
        Policy<object> left,
        Policy<object> right)
    {
        var sut = left.And(right);

        sut.Should().BeOfType<AndSpec<object>>();
    }
}