namespace jsn.agent.library.domain;

public sealed class AgentCore
{
    private readonly ILlm _llm;
    private SystemPrompt? _systemPrompt;
    private IReadOnlyList<ITool> _tools = [];

    public AgentCore(ILlm llm)
    {
        _llm = llm;
    }

    public AgentCore WithSystemPrompt(SystemPrompt systemPrompt)
    {
        ArgumentNullException.ThrowIfNull(systemPrompt);
        _systemPrompt = systemPrompt;
        return this;
    }

    public AgentCore WithTools(IEnumerable<ITool> tools)
    {
        _tools = tools.ToList();
        return this;
    }

    public Agent Create()
    {
        if (_systemPrompt is null)
            throw new InvalidOperationException("O agent só pode ser criado com um system prompt. Use WithSystemPrompt.");

        return new Agent(_llm, _systemPrompt.Render(), _tools);
    }
}
