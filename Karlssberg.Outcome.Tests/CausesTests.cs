using FluentAssertions;
using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Results;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests;

public class CausesTests
{
    private static Policy<object, string> CreatePolicy(string name, bool value) =>
        new Policy<object, string>(_ => value)
        {
            TrueOutcome = GetTrueOutcome(name),
            FalseOutcome = GetFalseOutcome(name)
        };

    private static string GetOutcome(string name, bool isSatisfied) => isSatisfied
        ? GetTrueOutcome(name)
        : GetFalseOutcome(name);

    private static string GetFalseOutcome(string name) => $"{name} is false";

    private static string GetTrueOutcome(string name) => $"{name} is true";

    [Theory]
    [InlineAutoData<SpecFixture>(true, false)]
    public void Should_remove_intermediate_spec_results_that_are_not_policy_results_objects(
        bool aValue,
        bool bValue,
        object model)
    {
        var a = CreatePolicy(nameof(aValue), aValue);
        var b = CreatePolicy(nameof(bValue), bValue);

        var xOrSpec = (a & !b) | (!a & b);
        const string trueBecause = "XOR is true";
        const string falseBecause = "XOR is false";
        
        var xOrPolicy = xOrSpec.ToPolicy(trueBecause, falseBecause);

        var sut = xOrPolicy.Evaluate(model);
        var act = sut.Causes.ToList();

        act.Should().ContainSingle(cause => cause.Outcome == a.TrueOutcome);
        act.Should().ContainSingle(cause => cause.Outcome == b.FalseOutcome);
    }

    [Theory]
    [InlineAutoData<SpecFixture>(true, false)]
    public void Should_remove_spec_results_that_are_not_policy_results_objects(
        bool aValue,
        bool bValue,
        object model)
    {
        var a = CreatePolicy(nameof(aValue), aValue);
        var b = CreatePolicy(nameof(bValue), bValue);

        var xOrSpec = (a & !b) | (!a & b);

        var sut = xOrSpec.Evaluate(model);
        var act = sut.Causes.ToList();

        act.Should().ContainSingle(cause => cause.Outcome == a.TrueOutcome);
        act.Should().ContainSingle(cause => cause.Outcome == b.FalseOutcome);
    }
}