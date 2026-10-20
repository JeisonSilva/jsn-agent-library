using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.Publishing;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.tests.Publishing;

public class PublishAgentVersionTests
{
    private static AgentDefinition NewAgent() => AgentDefinition.Create(AgentName.From("reviewer"));

    private static AgentVersion Publish(AgentDefinition agent, string number) =>
        agent.Publish(SemanticVersion.Parse(number), Instructions.From("Revise o código."), ModelSpec.From("claude-sonnet-5-5"));

    [Fact]
    public void Publish_moves_the_agent_to_published_and_records_the_version()
    {
        var agent = NewAgent();
        agent.ClearDomainEvents();

        var version = Publish(agent, "1.0.0");

        Assert.Equal(AgentStatus.Published, agent.Status);
        Assert.Same(version, agent.LatestVersion);
        var published = Assert.IsType<AgentVersionPublished>(Assert.Single(agent.DomainEvents));
        Assert.Equal(version.Number, published.Version);
    }

    [Fact]
    public void Publish_keeps_tools_and_capabilities_without_duplicates()
    {
        var agent = NewAgent();

        var version = agent.Publish(
            SemanticVersion.Of(1, 0, 0),
            Instructions.From("Revise o código."),
            ModelSpec.From("claude-sonnet-5-5"),
            [ToolReference.From("git"), ToolReference.From("GIT")],
            [Capability.From("code-review"), Capability.From("Code-Review")]);

        Assert.Single(version.Tools);
        Assert.Single(version.Capabilities);
    }

    [Theory]
    [InlineData("1.0.0")]
    [InlineData("0.9.0")]
    public void Publish_rejects_a_version_not_greater_than_the_latest(string number)
    {
        var agent = NewAgent();
        Publish(agent, "1.0.0");

        Assert.Throws<DomainException>(() => Publish(agent, number));
    }

    [Fact]
    public void Publish_accepts_a_greater_version()
    {
        var agent = NewAgent();
        Publish(agent, "1.0.0");

        Publish(agent, "1.1.0");

        Assert.Equal(2, agent.Versions.Count);
    }
}
