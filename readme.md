# Documentação da Biblioteca Utils

A biblioteca **`Utils`** é um conjunto de utilitários e padrões de software para C\#, com o objetivo de promover a criação de código **limpo, expressivo e robusto** em aplicações .NET. Ela é fundamentada em princípios de programação funcional para gerenciamento de resultados e na imutabilidade para definição de objetos de valor.

-----

## Estrutura e Módulos

| Módulo | Descrição |
| :--- | :--- |
| **`Utils.Results`** | Implementação do Padrão Result (Either) para gerenciamento explícito de erros. |
| **`Utils.ValueObjects`** | Coleção de *Records* imutáveis para tipos de valor comuns (`Email`, `CNPJ`). |
| **`Utils.AspNet`** (Addon) | **Integração com ASP.NET Core**, fornecendo *middleware* e *mappers* para converter `Result<T>` em respostas HTTP (`IResult`). |

-----

## 1\. Módulo Utils.Results: Gerenciamento Funcional de Erros

**(Manter a seção 1. Módulo Utils.Results, incluindo o Subtítulo "Extensibilidade de Erros (Módulos Customizados)")**

-----

## 2\. Módulo Utils.ValueObjects: Imutabilidade de Dados

**(Manter a seção 2. Módulo Utils.ValueObjects)**

-----

## 3\. Módulo Utils.AspNet: Integração com ASP.NET Core

O módulo **`Utils.AspNet`** (namespace `Utils.Results.AspNet`) fornece um conjunto de classes e métodos de extensão para integrar de forma limpa o **Padrão Result** do seu domínio com o *pipeline* de *endpoints* do ASP.NET Core. Ele resolve o desafio de converter um `Result<T>` interno em uma resposta HTTP externa (`IResult`).

### 3.1. Adaptadores de Endpoint (`EndpointResult<TValue>` e `EndpointResult`)

As classes **`EndpointResult<TValue>`** e **`EndpointResult`** atuam como adaptadores. Elas implementam a interface `Microsoft.AspNetCore.Http.IResult`, permitindo que métodos de *endpoints* (sejam *Minimal APIs* ou *Controllers*) retornem diretamente um tipo `Result<T>` ou `Result`.

| Recurso | Descrição |
| :--- | :--- |
| **Conversão Implícita** | Permite que você retorne um objeto `Result<T>` ou `Error` diretamente do seu *endpoint* sem *casting* explícito. |
| **Mapeamento Automático**| Internamente, ele converte um **Sucesso** para um `SuccessResult<T>` e uma **Falha** para um `ErrorResult`. |

**Exemplo de Uso em um Endpoint:**

```csharp
// Em vez de retornar IActionResult ou StatusCode, você retorna EndpointResult<T>
public EndpointResult<User> Get(int id, [FromServices] UserService service)
{
    // A conversão implícita de Result<User> para EndpointResult<User> acontece aqui.
    return service.GetUser(id);
}
```

### 3.2. Mapeamento de Status HTTP

O módulo utiliza serviços de mapeamento para traduzir a semântica interna do domínio (`Error` ou `Success`) para a sintaxe da web (Status Code, *Problem Details*).

#### Mapeamento de Falhas (`ErrorMappingService` e `ErrorResult`)

O **`ErrorResult`** serializa todas as falhas no formato padrão **Problem Details (RFC 7807)**.

  * **Serviço de Mapeamento:** O **`ErrorMappingService`** é um serviço *Singleton* configurável que mantém um dicionário de mapeamentos: **Tipo do Erro (`System.Type`)** $\\to$ **Detalhes HTTP (`Status Code`, `Title`, `Problem Type` URL)**.
  * **Tratamento de Falha:**
      * O `Status Code` é determinado pelo mapeamento configurado (ex: `Error.NotFound` $\\to$ **404**).
      * Erros não mapeados explicitamente retornam o padrão **500 Internal Server Error**.
      * Detalhes de Validação (`ErrorDetail`) são incluídos na extensão `"errors"` do *Problem Details*.

#### Mapeamento de Sucessos (`SuccessMappingService` e `SuccessResult<T>`)

O **`SuccessResult<T>`** mapeia o `Success` interno para o *Status Code* e corpo da resposta.

  * **Status Code:** Determinado pelo tipo de `Success` retornado (e.g., `Success.Created()` $\\to$ **201 Created**).
  * **Corpo da Resposta:** No sucesso, o `TValue` é retornado no corpo da resposta, seguindo o `Status Code` mapeado.

### 3.3. Configuração no Pipeline (DI e Startup)

O registro e a inicialização dos serviços de mapeamento são realizados através de métodos de extensão no *startup* da aplicação.

| Método de Extensão | Função |
| :--- | :--- |
| **`services.AddEndpointResultMappers()`** | Registra os serviços `SuccessMappingService` e `ErrorMappingService` no contêiner de Injeção de Dependência (DI). Permite configurar **mapeamentos customizados** no *startup*. |
| **`app.UseEndpointResultMappers()`** | Força a inicialização dos serviços de mapeamento logo após a construção do `WebApplication` para garantir que todos os mapeamentos estejam prontos antes da primeira requisição. |
| **`app.OutputErrorsList()`** | **Ferramenta de Desenvolvimento.** Gera e salva um arquivo Markdown contendo a lista completa de erros de domínio da biblioteca, incluindo seus códigos internos e os *Status Codes* HTTP mapeados. Essencial para a documentação da API. |

**Exemplo de Configuração:**

```csharp
// Program.cs
builder.Services.AddEndpointResultMappers((successes, errors) =>
{
    // Mapeamento de um erro de domínio customizado para um Status Code HTTP específico
    errors.Map<Business.OrderRejectedError>(
        HttpStatusCode.UnprocessableEntity, 
        "Pedido Rejeitado", 
        "urn:api-errors:order-rejected"
    );
});

var app = builder.Build();

// Inicializa os Singletons e garante a prontidão dos mapeamentos
app.UseEndpointResultMappers(); 

// Gera a documentação dos erros
app.OutputErrorsList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorsList.md"));
```