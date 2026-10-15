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
        const string systemPrompt = "Você é um assistente objetivo.";
        const string humanRequest = "Explique o que é um agent.";
        const string textoProcessado = "Um agent é um programa que age de forma autônoma.";

        var tools = new[] { _mocker.GetMock<ITool>().Object };

        Expression<Func<ILlm, Task<string>>> processar = x => x.CompleteAsync(
            systemPrompt,
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
}
