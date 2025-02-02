# Projeto ASP.NET com SQLite (Consumindo API MakeUP)

Este projeto � uma API constru�da com ASP.NET Core e SQLite como banco de dados embutido. Abaixo est�o as orienta��es para configurar e rodar o projeto localmente.

## Pr�-requisitos

Certifique-se de ter as seguintes ferramentas instaladas no seu ambiente de desenvolvimento:

- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) com suporte para .NET Core
- [.NET SDK 6.0+](https://dotnet.microsoft.com/download/dotnet) (ou vers�o compat�vel)
- [SQLite](https://www.sqlite.org/download.html) (embutido no projeto, mas voc� pode precisar do cliente para inspe��o)

## Instala��o

1. **Clone o reposit�rio**:

    ```bash
    git clone https://github.com/seu-usuario/seu-repositorio.git
    cd seu-repositorio
    ```

2. **Instalar as depend�ncias**:

    O projeto depende de pacotes NuGet. Voc� pode restaurar as depend�ncias executando o comando:

    ```bash
    dotnet restore
    ```

## Banco de Dados SQLite

Este projeto utiliza um banco de dados SQLite embutido. Para configurar o banco e realizar as migra��es, siga as etapas abaixo.

### Gerar o Banco de Dados e as Migra��es

1. **Criar o arquivo de banco de dados**:

    O SQLite cria o banco de dados automaticamente quando o projeto � executado pela primeira vez. No entanto, � recomendado executar as migra��es para garantir que o banco de dados esteja na vers�o mais recente.

2. **Aplicar migra��es**:

    Ap�s restaurar as depend�ncias, voc� pode aplicar as migra��es para configurar o banco de dados SQLite corretamente. Execute o seguinte comando:

    ```bash
    dotnet ef migrations add Inicial
    dotnet ef database update
    ```

    - O primeiro comando cria as migra��es (se n�o houver uma pasta `Migrations`).
    - O segundo comando aplica as migra��es e cria o banco de dados `app.db`.

### Observa��o sobre o arquivo `app.db`

O banco de dados SQLite � armazenado no arquivo `app.db` na raiz do projeto. Este arquivo **n�o deve ser versionado no Git**. Certifique-se de que ele esteja inclu�do no seu arquivo `.gitignore`.

## Executando o Projeto

1. **Rodar o projeto**:

    Ap�s configurar o banco de dados e restaurar as depend�ncias, voc� pode rodar o projeto com o comando:

    ```bash
    dotnet run
    ```

    A API estar� dispon�vel no endere�o `https://localhost:7241/swagger/index.html` (ou o que for configurado na sua aplica��o).

## Autentica��o

A API usa autentica��o baseada em **JWT**. Ao realizar login, voc� receber� um token que deve ser usado nas requisi��es subsequentes.

1. **Obter um token JWT**:
   
    Fa�a uma requisi��o `POST` para o endpoint `/api/Auth/login` passando as credenciais do usu�rio. O token ser� retornado no corpo da resposta.

2. **Utilizar o token**:

    Para acessar endpoints que requerem autentica��o, adicione o token JWT no cabe�alho `Authorization` de suas requisi��es.

    Exemplo de uso no `Postman`:

    ```
    Authorization: Bearer <SEU_TOKEN_AQUI>
    ```

## Testando a API

Se voc� deseja testar a API localmente, pode usar ferramentas como **Postman** ou **cURL** para fazer requisi��es HTTP para os endpoints da API.

### Exemplos de requisi��es:

- **Obter produtos de maquiagem**:

    Requisi��o `GET` para `https://localhost:7241/api/v1/Product`, incluindo o cabe�alho de autoriza��o com o token JWT.

---

## Ilustra��es

Swagger pronto para alternar entre vers�es

![Exemplo](/img/version.png "Version")

Endpoints da API

![Exemplo](/img/endpoints.png "Endpoints")

## Licen�a

Este projeto est� licenciado sob a Licen�a MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
