using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class Capability : ValueObject
{
    private Capability(string name) => Name = name;

    public string Name { get; }

    public static Capability From(string? name)
    {
        var value = name?.Trim();

        return string.IsNullOrEmpty(value)
            ? throw new DomainException("O nome da capacidade é obrigatório.")
            : new(value);
    }

    public override string ToString() => Name;

    protected override IEnumerable<object?> GetEqualityComponents() => [Name.ToLowerInvariant()];
}
