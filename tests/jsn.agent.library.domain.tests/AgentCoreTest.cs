using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace jsn.agent.library.domain.tests;

public class AgentCoreTest
{
    private readonly AutoMocker _mocker = new();

    [Fact]
    public async Task Create_ComSystemPromptETools_DeveGerarUmAgentQueProcessaAMensagemDoUsuario()
    {
        // Arrange
        var systemPrompt = SystemPrompt
            .Create("Você é um assistente técnico.", "Explicar conceitos de forma objetiva.", "Atende desenvolvedores iniciantes.")
            .WithOutputFormat("Texto corrido, no máximo três frases.");
        const string humanRequest = "Explique o que é um agent.";
        const string textoProcessado = "Um agent é um programa que age de forma autônoma.";

        var tools = new[] { _mocker.GetMock<ITool>().Object };

        Expression<Func<ILlm, Task<string>>> processar = x => x.CompleteAsync(
            systemPrompt.Render(),
            humanRequest,
            It.Is<IReadOnlyList<ITool>>(t => t.SequenceEqual(tools)));

        _mocker.Setup(processar).ReturnsAsync(textoProcessado);

        // Act
        var agent = _mocker.CreateInstance<AgentCore>()
            .WithSystemPrompt(systemPrompt)
            .WithTools(tools)
            .Create();

        var result = await agent
            .WithHumanRequest(humanRequest)
            .ExecuteAsync();

        // Assert
        agent.Should().NotBeNull();
        result.Response.Should().Be(textoProcessado);
        _mocker.Verify(processar, Times.Once);
    }

    [Fact]
    public void Create_SemSystemPrompt_DeveLancarExcecao()
    {
        // Arrange
        var core = _mocker.CreateInstance<AgentCore>().WithTools([]);

        // Act
        var act = () => core.Create();

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}
