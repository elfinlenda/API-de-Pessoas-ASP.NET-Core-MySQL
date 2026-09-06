# API de Pessoas — ASP.NET Core + MySQL

CRUD simples de uma API REST em C#/.NET, com MySQL como banco de dados. Projeto feito para praticar ASP.NET Core: controllers, injeção de dependência, acesso a dados com ADO.NET e documentação via Swagger.

## Stack

- .NET 10 / ASP.NET Core Web API
- MySQL (MySql.Data)
- Swagger (Swashbuckle.AspNetCore)

## Estrutura

```
Controles/PessoaControle.cs      # rotas e validações
Modelos/Pessoa.cs                 # entidade (Codigo, Nome, Cidade, Idade)
Repositorios/PessoaRepositorio.cs # queries SQL
Program.cs
```

## Rodando localmente

1. Crie o banco:
   ```sql
   CREATE DATABASE api;
   USE api;

   CREATE TABLE pessoas (
       codigo INT AUTO_INCREMENT PRIMARY KEY,
       nome VARCHAR(100) NOT NULL,
       cidade VARCHAR(100) NOT NULL,
       idade INT NOT NULL
   );
   ```

2. Configure a connection string (via User Secrets, para não versionar credenciais):
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=api;User=root;Password=SUA_SENHA;"
   ```

3. Rode:
   ```bash
   dotnet watch run
   ```

4. Swagger em `http://localhost:5130/swagger` (a porta pode variar).

## Endpoints

| Método | Rota                | Descrição                    |
|--------|---------------------|-------------------------------|
| POST   | `/Pessoa`            | Cadastra pessoa                |
| GET    | `/Pessoa`            | Lista todas                    |
| PUT    | `/Pessoa/{codigo}`   | Atualiza pessoa                |
| DELETE | `/Pessoa/{codigo}`   | Remove pessoa                  |

Validações: nome e cidade obrigatórios, idade entre 0 e 120. Alteração e remoção verificam se o código existe antes de executar.

---
<sub>Baseado no curso gratuito de Ralf Lima no YouTube.</sub>
