namespace jsn.agent.library.domain;

public sealed class AgentCore
{
    private readonly ILlm _llm;
    private string _systemPrompt = string.Empty;
    private IReadOnlyList<ITool> _tools = [];

    public AgentCore(ILlm llm)
    {
        _llm = llm;
    }

    public AgentCore WithSystemPrompt(string systemPrompt)
    {
        _systemPrompt = systemPrompt;
        return this;
    }

    public AgentCore WithTools(IEnumerable<ITool> tools)
    {
        _tools = tools.ToList();
        return this;
    }

    public Agent Create() => new(_llm, _systemPrompt, _tools);
}
