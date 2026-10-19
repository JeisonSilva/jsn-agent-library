using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Retirement;

public sealed record AgentDefinitionDeprecated(AgentId AgentId, DateTimeOffset OccurredOn) : IDomainEvent;
