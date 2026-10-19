using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class ToolReference : ValueObject
{
    private ToolReference(string name) => Name = name;

    public string Name { get; }

    public static ToolReference From(string? name)
    {
        var value = name?.Trim();

        return string.IsNullOrEmpty(value)
            ? throw new DomainException("O nome da ferramenta é obrigatório.")
            : new(value);
    }

    public override string ToString() => Name;

    protected override IEnumerable<object?> GetEqualityComponents() => [Name.ToLowerInvariant()];
}
