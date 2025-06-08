# 📦 .NET 9 API

API RESTful construída em **.NET 9**, seguindo a arquitetura em **3 camadas**:

- **API**: Camada de apresentação (Controllers, DTOs, Middlewares)  
- **Domain**: Regras de negócio, entidades e interfaces  
- **Infrastructure**: Implementações de repositórios e acesso a dados  

## 🗄️ Banco de Dados

- **MySQL** hospedado na **Clever Cloud**  
- A conexão é configurada via `appsettings.json` ou .env (`.env-example`) e injetada via `DbContext`  

## 🚀 Stack

- ASP.NET Core 9  
- EF Core 9  
- MySQL (Clever Cloud)  
- Clean Architecture  

## ▶️ Rodando o projeto

```bash
dotnet build
dotnet ef database update
dotnet run
