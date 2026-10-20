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

- Esqueleto do domain em DDD, organizado por funcionalidade de negócio: `Authoring`, `Publishing`, `Retirement`, `Discovery` e `SharedKernel`, com testes xUnit (2026-10-19).

### Alterado

- README com a descrição, a estrutura do projeto e como compilar e testar.
- README com a organização do domain.

### Removido

- `UnitTest1.cs` de exemplo.
