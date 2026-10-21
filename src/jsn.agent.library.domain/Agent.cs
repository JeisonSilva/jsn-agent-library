namespace jsn.agent.library.domain;

public sealed class Agent
{
    private readonly ILlm _llm;
    private readonly string _systemPrompt;
    private readonly IReadOnlyList<ITool> _tools;
    private string _humanRequest = string.Empty;

    internal Agent(ILlm llm, string systemPrompt, IReadOnlyList<ITool> tools)
    {
        _llm = llm;
        _systemPrompt = systemPrompt;
        _tools = tools;
    }

    public Agent WithHumanRequest(string humanRequest)
    {
        _humanRequest = humanRequest;
        return this;
    }

    public async Task<AgentResponse> ExecuteAsync()
    {
        var response = await _llm.CompleteAsync(_systemPrompt, _humanRequest, _tools);
        return new AgentResponse(response);
    }
}
