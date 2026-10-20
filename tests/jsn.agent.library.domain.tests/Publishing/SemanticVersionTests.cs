using jsn.agent.library.domain.Publishing;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.tests.Publishing;

public class SemanticVersionTests
{
    [Fact]
    public void Parse_reads_major_minor_patch()
    {
        var version = SemanticVersion.Parse("1.2.3");

        Assert.Equal(SemanticVersion.Of(1, 2, 3), version);
        Assert.Equal("1.2.3", version.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("1.2")]
    [InlineData("1.2.x")]
    [InlineData("1.2.-3")]
    public void Parse_rejects_invalid_format(string? value) =>
        Assert.Throws<DomainException>(() => SemanticVersion.Parse(value));

    [Fact]
    public void Of_rejects_negative_numbers() =>
        Assert.Throws<DomainException>(() => SemanticVersion.Of(1, -1, 0));

    [Fact]
    public void Versions_are_ordered_by_major_then_minor_then_patch()
    {
        Assert.True(SemanticVersion.Of(1, 0, 0) < SemanticVersion.Of(2, 0, 0));
        Assert.True(SemanticVersion.Of(1, 1, 0) > SemanticVersion.Of(1, 0, 9));
        Assert.True(SemanticVersion.Of(1, 0, 1) >= SemanticVersion.Of(1, 0, 1));
    }
}
