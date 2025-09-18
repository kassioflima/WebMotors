# WebMotors API

Uma API RESTful desenvolvida em .NET 9 para gerenciamento de anúncios de veículos, implementando arquitetura limpa (Clean Architecture) com padrões de projeto modernos e princípios SOLID.

## 🚀 Tecnologias Utilizadas

### Framework e Linguagem
- **.NET 9.0** - Framework principal
- **C# 12** - Linguagem de programação
- **ASP.NET Core Web API** - Framework web

### Banco de Dados
- **Entity Framework Core 9.0.9** - ORM
- **SQL Server 2022** - Banco de dados principal
- **Entity Framework Migrations** - Controle de versão do banco

### Autenticação e Segurança
- **ASP.NET Core Identity** - Sistema de autenticação
- **JWT Bearer Token** - Autenticação baseada em tokens
- **RSA 2048-bit** - Assinatura de tokens JWT
- **Políticas de Autorização** - Controle de acesso baseado em roles

### Logging e Monitoramento
- **Serilog** - Sistema de logging estruturado
- **Serilog.Sinks.File** - Logging em arquivos
- **Serilog.Sinks.Console** - Logging no console

### Documentação e Testes
- **Swagger/OpenAPI** - Documentação da API
- **xUnit** - Framework de testes
- **Moq** - Framework de mocking
- **AutoFixture** - Geração automática de dados de teste
- **Microsoft.AspNetCore.Mvc.Testing** - Testes de integração

### Containerização
- **Docker** - Containerização da aplicação
- **Docker Compose** - Orquestração de containers
- **SQL Server Linux** - Banco de dados containerizado

### Validação e Notificações
- **Flunt** - Biblioteca de validação e notificações
- **Domain Notifications** - Padrão de notificações do domínio

## 🏗️ Arquitetura e Padrões de Projeto

### Clean Architecture
O projeto segue os princípios da Clean Architecture com separação clara de responsabilidades:

```
01 - Presentation (WebMotors.API)
02 - Hosts (Docker)
03 - Application (WebMotors.Application)
04 - Domain (WebMotors.Domain + WebMotors.Domain.Shared)
05 - Infra (WebMotors.Data + WebMotors.Infra.CrossCutting.IoC)
06 - Testes (WebMotors.Tests)
```

### Padrões Implementados

#### 1. **CQRS (Command Query Responsibility Segregation)**
- **Commands**: `AnuncioInsertCommand`, `AnuncioUpdateCommand`, `AnuncioDeleteCommand`
- **Handlers**: `IAnuncioInsertHandler`, `IAnuncioUpdateHandler`, `IAnuncioDeleteHandler`
- **CommandHandler**: Classe base para processamento de comandos

#### 2. **Repository Pattern**
- **IRepositorio<TEntity>**: Interface base para repositórios
- **RepositorioBase<TEntity>**: Implementação base com Entity Framework
- **AnuncioEFRepositorio**: Repositório específico para entidade Anuncio

#### 3. **Unit of Work**
- **IUnitOfWork**: Interface para controle de transações
- **UnitOfWork**: Implementação com Entity Framework Context

#### 4. **Dependency Injection**
- **NativeInjectorBootStrapper**: Configuração centralizada de DI
- Registro de serviços por categoria (Handlers, Repositories, ExternalData)

#### 5. **Domain-Driven Design (DDD)**
- **Entities**: `Anuncio`, `ApplicationUser`
- **Value Objects**: Implementados através de validações com Flunt
- **Domain Services**: `AuthService`, `SeedDataService`

#### 6. **Specification Pattern**
- Validações implementadas através de contratos Flunt
- Validação de regras de negócio nas entidades

## 🔒 Princípios SOLID

### ✅ Single Responsibility Principle (SRP)
- Cada classe tem uma única responsabilidade
- Controllers apenas coordenam requisições
- Services focam em lógica específica
- Repositories gerenciam apenas acesso a dados

### ✅ Open/Closed Principle (OCP)
- Interfaces bem definidas permitem extensão
- Handlers podem ser facilmente adicionados
- Políticas de autorização configuráveis

### ✅ Liskov Substitution Principle (LSP)
- Implementações respeitam contratos das interfaces
- RepositorioBase pode ser substituído por implementações específicas

### ✅ Interface Segregation Principle (ISP)
- Interfaces específicas para cada responsabilidade
- `IAnuncioInsertHandler`, `IAnuncioUpdateHandler`, `IAnuncioDeleteHandler`
- Interfaces pequenas e focadas

### ✅ Dependency Inversion Principle (DIP)
- Dependências injetadas através de interfaces
- Alto nível não depende de baixo nível
- Inversão de controle através de DI Container

## 🛡️ Segurança

### Autenticação
- **JWT Tokens** com RSA 2048-bit para assinatura
- **ASP.NET Core Identity** para gerenciamento de usuários
- **Password Policies** configuradas (mínimo 6 caracteres, maiúscula, minúscula, dígito)
- **Lockout Policy** (5 tentativas, bloqueio por 5 minutos)

### Autorização
- **Role-based Authorization** (Admin, User)
- **Policy-based Authorization**:
  - `AdminOnly`: Apenas administradores
  - `UserOrAdmin`: Usuários e administradores
  - `ReadOnly`: Permissão de leitura

### Configurações de Segurança
- **HTTPS Redirection** habilitado
- **CORS** configurado adequadamente
- **Exception Handling** centralizado
- **Logging** de tentativas de acesso

### Validação de Dados
- **Model Validation** com Data Annotations
- **Domain Validation** com Flunt
- **Input Sanitization** através de validações

## 📁 Estrutura do Projeto

```
WebMotors/
├── WebMotors.API/                    # Camada de Apresentação
│   ├── Controllers/V1/               # Controllers da API
│   ├── Configurations/               # Configurações (Swagger, Security, Database)
│   ├── Middleware/                   # Middlewares customizados
│   ├── Models/                       # DTOs e Models de Request/Response
│   ├── Security/                     # Configurações de segurança
│   └── Services/                     # Serviços da aplicação
├── WebMotors.Application/            # Camada de Aplicação
│   └── Application/Anuncios/         # Casos de uso
├── WebMotors.Domain/                 # Camada de Domínio
│   └── Anuncios/
│       ├── Commands/                 # Comandos CQRS
│       ├── Entities/                 # Entidades de domínio
│       ├── Handlers/                 # Handlers CQRS
│       └── Repositories/             # Interfaces de repositório
├── WebMotors.Domain.Shared/          # Domínio Compartilhado
│   ├── Commands/                     # Base para comandos
│   ├── DomainNotifications/          # Sistema de notificações
│   ├── Entities/                     # Entidades base
│   └── UoW/                          # Unit of Work
├── WebMotors.Data/                   # Camada de Dados
│   ├── Context/                      # DbContext
│   ├── Mapping/                      # Configurações EF
│   ├── Migrations/                   # Migrações do banco
│   ├── Repositories/                 # Implementações de repositório
│   └── UoW/                          # Implementação UoW
├── WebMotors.Infra.CrossCutting.IoC/ # Injeção de Dependência
└── WebMotors.Tests/                  # Testes
    ├── Application/                  # Testes de aplicação
    ├── Controllers/                   # Testes de controllers
    ├── Domain/                       # Testes de domínio
    ├── Integration/                  # Testes de integração
    └── Services/                     # Testes de serviços
```

## 🚀 Como Executar

### Pré-requisitos
- .NET 9.0 SDK
- SQL Server (ou Docker)
- Docker e Docker Compose (opcional)

### Execução Local

1. **Clone o repositório**
```bash
git clone <repository-url>
cd WebMotors
```

2. **Configure a string de conexão**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "WebMotorsContext": "Server=localhost;Database=WebMotorsDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

3. **Execute as migrações**
```bash
cd WebMotors.API
dotnet ef database update
```

4. **Execute a aplicação**
```bash
dotnet run
```

### Execução com Docker

1. **Configure as variáveis de ambiente**
```bash
# docker.env
SA_PASSWORD=WebMotors@123
DB_NAME=WebMotorsDB
API_PORT_HTTP=6000
API_PORT_HTTPS=6001
```

2. **Execute com Docker Compose**
```bash
docker-compose up -d
```

## 📚 Documentação da API

Acesse a documentação Swagger em:
- **Local**: `https://localhost:6001/swagger`
- **Docker**: `http://localhost:6000/swagger`

### Endpoints Principais

#### Autenticação
- `POST /api/v1/auth/login` - Login
- `POST /api/v1/auth/register` - Registro
- `GET /api/v1/auth/me` - Informações do usuário

#### Anúncios
- `GET /api/v1/anuncio` - Listar anúncios
- `GET /api/v1/anuncio/{id}` - Obter anúncio por ID
- `POST /api/v1/anuncio` - Criar anúncio (Admin)
- `PUT /api/v1/anuncio` - Atualizar anúncio (Admin)
- `DELETE /api/v1/anuncio` - Excluir anúncio (Admin)

## 🧪 Testes

### Executar Testes
```bash
dotnet test
```

### Cobertura de Código
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 📊 Logging

O sistema utiliza Serilog para logging estruturado:
- **Console**: Logs em tempo real
- **Arquivo**: Logs diários em `logs/webmotors-{date}.log`
- **Retenção**: 30 dias de logs
- **Níveis**: Information, Warning, Error

## 🔧 Configurações

### JWT Configuration
```json
{
  "Jwt": {
    "Key": "WebMotorsSecretKey123456789012345678901234567890",
    "Issuer": "WebMotorsAPI",
    "Audience": "WebMotorsUsers",
    "ExpiryHours": 24
  }
}
```

### Serilog Configuration
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 👥 Equipe

- **Desenvolvedor**: [Kássio Fernandes Lima]
- **Email**: [kassioflima@gmail.com]

---

**WebMotors API** - Uma solução robusta e escalável para gerenciamento de anúncios de veículos, construída com as melhores práticas de desenvolvimento .NET.
