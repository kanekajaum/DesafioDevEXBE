# Desafio de Teste para Desenvolvedor C# Pleno

## ✨ Objetivo
Desenvolver uma aplicação utilizando C#, seguindo os conceitos de **DDD (Domain-Driven Design)**, **Clean Code**, **SOLID**, implementando os padrões de projeto **Repository Genérico** e **UnitOfWork**.

A aplicação consome a API externa [football-data.org](https://www.football-data.org/) para exibir informações sobre **competições de futebol**, **equipes** e **partidas**. Também foi implementado um sistema de autenticação com cadastro de usuários.

---

## 📊 Funcionalidades

### 1. Listagem de Competições e Equipes
- Consome a API `football-data.org`
- Exibe nome, temporada e país das competições
- Mostra equipes participantes de cada competição
- Mostra partidas agendadas para cada equipe/competição

### 2. Autenticação e Cadastro de Usuários
- Cadastro com:
  - Nome
  - E-mail
  - Senha (com criptografia/hash)
- Autenticação com JWT
- Proteção de endpoints privados com autenticação

### 3. Banco de Dados
- Utiliza banco relacional (SQL Server)
- Migrations com EF Core
- Repositório genérico e UnitOfWork implementados

---

## 🔧 Tecnologias e Conceitos Utilizados

- ASP.NET Core 7
- DDD (Domain-Driven Design)
- SOLID & Clean Code
- Entity Framework Core
- Repository Pattern
- UnitOfWork Pattern
- JWT para autenticação
- Swagger para documentação da API
- xUnit e Moq para testes unitários (em progresso)

---

## 🔗 Endpoints Disponíveis

### ⚖️ Auth
- `POST /api/auth/login` - Login do usuário

### 🏆 Competicao
- `GET /ObterCompeticoes` - Lista competições
- `POST /PorArea/{areaId}` - Competições por área
- `GET /ObterJogosDeHoje` - Jogos do dia
- `GET /ObterJogosChampionsLeague` - Jogos da Champions League
- `GET /ObterJogosBrasileirao` - Jogos do Brasileirão
- `GET /ObterTopJogadoresBrasileirao` - Top jogadores

### ⚽ Futebol
- `GET /Times` - Lista todos os times
- `POST /Times/Id` - Busca time por ID
- `GET /TimesDecompeticoes/{competicaoId}` - Times de uma competição
- `GET /TimesJogosRecentes/{timeId}` - Jogos recentes de um time
- `GET /Areas` - Lista áreas disponíveis

### 👤 Usuario
- `POST /api/usuarios/cadastrar` - Cadastro de novo usuário
- `GET /api/usuarios` - Lista todos os usuários
- `POST /api/usuarios/validarLogin` - Valida login de usuário

---

## 🌐 Execução

1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
```

2. Configure as variáveis de ambiente (ou `appsettings.json`):
```json
"FootballApi": {
  "BaseUrl": "https://api.football-data.org/v4/",
  "ApiKey": "sua-api-key"
}
```

3. Rode as migrations:
```bash
dotnet ef database update
```

4. Rode a aplicação:
```bash
dotnet run --project DesafioDev.API
```

5. Acesse o Swagger:
```
https://localhost:{porta}/
```

---

## 📊 Status do Projeto
- [x] Listagem de Competições e Times
- [x] Cadastro e Login de Usuários
- [x] Repositório Genérico
- [x] UnitOfWork
- [x] Swagger

---

## 📄 Licença
Este projeto está sob a licença MIT.

