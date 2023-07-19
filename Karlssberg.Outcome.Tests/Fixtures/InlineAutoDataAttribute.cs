using AutoFixture;
using AutoFixture.Xunit2;

namespace Karlssberg.Outcome.Tests.Fixtures;

public class InlineAutoDataAttribute<TFixture> : InlineAutoDataAttribute
    where TFixture : IFixture, new()
{
    public InlineAutoDataAttribute()
        : base(new AutoDataAttribute<TFixture>())
    {
    }
    
    public InlineAutoDataAttribute(params object[] values)
        : base(new AutoDataAttribute<TFixture>(), values)
    {
    }
}