using AutoFixture;
using AutoFixture.AutoMoq;

namespace Karlssberg.Outcome.Tests.Fixtures;

public class SpecFixture : Fixture
{
    public SpecFixture()
    {
        Customize(
            new AutoMoqCustomization
            {
                GenerateDelegates = true,
                ConfigureMembers = true
            });
    }
}