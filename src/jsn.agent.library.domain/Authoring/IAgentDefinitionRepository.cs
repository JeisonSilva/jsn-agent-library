namespace jsn.agent.library.domain.Authoring;

public interface IAgentDefinitionRepository
{
    Task<AgentDefinition?> GetByIdAsync(AgentId id, CancellationToken cancellationToken = default);

    Task AddAsync(AgentDefinition agent, CancellationToken cancellationToken = default);

    Task UpdateAsync(AgentDefinition agent, CancellationToken cancellationToken = default);
}
