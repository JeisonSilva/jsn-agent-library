using jsn.agent.library.domain.Authoring;

namespace jsn.agent.library.domain.Discovery;

public sealed class AgentSearchSpecification
{
    public Capability? RequiredCapability { get; init; }

    public bool IncludeDeprecated { get; init; }

    public bool IsSatisfiedBy(AgentDefinition agent)
    {
        if (agent.Status == AgentStatus.Draft)
            return false;

        if (agent.Status == AgentStatus.Deprecated && !IncludeDeprecated)
            return false;

        return RequiredCapability is null
            || agent.LatestVersion!.Capabilities.Contains(RequiredCapability);
    }
}
