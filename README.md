# jsn.agent.library

Biblioteca de agents em .NET 10, com as camadas domain e infra e testes xUnit.

Parte do projeto `agents-pipeline`. As bibliotecas ainda não têm código.

## Estrutura

| Projeto | Função |
| --- | --- |
| `src/jsn.agent.library.domain` | Camada de domínio |
| `src/jsn.agent.library.infra` | Camada de infraestrutura; referencia o domain |
| `tests/jsn.agent.library.domain.tests` | Testes xUnit do domain |

## Requisitos

.NET SDK 10. O `global.json` fixa o SDK 10.0.112 e aceita versões mais novas do SDK 10.

## Compilar e testar

```bash
dotnet build jsn.agent.library.slnx
dotnet test jsn.agent.library.slnx
```

## Licença

[MIT](LICENSE)

## Histórico de versões

Veja o [CHANGELOG](CHANGELOG.md).
