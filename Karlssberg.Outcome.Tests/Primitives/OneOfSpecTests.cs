using AutoFixture.Xunit2;
using FluentAssertions;
using Karlssberg.Outcome.Primitives;

namespace Karlssberg.Outcome.Tests;

public class OneOfSpecTests
{
    [Theory]
    [AutoData]
    public void Evaluate_Should_Return_True_When_One_Policy_Returns_True(Guid expected, object model)
    {
        // Arrange
        var policy1 = new Policy<object, Guid>(m => true) { TrueOutcome = expected, FalseOutcome = Guid.NewGuid() };
        var policy2 = new Policy<object, Guid>(m => false) { TrueOutcome = Guid.NewGuid(), FalseOutcome = Guid.NewGuid() };
        var sut = new OneOfSpec<object, Guid>(new[] { policy1, policy2 });

        // Act
        var result = sut.Evaluate(model);

        // Assert
        result.Outcomes.Should().HaveCount(1);
        result.Outcomes.Should().Contain(expected);
    }

    [Theory]
    [AutoData]
    public void Evaluate_Should_Return_False_When_No_Policy_Returns_True(Guid expected, object model)
    {
        // Arrange
        var policy1 = new Policy<object, Guid>(m => false) { TrueOutcome = Guid.NewGuid(), FalseOutcome = Guid.NewGuid() };
        var policy2 = new Policy<object, Guid>(m => true) { TrueOutcome = expected, FalseOutcome = Guid.NewGuid() };
        var sut = new OneOfSpec<object, Guid>(new[] { policy1, policy2 });

        // Act
        var result = sut.Evaluate(model);

        // Assert
        result.Outcomes.Should().HaveCount(1);
        result.Outcomes.Should().Contain(expected);
    }

    [Theory]
    [AutoData]
    public void Evaluate_Should_Return_False_When_More_Than_One_Policy_Returns_True(Guid expected1, Guid expected2, object model)
    {
        // Arrange
        var policy1 = new Policy<object, Guid>(m => true) { TrueOutcome = expected1, FalseOutcome = Guid.NewGuid() };
        var policy2 = new Policy<object, Guid>(m => true) { TrueOutcome = expected2, FalseOutcome = Guid.NewGuid() };
        var sut = new OneOfSpec<object, Guid>(new[] { policy1, policy2 });

        // Act¬
        var result = sut.Evaluate(model);

        // Assert
        result.Outcomes.Should().HaveCount(2);
        result.Outcomes.Should().Contain(expected1);
        result.Outcomes.Should().Contain(expected2);
    }

    [Theory]
    [AutoData]
    public void Evaluate_Should_Return_False_When_Both_Policies_Returns_False(Guid expected1, Guid expected2, object model)
    {
        // Arrange
        var policy1 = new Policy<object, Guid>(m => false) { TrueOutcome = Guid.NewGuid(), FalseOutcome = expected1 };
        var policy2 = new Policy<object, Guid>(m => false) { TrueOutcome = Guid.NewGuid(), FalseOutcome = expected2 };
        var sut = new OneOfSpec<object, Guid>(new[] { policy1, policy2 });

        // Act
        var result = sut.Evaluate(model);

        // Assert
        result.Outcomes.Should().HaveCount(2);
        result.Outcomes.Should().Contain(expected1);
        result.Outcomes.Should().Contain(expected2);
    }
}