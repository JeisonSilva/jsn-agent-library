using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.tests.Authoring;

public class AgentNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void From_rejects_empty_name(string? value) =>
        Assert.Throws<DomainException>(() => AgentName.From(value));

    [Fact]
    public void From_rejects_name_over_the_limit() =>
        Assert.Throws<DomainException>(() => AgentName.From(new string('a', AgentName.MaxLength + 1)));

    [Fact]
    public void From_trims_and_compares_by_value() =>
        Assert.Equal(AgentName.From("reviewer"), AgentName.From("  reviewer "));
}
