Este projeto é composto por duas partes principais:

API .NET 6

Implementada seguindo o padrão DDD (Domain-Driven Design) e Arquitetura Limpa.

Camadas bem definidas: Domain, Application (Services), Infrastructure (Repository), API.

Utiliza AutoMapper para mapear entidades para DTOs.

Banco de dados: SQL Server, base utilizada para exemplo: ClientesDb.

Configurado Swagger para documentação e testes da API.

Projeto Web CRUD

Interface web para gerenciamento de clientes (CRUD: Create, Read, Update, Delete).

Tecnologias: JavaScript para formatação de campos, validações e integração com API.

Consome os endpoints da API de forma RESTful.

Front-end leve e funcional, integrado ao back-end .NET.

Arquitetura

O projeto segue o padrão DDD + Clean Architecture, com as seguintes camadas:

Domain

Entidades, agregados e regras de negócio.

Application / Services

Contém a lógica de aplicação, manipula dados de domínio e orquestra operações.

Infrastructure / Repository

Acesso ao banco de dados SQL Server através de repositórios.

API

Exposição dos endpoints RESTful.

Configuração de Swagger para documentação.

API → Services → Repository → SQL Server


| Camada / Ferramenta | Tecnologia / Biblioteca                   |
| ------------------- | ----------------------------------------- |
| Backend             | .NET 6                                    |
| Arquitetura         | DDD, Clean Architecture                   |
| Mapeamento          | AutoMapper                                |
| Banco de dados      | SQL Server (`ClientesDb`)                 |
| Documentação        | Swagger (Swashbuckle)                     |
| Frontend            | HTML, CSS, JavaScript, bootstrap, JQuery  |
| Validação JS        | Formatação de campos e validações básicas |
| CRUD Web            | Consome API via AJAX / Fetch              |


Funcionalidades
API

CRUD completo para clientes.

Validações de dados via Services.

Retorna DTOs padronizados.

Documentação Swagger acessível em /swagger no ambiente local.

CRUD Web

Tela de cadastro, edição, exclusão e listagem de clientes.

Formatação de campos (ex: datas, CPF, telefone) com JavaScript.

Validações antes do envio para a API.

Atualização dinâmica da tabela sem recarregar a página.

- Configuração do Banco de Dados

Banco de exemplo: ClientesDb

Servidor SQL Server local: localhost\SQLEXPRESS (ou instância padrão)

Scripts de criação de tabelas estão no projeto Infrastructure / Migrations.

Connection string exemplo no appsettings.json:

Como Executar
1. API

Abra o projeto no Visual Studio 2022 ou VS Code.

Restaure os pacotes NuGet.

Configure a connection string para o seu SQL Server local.

Execute a aplicação (F5 ou dotnet run)

Acesse Swagger: https://localhost:7119/swagger/index.html

2. Web CRUD

Abra o projeto web no navegador (arquivo index.html ou via servidor local).

Certifique-se que a API está rodando.

A interface se conecta automaticamente aos endpoints da API.

Realize operações de cadastro, edição, exclusão e listagem.

- Observações Técnicas

AutoMapper é utilizado para reduzir o acoplamento entre entidades e DTOs.

Services implementam a lógica de validação antes de chamar o Repository.

Repository encapsula acesso ao banco, facilitando manutenção e testes.

Swagger está configurado para exibir corretamente todos os endpoints e DTOs.

JavaScript no front-end realiza:

Formatação de campos (datas, números, CPFs)

Validações antes de enviar dados para API

Atualização dinâmica da tabela de clientes

- Dependências

.NET 6 SDK

SQL Server Express ou Developer

Node.js (opcional, se usar ferramentas JS)

Browsers modernos (Chrome, Edge, Firefox)

- Boas Práticas Implementadas

Separação de camadas (DDD + Clean Architecture)

Validação de dados na camada Service

Documentação via Swagger

Configuração de conexão via appsettings.json

JavaScript apenas para UX e validação (não lógica de negócio)

Código limpo e modular