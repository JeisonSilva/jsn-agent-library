using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Publishing;

public sealed record AgentVersionPublished(AgentId AgentId, SemanticVersion Version, DateTimeOffset OccurredOn) : IDomainEvent;
