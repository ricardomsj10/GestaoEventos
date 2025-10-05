# Diretrizes para Revisão de Pull Requests

Este documento define as diretrizes que o GitHub Copilot deve seguir ao revisar Pull Requests neste projeto.

## Geral

- Verificar se o código segue os princípios SOLID
- Garantir que o código está bem documentado e com comentários claros quando necessário
- Verificar se foram adicionados testes apropriados para novas funcionalidades
- Avaliar a cobertura de testes para mudanças em código existente

## Segurança

- Verificar possíveis vulnerabilidades de segurança
- Garantir que dados sensíveis não estão expostos
- Verificar se as autorizações e autenticações estão sendo aplicadas corretamente
- Avaliar possíveis injeções de SQL em queries

## Performance

- Identificar possíveis problemas de performance em queries do Entity Framework
- Verificar uso adequado de async/await em operações I/O
- Avaliar o uso correto de caching quando apropriado
- Identificar potenciais memory leaks

## Convenções de Código C#

- Seguir as convenções de nomenclatura do C#
  - PascalCase para classes, métodos e propriedades
  - camelCase para variáveis locais e parâmetros
  - _camelCase para campos privados
- Usar var apenas quando o tipo é óbvio pelo contexto
- Evitar métodos com mais de 20 linhas
- Manter classes com responsabilidade única
- Usar injeção de dependência apropriadamente

## Controllers

- Seguir o padrão RESTful para endpoints
- Usar os verbos HTTP corretamente (GET, POST, PUT, DELETE)
- Implementar tratamento adequado de erros
- Retornar códigos HTTP apropriados
- Validar inputs adequadamente
- Usar atributos de autorização conforme necessário

## Models

- Implementar validações usando DataAnnotations
- Manter as entidades simples e focadas
- Usar tipos apropriados para cada propriedade
- Implementar relações entre entidades corretamente

## Views

- Seguir as convenções do Razor
- Evitar lógica complexa nas views
- Usar layouts e partial views apropriadamente
- Implementar validações client-side quando necessário

## Banco de Dados

- Verificar se as migrations estão corretas
- Garantir que índices apropriados foram criados
- Verificar se as foreign keys estão configuradas corretamente
- Avaliar o impacto das mudanças no schema

## Commits

- Verificar se as mensagens dos commits seguem o padrão convencional
  - feat: para novas funcionalidades
  - fix: para correções de bugs
  - docs: para documentação
  - style: para formatação de código
  - refactor: para refatorações
  - test: para adição/modificação de testes
  - chore: para manutenção de código

## Testes

- Verificar se os testes seguem o padrão AAA (Arrange, Act, Assert)
- Garantir que os testes são independentes
- Verificar se os testes cobrem casos de sucesso e falha
- Avaliar se os mocks são utilizados apropriadamente

## Qualidade de Código

- Manter a complexidade ciclomática baixa (preferencialmente < 10)
- Evitar duplicação de código
- Seguir princípios DRY (Don't Repeat Yourself)
- Usar padrões de design apropriados

## Acessibilidade

- Garantir que as views seguem práticas de acessibilidade web
- Verificar uso apropriado de ARIA labels
- Confirmar que a navegação por teclado funciona corretamente

## Observações Finais

- Identificar potenciais melhorias mesmo quando o código está funcionando
- Sugerir refatorações quando apropriado
- Elogiar boas práticas e soluções elegantes
- Manter um tom construtivo nas revisões