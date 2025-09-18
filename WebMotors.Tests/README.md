# WebMotors Tests

Este projeto contém testes unitários para a aplicação WebMotors utilizando XUnit com asserts padrão.

## Estrutura dos Testes

### 📁 Services
- **AuthServiceBasicTests**: Testes básicos para o serviço de autenticação
  - Testes de funcionalidade básica
  - Testes com teoria (Theory) para múltiplos cenários
  - Testes de strings e coleções

### 📁 Controllers  
- **AuthControllerTests**: Testes básicos para o controller de autenticação
  - Testes de criação de instâncias
  - Testes de funcionalidade básica
  - Testes de operações matemáticas e strings

### 📁 Domain/Entities
- **AnuncioEntityTests**: Testes para a entidade de domínio Anuncio
  - Testes de criação com dados válidos
  - Testes de criação com strings nulas/vazias
  - Testes de métodos Update e Atualisar
  - Testes com teoria (Theory) para diferentes cenários
  - Verificação de propriedades readonly

### 📁 Domain/Commands
- **AnuncioInsertCommandTests**: Testes para o comando de inserção
  - Validação de campos obrigatórios
  - Validação de tamanhos de campos
  - Validação de valores numéricos
  - Testes com teoria para múltiplos cenários
- **AnuncioUpdateCommandTests**: Testes para o comando de atualização
  - Validação de ID obrigatório
  - Validação de campos obrigatórios
  - Testes com teoria para múltiplos cenários
- **AnuncioDeleteCommandTests**: Testes para o comando de exclusão
  - Validação de ID obrigatório
  - Testes com teoria para diferentes IDs

### 📁 Application/Handlers
- **AnuncioInsertHandlerTests**: Testes para o handler de inserção
  - Testes de criação de instâncias
  - Testes de processamento de comandos válidos/inválidos
  - Testes com comandos nulos
- **AnuncioUpdateHandlerTests**: Testes para o handler de atualização
  - Testes de criação de instâncias
  - Testes de atualização de anúncios existentes/inexistentes
  - Testes com comandos inválidos
- **AnuncioDeleteHandlerTests**: Testes para o handler de exclusão
  - Testes de criação de instâncias
  - Testes de exclusão de anúncios existentes/inexistentes
  - Testes com comandos inválidos

### 📁 Integration
- **IntegrationTests**: Testes de integração simulados
  - Testes de conexão com banco de dados
  - Testes de endpoints de API
  - Testes de métodos HTTP

## Tecnologias Utilizadas

- **XUnit**: Framework de testes com asserts padrão
- **Moq**: Biblioteca para criação de mocks
- **AutoFixture**: Biblioteca para geração automática de dados de teste
- **Microsoft.AspNetCore.Mvc.Testing**: Para testes de integração
- **Microsoft.EntityFrameworkCore.InMemory**: Para testes com banco em memória

## Como Executar os Testes

### Via Terminal
```bash
# Executar todos os testes
dotnet test

# Executar testes com detalhes
dotnet test --verbosity normal

# Executar testes específicos
dotnet test --filter "ClassName=AuthServiceBasicTests"
```

### Via Visual Studio
1. Abra o Test Explorer (Test > Test Explorer)
2. Execute todos os testes ou testes específicos
3. Visualize os resultados e cobertura de código

## Cobertura de Código

Para gerar relatório de cobertura:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Estrutura dos Testes

Cada teste segue o padrão **Arrange-Act-Assert**:

```csharp
[Fact]
public void TestName_ShouldDoSomething()
{
    // Arrange - Preparar dados e configurações
    var expected = "expected value";
    
    // Act - Executar a ação sendo testada
    var actual = SomeMethod();
    
    // Assert - Verificar o resultado usando XUnit asserts
    Assert.Equal(expected, actual);
}
```

## Asserts Disponíveis no XUnit

### Asserts Básicos
```csharp
Assert.Equal(expected, actual);           // Igualdade
Assert.NotEqual(expected, actual);        // Desigualdade
Assert.True(condition);                   // Verdadeiro
Assert.False(condition);                  // Falso
Assert.NotNull(object);                    // Não nulo
Assert.Null(object);                      // Nulo
```

### Asserts de Strings
```csharp
Assert.Contains("substring", text);       // Contém substring
Assert.DoesNotContain("substring", text); // Não contém substring
Assert.StartsWith("prefix", text);        // Começa com
Assert.EndsWith("suffix", text);          // Termina com
```

### Asserts de Coleções
```csharp
Assert.Contains(item, collection);        // Contém item
Assert.DoesNotContain(item, collection);  // Não contém item
Assert.Empty(collection);                  // Coleção vazia
Assert.NotEmpty(collection);               // Coleção não vazia
```

### Asserts de Exceções
```csharp
Assert.Throws<ExceptionType>(() => method());
Assert.ThrowsAsync<ExceptionType>(() => asyncMethod());
```

## Testes com Teoria (Theory)

Para testar múltiplos cenários:

```csharp
[Theory]
[InlineData(1, 2, 3)]
[InlineData(5, 5, 10)]
[InlineData(-1, 1, 0)]
public void Addition_ShouldWork(int a, int b, int expected)
{
    var result = a + b;
    Assert.Equal(expected, result);
}
```

## Próximos Passos

1. **Expandir testes unitários** para todos os serviços e handlers
2. **Implementar testes de integração** completos
3. **Adicionar testes de performance** para endpoints críticos
4. **Configurar CI/CD** para execução automática de testes
5. **Implementar testes de carga** para APIs

## Notas Importantes

- Os testes utilizam apenas XUnit com asserts padrão (sem FluentAssertions)
- Para testes mais complexos com Identity, considere usar `Microsoft.AspNetCore.Identity.Testing`
- Para testes de integração completos, configure um banco de dados de teste
- Mantenha os testes independentes e isolados
- Use mocks para dependências externas
- Prefira `Assert.Equal(expected, actual)` ao invés de `Assert.True(expected == actual)`
