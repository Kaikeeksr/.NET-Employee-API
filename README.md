# 📦 .NET 9 API - (🏗️ Projeto em construção...)

API RESTful construída em **.NET 9**, seguindo a arquitetura em **3 camadas**:

- **API**: Camada de apresentação (Controllers, DTOs, Middlewares)  
- **Domain**: Regras de negócio, entidades e interfaces  
- **Infrastructure**: Implementações de repositórios e acesso a dados  

## 🎯 Objetivo do projeto

Aprender e praticar as bases fundamentais da arquitetura de uma API .NET, com foco em código limpo, organização e boas práticas.

## 🗄️ Banco de Dados

- **MySQL** hospedado na **Clever Cloud**  
- A conexão é configurada via `appsettings.json` ou `.env` (`.env-example`) e injetada via `DbContext`  

## 🚀 Stack

- ASP.NET Core 9  
- EF Core 9  
- MySQL (Clever Cloud)  
- Clean Architecture  

## 🚦 Rotas (Employees)

| Método | Endpoint                 | O que faz                         |
|--------|--------------------------|----------------------------------|
| GET    | `/v1/employees`           | Lista todos os funcionários      |
| GET    | `/v1/employees/{id}`      | Busca funcionário pelo ID        |
| POST   | `/v1/employees`           | Cria um novo funcionário         |
| PATCH  | `/v1/employees/{id}`      | Atualiza dados de um funcionário |
| PUT    | `/v1/employees/{id}/deactivate` | Desativa um funcionário      |
| PUT    | `/v1/employees/{id}/activate`   | Ativa um funcionário          |

## ▶️ Rodando o projeto

```bash
dotnet build
dotnet ef database update
dotnet run
