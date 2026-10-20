using jsn.agent.library.domain.Publishing;
using jsn.agent.library.domain.Retirement;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class AgentDefinition : AggregateRoot<AgentId>
{
    private readonly List<AgentVersion> _versions = [];

    private AgentDefinition(AgentId id, AgentName name) : base(id) => Name = name;

    public AgentName Name { get; }

    public AgentStatus Status { get; private set; } = AgentStatus.Draft;

    public IReadOnlyList<AgentVersion> Versions => _versions;

    public AgentVersion? LatestVersion => _versions.Count == 0 ? null : _versions[^1];

    public static AgentDefinition Create(AgentName name, TimeProvider? clock = null)
    {
        var agent = new AgentDefinition(AgentId.New(), name);
        agent.Raise(new AgentDefinitionCreated(agent.Id, name, (clock ?? TimeProvider.System).GetUtcNow()));
        return agent;
    }

    public AgentVersion Publish(
        SemanticVersion number,
        Instructions instructions,
        ModelSpec model,
        IEnumerable<ToolReference>? tools = null,
        IEnumerable<Capability>? capabilities = null,
        TimeProvider? clock = null)
    {
        if (Status == AgentStatus.Deprecated)
            throw new DomainException("Um agent descontinuado não pode receber novas versões.");

        if (LatestVersion is { } latest && number <= latest.Number)
            throw new DomainException($"A versão {number} deve ser maior que a última publicada ({latest.Number}).");

        var now = (clock ?? TimeProvider.System).GetUtcNow();
        var version = new AgentVersion(number, instructions, model, tools ?? Array.Empty<ToolReference>(), capabilities ?? Array.Empty<Capability>(), now);

        _versions.Add(version);
        Status = AgentStatus.Published;
        Raise(new AgentVersionPublished(Id, number, now));

        return version;
    }

    public void Deprecate(TimeProvider? clock = null)
    {
        if (Status == AgentStatus.Deprecated)
            throw new DomainException("O agent já está descontinuado.");

        if (Status == AgentStatus.Draft)
            throw new DomainException("Um agent sem versão publicada não pode ser descontinuado.");

        Status = AgentStatus.Deprecated;
        Raise(new AgentDefinitionDeprecated(Id, (clock ?? TimeProvider.System).GetUtcNow()));
    }
}
