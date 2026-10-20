using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class ModelSpec : ValueObject
{
    private ModelSpec(string name) => Name = name;

    public string Name { get; }

    public static ModelSpec From(string? name)
    {
        var value = name?.Trim();

        return string.IsNullOrEmpty(value)
            ? throw new DomainException("O modelo do agent é obrigatório.")
            : new(value);
    }

    public override string ToString() => Name;

    protected override IEnumerable<object?> GetEqualityComponents() => [Name];
}
