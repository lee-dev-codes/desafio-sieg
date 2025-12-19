# Desafio SIEG – API de Documentos Fiscais

API REST desenvolvida em **.NET 8** para recebimento, processamento e consulta de documentos fiscais eletrônicos (**NFe / CTe**), com persistência em **SQL Server**, testes automatizados e execução via **Docker**.

---

## Tecnologias Utilizadas

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server
- Docker & Docker Compose
- xUnit
- FluentAssertions
- Swagger / OpenAPI

---

## Funcionalidades

- Upload de arquivos XML fiscais (NFe / CTe)
- Detecção automática do tipo do documento
- Prevenção de duplicidade via hash do XML
- Persistência do XML original
- Listagem paginada e filtrável
- Consulta por ID
- Atualização parcial
- Remoção de documentos
- Testes unitários (Services e Controllers)
- Execução completa via Docker

---

## Estrutura do Projeto

```text
Desafio SIEG/
│
├── Desafio SIEG/
│   ├── Controllers
│   ├── Data
│   ├── DTOs
│   ├── Migrations
│   ├── Models
│   ├── Services
│   └── Program.cs
│
├── DesafioSIEG.Tests/
│   ├── Services
│   └── Controllers
│
├── Dockerfile
├── docker-compose.yml
└── README.md


## ▶️ Como Rodar a Aplicação

### Requisitos
- Docker
- Docker Compose

### Subindo a aplicação

Na raiz do projeto, execute:

```bash
docker-compose up --build

Aplicação fica disponível nos links:
- API: http://localhost:8080
- Swagger: http://localhost:8080/swagger

## Execução de testes via terminal

```bash
dotnet test

## Arquitetura e Modelagem

- Foi utilizada uma **API REST** com ASP.NET Core, seguindo boas práticas de separação de responsabilidades.
- A persistência foi feita com **SQL Server**, por ser um banco relacional amplamente utilizado em cenários fiscais e corporativos, oferecendo melhor desempenho e previsibilidade em consultas estruturadas, com uso eficiente de índices, filtros e ordenações, quando comparado a soluções NoSQL para esse tipo de carga.
- O **Entity Framework Core** foi utilizado para abstração do acesso a dados.
- A lógica de negócio foi centralizada em **Services**, evitando controllers inchados.
- O upload de XML foi tratado via **multipart/form-data**, conforme padrão para envio de arquivos.
- Foi criada uma camada de **DTOs** para evitar exposição direta das entidades do banco.
- A idempotência foi garantida através do cálculo de um **hash SHA256 do XML**, impedindo inserções duplicadas.

## Tratamento de Dados Sensíveis

- Os dados sensíveis presentes no XML (como CNPJ) não são expostos integralmente nos endpoints de listagem.
- O XML original é armazenado no banco para fins de rastreabilidade, mas não é retornado nos endpoints públicos.
- A conexão com o banco de dados utiliza **variáveis de ambiente** no Docker, evitando credenciais hardcoded no código.
- Poderia implementar melhorias como:
  - Mascaramento de dados sensíveis
  - Criptografia de campos específicos no banco
  - Autenticação e autorização via JWT

## Possíveis Melhorias

Caso houvesse mais tempo, algumas melhorias planejadas seriam:

- Implementar autenticação e autorização (JWT / OAuth2)
- Adicionar validação de schema XSD dos XMLs fiscais
- Melhorar cobertura de testes (cenários de erro e concorrência)
- Implementar criptografia para armazenamento do XML original