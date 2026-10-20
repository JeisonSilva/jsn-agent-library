using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.Discovery;
using jsn.agent.library.domain.Publishing;

namespace jsn.agent.library.domain.tests.Discovery;

public class AgentSearchSpecificationTests
{
    private static AgentDefinition PublishedAgent(params string[] capabilities)
    {
        var agent = AgentDefinition.Create(AgentName.From("reviewer"));
        agent.Publish(
            SemanticVersion.Of(1, 0, 0),
            Instructions.From("Revise o código."),
            ModelSpec.From("claude-sonnet-5-5"),
            capabilities: capabilities.Select(Capability.From));
        return agent;
    }

    [Fact]
    public void Draft_agents_are_never_listed() =>
        Assert.False(new AgentSearchSpecification().IsSatisfiedBy(AgentDefinition.Create(AgentName.From("reviewer"))));

    [Fact]
    public void Published_agents_are_listed() =>
        Assert.True(new AgentSearchSpecification().IsSatisfiedBy(PublishedAgent()));

    [Fact]
    public void Deprecated_agents_are_listed_only_when_requested()
    {
        var agent = PublishedAgent();
        agent.Deprecate();

        Assert.False(new AgentSearchSpecification().IsSatisfiedBy(agent));
        Assert.True(new AgentSearchSpecification { IncludeDeprecated = true }.IsSatisfiedBy(agent));
    }

    [Fact]
    public void Required_capability_filters_by_the_latest_version()
    {
        var agent = PublishedAgent("code-review");

        Assert.True(new AgentSearchSpecification { RequiredCapability = Capability.From("Code-Review") }.IsSatisfiedBy(agent));
        Assert.False(new AgentSearchSpecification { RequiredCapability = Capability.From("testing") }.IsSatisfiedBy(agent));
    }
}
