using FluentAssertions;

namespace jsn.agent.library.domain.tests;

public class SystemPromptTest
{
    [Theory]
    [InlineData(null, "propósito", "contexto")]
    [InlineData("identidade", " ", "contexto")]
    [InlineData("identidade", "propósito", "")]
    public void Create_SemCampoObrigatorio_DeveLancarExcecao(string? identity, string purpose, string context)
    {
        // Act
        var act = () => SystemPrompt.Create(identity!, purpose, context);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Render_ComApenasOsCamposObrigatorios_DeveMontarIdentidadeObjetivoEContexto()
    {
        // Arrange
        var prompt = SystemPrompt.Create("Você é um revisor.", "Revisar textos.", "Textos em português.");

        // Act
        var texto = prompt.Render();

        // Assert
        texto.Should().Be("Você é um revisor.\n\nObjetivo: Revisar textos.\n\nContexto: Textos em português.");
    }

    [Fact]
    public void Render_ComCamposOpcionais_DeveIncluirFormatoEAmbienteNaOrdem()
    {
        // Arrange
        var prompt = SystemPrompt
            .Create("Você é um revisor.", "Revisar textos.", "Textos em português.")
            .WithOutputFormat("Lista numerada.")
            .WithEnvironment("Data: 2026-10-21.");

        // Act
        var texto = prompt.Render();

        // Assert
        texto.Should().EndWith("Formato da resposta: Lista numerada.\n\nAmbiente: Data: 2026-10-21.");
    }
}
