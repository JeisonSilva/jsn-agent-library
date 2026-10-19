using System.Globalization;
using jsn.agent.library.domain.SharedKernel;

namespace jsn.agent.library.domain.Publishing;

public sealed class SemanticVersion : ValueObject, IComparable<SemanticVersion>
{
    private SemanticVersion(int major, int minor, int patch)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
    }

    public int Major { get; }

    public int Minor { get; }

    public int Patch { get; }

    public static SemanticVersion Of(int major, int minor, int patch)
    {
        if (major < 0 || minor < 0 || patch < 0)
            throw new DomainException("Os números da versão não podem ser negativos.");

        return new(major, minor, patch);
    }

    public static SemanticVersion Parse(string? value)
    {
        var parts = value?.Trim().Split('.');

        if (parts is not { Length: 3 }
            || !int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out var major)
            || !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out var minor)
            || !int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out var patch))
            throw new DomainException($"A versão '{value}' não segue o formato major.minor.patch.");

        return new(major, minor, patch);
    }

    public int CompareTo(SemanticVersion? other)
    {
        if (other is null) return 1;

        var result = Major.CompareTo(other.Major);
        if (result != 0) return result;

        result = Minor.CompareTo(other.Minor);
        return result != 0 ? result : Patch.CompareTo(other.Patch);
    }

    public static bool operator <(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) < 0;

    public static bool operator >(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) > 0;

    public static bool operator <=(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) <= 0;

    public static bool operator >=(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) >= 0;

    public override string ToString() => $"{Major}.{Minor}.{Patch}";

    protected override IEnumerable<object?> GetEqualityComponents() => [Major, Minor, Patch];
}
