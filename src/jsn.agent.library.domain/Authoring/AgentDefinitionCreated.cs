using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed record AgentDefinitionCreated(AgentId AgentId, AgentName Name, DateTimeOffset OccurredOn) : IDomainEvent;
