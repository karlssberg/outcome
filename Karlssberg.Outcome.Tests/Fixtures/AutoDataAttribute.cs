using AutoFixture;
using AutoFixture.Xunit2;

namespace Karlssberg.Outcome.Tests.Fixtures;

public class AutoDataAttribute<TFixture> : AutoDataAttribute
    where TFixture : IFixture, new()
{
    public AutoDataAttribute() : base(()=> new TFixture())
    {
    }
}