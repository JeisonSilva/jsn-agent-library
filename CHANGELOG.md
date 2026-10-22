# Histórico de versões

Todas as mudanças relevantes deste projeto ficam aqui. O formato segue o
[Keep a Changelog](https://keepachangelog.com/pt-BR/1.1.0/) e as versões seguem o
[Versionamento Semântico](https://semver.org/lang/pt-BR/).

## [Não lançado]

### Adicionado

- README, licença MIT e este histórico de versões (2026-10-15).
- Solution `jsn.agent.library` (`.slnx`) em .NET 10, com `global.json` fixando o SDK 10.0.112 (aceita versões mais novas do SDK 10).
- Biblioteca `jsn.agent.library.domain` (`net10.0`).
- Biblioteca `jsn.agent.library.infra` (`net10.0`), que referencia o domain.
- Projeto de testes xUnit `jsn.agent.library.domain.tests` (`net10.0`), que referencia só o domain.
- `.gitignore` padrão do .NET.
- Primeiro teste do `AgentCore` (`AgentCoreTest`): cria um agent com system prompt e tools e confere que ele processa a mensagem do usuário e devolve o texto do LLM.
- Processo de criação do agent no domain: `AgentCore` (criador, com `WithSystemPrompt`, `WithTools` e `Create()`) gera um `Agent`, que recebe o pedido com `WithHumanRequest` e executa com `ExecuteAsync`. Inclui `ILlm`, `ITool` (ainda sem membros) e `AgentResponse`.
- FluentAssertions 7.2.2 e Moq.AutoMock 4.0.3 no projeto de testes. O FluentAssertions fica na série 7 (Apache-2.0) porque a 8 tem licença comercial.
- Esqueleto do domain em DDD, organizado por funcionalidade de negócio: `Authoring`, `Publishing`, `Retirement`, `Discovery` e `SharedKernel`, com testes xUnit (2026-10-19).

### Alterado

- README com a descrição, a estrutura do projeto e como compilar e testar.
- README com a organização do domain.
- README com o diagrama de classes do componente agent (2026-10-21).

### Removido

- Esqueleto DDD do domain (`Authoring`, `Publishing`, `Retirement`, `Discovery` e `SharedKernel`) e seus testes. O domain fica só com o componente agent (2026-10-21).
- `UnitTest1.cs` de exemplo.
