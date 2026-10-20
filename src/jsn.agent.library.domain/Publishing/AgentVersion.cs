using jsn.agent.library.domain.Authoring;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Publishing;

public sealed class AgentVersion : Entity<SemanticVersion>
{
    internal AgentVersion(
        SemanticVersion number,
        Instructions instructions,
        ModelSpec model,
        IEnumerable<ToolReference> tools,
        IEnumerable<Capability> capabilities,
        DateTimeOffset publishedOn) : base(number)
    {
        Instructions = instructions;
        Model = model;
        Tools = [.. tools.Distinct()];
        Capabilities = [.. capabilities.Distinct()];
        PublishedOn = publishedOn;
    }

    public SemanticVersion Number => Id;

    public Instructions Instructions { get; }

    public ModelSpec Model { get; }

    public IReadOnlyList<ToolReference> Tools { get; }

    public IReadOnlyList<Capability> Capabilities { get; }

    public DateTimeOffset PublishedOn { get; }
}
