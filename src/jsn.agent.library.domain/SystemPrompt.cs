namespace jsn.agent.library.domain;

public sealed record SystemPrompt
{
    public string Identity { get; }
    public string Purpose { get; }
    public string Context { get; }
    public string? OutputFormat { get; private init; }
    public string? Environment { get; private init; }

    private SystemPrompt(string identity, string purpose, string context)
    {
        Identity = identity;
        Purpose = purpose;
        Context = context;
    }

    public static SystemPrompt Create(string identity, string purpose, string context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identity);
        ArgumentException.ThrowIfNullOrWhiteSpace(purpose);
        ArgumentException.ThrowIfNullOrWhiteSpace(context);

        return new SystemPrompt(identity.Trim(), purpose.Trim(), context.Trim());
    }

    public SystemPrompt WithOutputFormat(string outputFormat)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(outputFormat);
        return this with { OutputFormat = outputFormat.Trim() };
    }

    public SystemPrompt WithEnvironment(string environment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(environment);
        return this with { Environment = environment.Trim() };
    }

    public string Render()
    {
        var sections = new List<string>
        {
            Identity,
            $"Objetivo: {Purpose}",
            $"Contexto: {Context}",
        };

        if (OutputFormat is not null)
            sections.Add($"Formato da resposta: {OutputFormat}");

        if (Environment is not null)
            sections.Add($"Ambiente: {Environment}");

        return string.Join("\n\n", sections);
    }
}
