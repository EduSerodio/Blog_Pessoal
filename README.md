# BlogPessoal 📝

Este projeto é uma aplicação de um blog pessoal que permite aos usuários criar postagens, associá-las a temas e compartilhar suas histórias online. A aplicação também incorpora autenticação com token Bearer JWT para garantir a segurança das postagens dos usuários.

## Funcionalidades

- **Criação de Postagens**: Os usuários podem criar e publicar suas postagens.
- **Associação a Temas**: Categorize as postagens associando-as a temas específicos.
- **Listagem de Postagens**: Visualize as postagens de outros usuários.
- **Autenticação com Token Bearer JWT**: Proteja sua aplicação e as postagens dos usuários com autenticação JWT.
- **Testes Unitários**: O projeto inclui testes unitários para garantir a qualidade do código.

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- Token Bearer JWT para autenticação
- API RESTful
- Testes de unidade

## Pré-requisitos

- [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/) ou outra IDE de sua preferência.
- [.NET Core](https://dotnet.microsoft.com/download/dotnet-core)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) ou outro sistema de gerenciamento de banco de dados.

## Instalação e Uso

1. Clone o repositório:

   ```bash
   git clone https://github.com/EduSerodio/Blog_Pessoal.git
   ```
2. Abra o projeto em seu ambiente de desenvolvimento.

3. Configure a conexão com o banco de dados no arquivo appsettings.json.

4. Execute a aplicação.

Agora, a aplicação estará disponível em **http://localhost:5000**.

## Testes Unitários

O projeto inclui testes unitários para garantir a qualidade do código. Para executar os testes, use o comando:
```bash
dotnet test
```
## Endpoints da API

### Usuário

GET /usuarios/all: Retorna a lista de usuários.

GET /usuarios/{id}: Retorna um usuário específico por ID.

POST /usuarios/cadastrar: Cria um novo usuário.

POST /usuarios/logar: Faz login do usuário.

PUT /usuarios/atualizar: Atualiza um usuário existente.

### Tema

GET /temas: Retorna a lista de temas.

GET /temas/{id}: Retorna um tema específico por ID.

GET /temas/descricao/{descricao}: Retorna um tema específico pelo Descrição.

POST /temas: Cria um novo tema.

PUT /temas: Atualiza um tema existente.

DELETE /temas/{id}: Exclui um tema.

### Postagem

GET /postagens: Retorna a lista de postagens.

GET /postagens/{id}: Retorna uma postagem específica por ID.

GET /postagens/titulo/{titulo}: Retorna uma postagem específica pelo Título.

POST /postagens: Cria uma nova postagem.

PUT /postagens: Atualiza uma postagem existente.

DELETE /postagens/{id}: Exclui uma postagem.

## Autenticação

Para acessar as rotas protegidas, você precisará incluir um token JWT válido nas solicitações. Esse token será gerado ao efetuar o login do usuário.

Certifique-se de incluir o token no cabeçalho das requisições protegidas com o cabeçalho **Authorization** no formato **Bearer seu-token-jwt-aqui**.

## Contribuição

Sinta-se à vontade para contribuir para o projeto. Você pode abrir problemas, enviar solicitações pull e melhorar a aplicação.
