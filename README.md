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
- **Redis** para cache distribuído

## 🚀 Stack

- ASP.NET Core 9  
- EF Core 9  
- MySQL (Clever Cloud)  
- Clean Architecture  

## 🚦 Rotas da API

### 🔐 Admin

| Método | Endpoint              | Ação                    |
|--------|------------------------|-------------------------|
| POST   | `/v1/admin/create`     | Cria um novo admin      |
| POST   | `/v1/admin/login`      | Login de admin (JWT)    |

---

### 🏢 Department

| Método | Endpoint               | Ação                                 |
|--------|-------------------------|--------------------------------------|
| GET    | `/api/v1/departments`   | Lista todos os departamentos ativos |

---

### 👨‍💼 Employee

| Método | Endpoint                                             | Ação                                     |
|--------|------------------------------------------------------|------------------------------------------|
| GET    | `/api/v1/employees`                                  | Lista todos os funcionários              |
| POST   | `/api/v1/employees`                                  | Cria um novo funcionário                 |
| GET    | `/api/v1/employees/by-department/{departmentId}`     | Lista funcionários por departamento      |
| GET    | `/api/v1/employees/{id}`                             | Busca funcionário por ID                 |
| PATCH  | `/api/v1/employees/{id}`                             | Atualiza parcialmente um funcionário     |
| PUT    | `/api/v1/employees/{id}/deactivate`                  | Desativa um funcionário                  |
| PUT    | `/api/v1/employees/{id}/activate`                    | Ativa um funcionário                     |

---

### 📊 Reports

| Método | Endpoint                                         | Ação                                           |
|--------|--------------------------------------------------|------------------------------------------------|
| GET    | `/api/v1/reports/summary/department`             | Resumo de funcionários por departamento        |
| GET    | `/api/v1/reports/summary/all-departments`        | Resumo de funcionários por todos os departamentos |


## ▶️ Rodando o projeto

```bash
dotnet build
dotnet ef database update
dotnet run
