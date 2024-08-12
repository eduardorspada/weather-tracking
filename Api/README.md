## Índice
* [Índice](#%C3%ADndice)
* [Introdução](#iVertion-Palladium)
    * [Autores](#Autores)
    * [Ambiente de desenvolvimento](#Ambiente-de-desenvolvimento)
        * [Windows](#Windows)
            * [iVertion WebApi](#iVertion-WebApi)
                * [.NET](#.NET)
            
# iVertion

## Autores

- [@Eduardo Rodrigo Spada](https://www.github.com/eduardorspada)

## Ambiente de desenvolvimento

### Windows

As configurações a seguir devem ser executadas no ambiente de desenvolvimento para executar as aplicações no Windows. Recomenda-se usar distribuições do Windows 10 ou 11.

### iVertion WebApi
O iVertion WebApi é o backend da aplicação, foi desenvolvido com .NET 8, MS SQL Server ou MySql como banco de dados (opcional MariaDB).

O projeto tem toda a estrutura com *Clean Architecture*, usa os padrões do *S.O.L.I.D* como princípios de boas práticas.

A abordagem *Code First* foi utilizada em conjunto com o ORM *Entity Framework*, isso tende a facilitar o deploy, assim como possíveis adaptações a certas necessidades como por exemplo a mudança da tecnologia de banco de dados empregada caso necessário.

A documentação é gerada automaticamente pelo *Swagger*, o que facilita o entendimento dos endpoints.

A proposta da API é servir um ERP robusto com inúmeras opções de módulos, e o objetivo é se tornar uns dos maiores ERPs do mercado na atualidade. 

#### .NET

Para o projeto API em ambiente de desenvolvimento, vamos instalar as dependências a seguir:

 - Versão Atual: 8.0.101
 - [Baixe aqui](https://download.visualstudio.microsoft.com/download/pr/cb56b18a-e2a6-4f24-be1d-fc4f023c9cc8/be3822e20b990cf180bb94ea8fbc42fe/dotnet-sdk-8.0.101-win-x64.exe)

#### Testando a instalação do .NET:

Teste a instalação do .NET com o seguinte comando:

```bash
dotnet --list-sdks
```

Se a versão *6.0.418* estiver na lista, isso significa que a instalação do .NET 6 foi um sucesso.

#### Entity Framework Core .NET Command-line Tools 6.0.13

Após a instalação do .NET 6, precisamos instalar a ferramenta do Entity Framework para habilitar as migrações de banco de dados. Para isso execute no terminal o seguinte comando. 

```bash
dotnet tool install --global dotnet-ef --version 8.0.1
```
#### Testando o Entity Framework
Agora vamos testar o Entity Framework.

Em um novo terminal execute o comando abaixo:

```bash
dotnet ef
```

A saída deve ser algo como isto:

```bash
        
                     _/\__       
               ---==/    \\      
         ___  ___   |.    \|\    
        | __|| __|  |  )   \\\   
        | _| | _|   \_/ |  //|\\ 
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 8.0.1

Usage: dotnet ef [options] [command]

Options:
  --version        Show version information
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  database    Commands to manage the database.
  dbcontext   Commands to manage DbContext types.
  migrations  Commands to manage migrations.

Use "dotnet ef [command] --help" for more information about a command.

```


#### Gerando os arquivos de migração com o Entity Framework

Para gerar os arquivos de migração, vamos executar alguns passos no terminal.

Certifique-se de estar na raiz do projeto **API**.

Execute o comando abaixo para gerar uma nova migração:

```bash
dotnet ef  migrations add initial --project iVertion.Infra.Data -s iVertion.WebApi -c ApplicationDbContext --verbose
```

Ao decorrer do desenvolvimento teremos novas migrações, o padrão adotado para o nome será *release-x.x.x*.

O projeto atualiza as migrações sempre no momento da execução, nesse caso não é necessário executar o update, mas para deixar documentado, caso execute, o comando é:

```bash
dotnet ef  database update --project iVertion.Infra.Data -s iVertion.WebApi -c ApplicationDbContext --verbose 
```

#### Executando a **API**

Após o ambiente de desenvolvimento do .NET configurado, vamos iniciar o projeto **API**, lembre-se de estar na raiz do projeto API para executar os comandos.

A seguir vamos atualizar os pacotes do projeto, dessa forma garantimos que as dependências sejam instaladas corretamente.

```bash
dotnet restore
```
Agora vamos compilar a aplicação para evitar erros na execução do projeto.

```bash
dotnet build
```
E por fim, vamos executar o projeto, o comando abaixo executa o projeto **API** a partir da camada *iVertion.WebApi*, o argumento *watch* é opcional, é utilizado especialmente para realizar algumas alterações com o projeto em execução, mas não necessário para o consumo da API em si.


```bash
dotnet watch run --project .\iVertion.WebApi\iVertion.WebApi.csproj
```

#### Acessando a documentação da **API**.

Quando o projeto estiver executando, acesse a documentação do Swagger no link abaixo:

- [iVertion WebApi - Swagger](http://localhost:5001/swagger/index.html)

Os testes podem ser executados diretamente no Swagger, mas não será possível salvar os parâmetros.

