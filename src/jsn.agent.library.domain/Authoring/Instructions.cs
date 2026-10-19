using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Authoring;

public sealed class Instructions : ValueObject
{
    public const int MaxLength = 20_000;

    private Instructions(string value) => Value = value;

    public string Value { get; }

    public static Instructions From(string? value)
    {
        var text = value?.Trim();

        if (string.IsNullOrEmpty(text))
            throw new DomainException("As instruções do agent são obrigatórias.");

        if (text.Length > MaxLength)
            throw new DomainException($"As instruções do agent devem ter no máximo {MaxLength} caracteres.");

        return new(text);
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents() => [Value];
}
