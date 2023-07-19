using FluentAssertions;
using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class GenericParamtersTests
{
    [Theory]
    [AutoData<SpecFixture>]
    public void Should_correctly_switch_generic_types_when_a_mixture_of_generic_parameters_lengths_are_used(
        ISpec<object> singleGenericSpec,
        ISpec<object, string> genericSpecWithStringOutcome)
    {
        var sut = singleGenericSpec & genericSpecWithStringOutcome;

        sut.Should().BeAssignableTo<ISpec<object, string>>();
        sut.Should().BeOfType<AndSpec<object, string>>();
    }
}
