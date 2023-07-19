using FluentAssertions;
using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class OrSpecTests
{
    [Theory]
    [InlineAutoData<SpecFixture>(true, true, true)]
    [InlineAutoData<SpecFixture>(true, false, true)]
    [InlineAutoData<SpecFixture>(false, true, true)]
    [InlineAutoData<SpecFixture>(false, false, false)]
    public void Should_evaluate_OR_spec_result(
        bool left,
        bool right,
        bool expected,
        Policy<object, string> leftPolicy,
        Policy<object, string> rightPolicy,
        object model)
    {
        leftPolicy = leftPolicy with { Predicate = _ => left };
        rightPolicy = rightPolicy with { Predicate = _ => right };
        var orSpec = leftPolicy.Or(rightPolicy);

        var actual = orSpec.Evaluate(model);

        actual.IsSatisfied.Should().Be(expected);
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_Or_spec_when_using_the_Or_operator_with_policies(
        Policy<object, string> left,
        Policy<object, string> right)
    {
        var act = left | right;

        act.Should().BeAssignableTo<ISpec<object, string>>();
        act.Should().BeOfType<OrSpec<object, string>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_Or_spec_when_using_the_Or_operator_with_policy_interfaces(
        IPolicy<object, string> left,
        IPolicy<object, string> right)
    {
        var act = left | right;

        act.Should().BeAssignableTo<ISpec<object, string>>();
        act.Should().BeOfType<OrSpec<object, string>>();
    }

    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    [Theory]
    public void Should_return_Or_spec_when_using_the_Or_operator_with_mixture_of_policy_interfaces_and_policies(
        bool areOperandsReversed,
        IPolicy<object, string> left,
        Policy<object, string> right)
    {
        var act = areOperandsReversed
            ? right | left
            : left | right;

        act.Should().BeAssignableTo<ISpec<object, string>>();
        act.Should().BeOfType<OrSpec<object, string>>();
    }

    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    [Theory]
    public void Should_return_Or_spec_when_using_the_Or_operator_with_mixture_of_spec_interfaces_and_policies(
        bool areOperandsReversed,
        ISpec<object, string> left,
        Policy<object, string> right)
    {
        var act = areOperandsReversed
            ? right | left
            : left | right;

        act.Should().BeAssignableTo<ISpec<object, string>>();
        act.Should().BeOfType<OrSpec<object, string>>();
    }

    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    [Theory]
    public void Should_return_Or_spec_when_using_the_Or_operator_with_mixture_of_spec_interfaces_and_policy_interfaces(
        bool areOperandsReversed,
        ISpec<object, string> left,
        IPolicy<object, string> right)
    {
        var act = areOperandsReversed
            ? right | left
            : left | right;

        act.Should().BeAssignableTo<ISpec<object, string>>();
        act.Should().BeOfType<OrSpec<object, string>>();
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true, true, true)]
    [InlineAutoData<SpecFixture>(true, false, true)]
    [InlineAutoData<SpecFixture>(false, true, true)]
    [InlineAutoData<SpecFixture>(false, false, false)]
    public void Should_have_OR_operator_that_behaves_the_same_as_the_Or_method(
        bool left,
        bool right,
        bool expected,
        Policy<object, string> leftPolicy,
        Policy<object, string> rightPolicy,
        object model)
    {
        leftPolicy = leftPolicy with { Predicate = _ => left };
        rightPolicy = rightPolicy with { Predicate = _ => right };

        var sut = leftPolicy | rightPolicy;

        var act = sut.Evaluate(model);

        act.IsSatisfied.Should().Be(expected);
    }
    [Theory]
    [InlineAutoData<SpecFixture>(true, true)]
    [InlineAutoData<SpecFixture>(true, false)]
    [InlineAutoData<SpecFixture>(false, true)]
    [InlineAutoData<SpecFixture>(false, false)]
    public void Should_allow_OR_to_provide_alternative_true_and_false_causes(
        bool left,
        bool right,
        Policy<object, string> leftPolicy,
        Policy<object, string> rightPolicy,
        string trueBecause,
        string falseBecause,
        object model)
    {
        leftPolicy = leftPolicy with { Predicate = _ => left };
        rightPolicy = rightPolicy with { Predicate = _ => right };

        var andSpec = leftPolicy
            .Or(rightPolicy)
            .ToPolicy(trueBecause, falseBecause);

        var act = andSpec.Evaluate(model);

        var expectedOutcome = act.IsSatisfied ? trueBecause : falseBecause;

        act.Outcome.Should().Be(expectedOutcome);
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_an_Or_specification_when_calling_Or_method_with_specs(
        ISpec<object, string> left,
        ISpec<object, string> right)
    {
        var sut = left.Or(right);

        sut.Should().BeOfType<OrSpec<object>>();
    }

    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_an_Or_specification_when_calling_Or_method_with_policies(
        Policy<object, string> left,
        Policy<object, string> right)
    {
        var sut = left.Or(right);

        sut.Should().BeOfType<OrSpec<object>>();
    }
}