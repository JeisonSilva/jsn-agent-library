using jsn.agent.library.domain.Authoring;

namespace jsn.agent.library.domain.Discovery;

public interface IAgentCatalog
{
    Task<IReadOnlyList<AgentDefinition>> SearchAsync(
        AgentSearchSpecification specification,
        CancellationToken cancellationToken = default);
}
