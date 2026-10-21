namespace jsn.agent.library.domain;

public interface ILlm
{
    Task<string> CompleteAsync(string systemPrompt, string humanRequest, IReadOnlyList<ITool> tools);
}
