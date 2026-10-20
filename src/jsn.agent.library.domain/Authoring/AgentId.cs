using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class AgentId : ValueObject
{
    private AgentId(Guid value) => Value = value;

    public Guid Value { get; }

    public static AgentId New() => new(Guid.NewGuid());

    public static AgentId From(Guid value) =>
        value == Guid.Empty ? throw new DomainException("O id do agent não pode ser vazio.") : new(value);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object?> GetEqualityComponents() => [Value];
}
