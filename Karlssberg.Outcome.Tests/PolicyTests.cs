using FluentAssertions;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests;

public class PolicyTests
{
    [Theory]
    [InlineAutoData<SpecFixture>(true)]
    [InlineAutoData<SpecFixture>(false)]
    public void Should_evaluate_policy(
        bool value,
        string trueOutcome,
        string falseOutcome,
        object model)
    {
        var expectedOutcome = value ? trueOutcome : falseOutcome;
        var sut = new Policy<object, string>(_ => value)
        {
            TrueOutcome = trueOutcome,
            FalseOutcome = falseOutcome
        };
        var act = sut.Evaluate(model);

        act.IsSatisfied.Should().Be(value);
        act.Outcome.Should().Be(expectedOutcome);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_handle_supplied_spec_item(
        int number,
        string trueOutcome,
        string falseOutcome)
    {
        var expected = (number & 1) == 0;
        var expectedOutcome = expected ? trueOutcome : falseOutcome;
        var sut = new Policy<int, string>(i => i % 2 == 0)
        {
            TrueOutcome = trueOutcome,
            FalseOutcome = falseOutcome
        };

        var act = sut.Evaluate(number);

        act.IsSatisfied.Should().Be(expected);
        act.Outcome.Should().Be(expectedOutcome);
    }

    [Theory]
    [AutoData<SpecFixture>]
    public void Should_return_outcome_when_outcome_is_set(
        bool value,
        string trueOutcome,
        string falseOutcome,
        object model)
    {
        var sut = new Policy<object, string>(_ => value)
        {
            TrueOutcome = trueOutcome,
            FalseOutcome = falseOutcome
        };
        var act = sut.Evaluate(model);

        act.Outcome.Should().NotBeNull();
    }
}