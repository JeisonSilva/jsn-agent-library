using jsn.agent.library.domain.Authoring;

namespace jsn.agent.library.domain.tests.Authoring;

public class AgentDefinitionTests
{
    [Fact]
    public void Create_starts_as_draft_without_versions()
    {
        var agent = AgentDefinition.Create(AgentName.From("reviewer"));

        Assert.Equal(AgentStatus.Draft, agent.Status);
        Assert.Empty(agent.Versions);
        Assert.Null(agent.LatestVersion);
    }

    [Fact]
    public void Create_raises_AgentDefinitionCreated()
    {
        var agent = AgentDefinition.Create(AgentName.From("reviewer"));

        var created = Assert.IsType<AgentDefinitionCreated>(Assert.Single(agent.DomainEvents));
        Assert.Equal(agent.Id, created.AgentId);
    }

    [Fact]
    public void ClearDomainEvents_empties_the_event_list()
    {
        var agent = AgentDefinition.Create(AgentName.From("reviewer"));

        agent.ClearDomainEvents();

        Assert.Empty(agent.DomainEvents);
    }
}
