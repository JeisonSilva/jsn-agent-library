using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class AgentName : ValueObject
{
    public const int MaxLength = 100;

    private AgentName(string value) => Value = value;

    public string Value { get; }

    public static AgentName From(string? value)
    {
        var name = value?.Trim();

        if (string.IsNullOrEmpty(name))
            throw new DomainException("O nome do agent é obrigatório.");

        if (name.Length > MaxLength)
            throw new DomainException($"O nome do agent deve ter no máximo {MaxLength} caracteres.");

        return new(name);
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents() => [Value];
}
