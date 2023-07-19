using FluentAssertions;
using Karlssberg.Outcome.Extensions;
using Karlssberg.Outcome.Primitives;
using Karlssberg.Outcome.Tests.Fixtures;

namespace Karlssberg.Outcome.Tests.OperationTests;

public class NotSpecTests
{
    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_a_Not_specification_when_calling_Not_method_with_specification(
        ISpec<object, string> spec)
    {
        var sut = spec.Not();

        sut.Should().BeAssignableTo<ISpec<object>>();
        sut.Should().BeOfType<NotSpec<object>>();
    }
    
    [AutoData<SpecFixture>]
    [Theory]
    public void Should_return_Not_spec_when_using_the_Not_operator_on_a_spec(
        ISpec<object, string> sut)
    {
        var act = !sut;

        act.Should().BeAssignableTo<ISpec<object, string>>();
        act.Should().BeOfType<NotSpec<object, string>>();
    }
}