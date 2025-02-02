# Projeto ASP.NET com SQLite

Este projeto é uma API construída com ASP.NET Core e SQLite como banco de dados embutido. Abaixo estão as orientações para configurar e rodar o projeto localmente.

## Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas no seu ambiente de desenvolvimento:

- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) com suporte para .NET Core
- [.NET SDK 6.0+](https://dotnet.microsoft.com/download/dotnet) (ou versão compatível)
- [SQLite](https://www.sqlite.org/download.html) (embutido no projeto, mas você pode precisar do cliente para inspeção)

## Instalação

1. **Clone o repositório**:

    ```bash
    git clone https://github.com/seu-usuario/seu-repositorio.git
    cd seu-repositorio
    ```

2. **Instalar as dependências**:

    O projeto depende de pacotes NuGet. Você pode restaurar as dependências executando o comando:

    ```bash
    dotnet restore
    ```

## Banco de Dados SQLite

Este projeto utiliza um banco de dados SQLite embutido. Para configurar o banco e realizar as migrações, siga as etapas abaixo.

### Gerar o Banco de Dados e as Migrações

1. **Criar o arquivo de banco de dados**:

    O SQLite cria o banco de dados automaticamente quando o projeto é executado pela primeira vez. No entanto, é recomendado executar as migrações para garantir que o banco de dados esteja na versão mais recente.

2. **Aplicar migrações**:

    Após restaurar as dependências, você pode aplicar as migrações para configurar o banco de dados SQLite corretamente. Execute o seguinte comando:

    ```bash
    dotnet ef migrations add Inicial
    dotnet ef database update
    ```

    - O primeiro comando cria as migrações (se não houver uma pasta `Migrations`).
    - O segundo comando aplica as migrações e cria o banco de dados `app.db`.

### Observação sobre o arquivo `app.db`

O banco de dados SQLite é armazenado no arquivo `app.db` na raiz do projeto. Este arquivo **não deve ser versionado no Git**. Certifique-se de que ele esteja incluído no seu arquivo `.gitignore`.

## Executando o Projeto

1. **Rodar o projeto**:

    Após configurar o banco de dados e restaurar as dependências, você pode rodar o projeto com o comando:

    ```bash
    dotnet run
    ```

    A API estará disponível no endereço `http://localhost:5000` (ou o que for configurado na sua aplicação).

## Autenticação

A API usa autenticação baseada em **JWT**. Ao realizar login, você receberá um token que deve ser usado nas requisições subsequentes.

1. **Obter um token JWT**:
   
    Faça uma requisição `POST` para o endpoint `/login` passando as credenciais do usuário. O token será retornado no corpo da resposta.

2. **Utilizar o token**:

    Para acessar endpoints que requerem autenticação, adicione o token JWT no cabeçalho `Authorization` de suas requisições.

    Exemplo de uso no `Postman`:

    ```
    Authorization: Bearer <SEU_TOKEN_AQUI>
    ```

## Testando a API

Se você deseja testar a API localmente, pode usar ferramentas como **Postman** ou **cURL** para fazer requisições HTTP para os endpoints da API.

### Exemplos de requisições:

- **Obter produtos de maquiagem**:

    Requisição `GET` para `http://localhost:5000/api/v1/products`, incluindo o cabeçalho de autorização com o token JWT.

---

## Contribuindo

Se você deseja contribuir para o projeto, siga as etapas abaixo:

1. Fork o repositório.
2. Crie uma branch para a sua feature (`git checkout -b feature/minha-nova-feature`).
3. Faça commit das suas alterações (`git commit -am 'Adicionando uma nova feature'`).
4. Push para a branch (`git push origin feature/minha-nova-feature`).
5. Abra um Pull Request explicando as alterações realizadas.

## Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
