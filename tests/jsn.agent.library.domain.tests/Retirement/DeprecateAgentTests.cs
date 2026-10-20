using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.Publishing;
using jsn.agent.library.domain.Retirement;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.tests.Retirement;

public class DeprecateAgentTests
{
    private static AgentDefinition PublishedAgent()
    {
        var agent = AgentDefinition.Create(AgentName.From("reviewer"));
        agent.Publish(SemanticVersion.Of(1, 0, 0), Instructions.From("Revise o código."), ModelSpec.From("claude-sonnet-5-5"));
        agent.ClearDomainEvents();
        return agent;
    }

    [Fact]
    public void Deprecate_marks_the_agent_and_raises_the_event()
    {
        var agent = PublishedAgent();

        agent.Deprecate();

        Assert.Equal(AgentStatus.Deprecated, agent.Status);
        Assert.IsType<AgentDefinitionDeprecated>(Assert.Single(agent.DomainEvents));
    }

    [Fact]
    public void Deprecate_rejects_an_agent_without_published_version() =>
        Assert.Throws<DomainException>(() => AgentDefinition.Create(AgentName.From("reviewer")).Deprecate());

    [Fact]
    public void Deprecate_rejects_an_already_deprecated_agent()
    {
        var agent = PublishedAgent();
        agent.Deprecate();

        Assert.Throws<DomainException>(() => agent.Deprecate());
    }

    [Fact]
    public void Deprecated_agent_does_not_accept_new_versions()
    {
        var agent = PublishedAgent();
        agent.Deprecate();

        Assert.Throws<DomainException>(() =>
            agent.Publish(SemanticVersion.Of(2, 0, 0), Instructions.From("Revise o código."), ModelSpec.From("claude-sonnet-5-5")));
    }
}
