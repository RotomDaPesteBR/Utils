# Documentação do Módulo Utils.Results.AspNet

O módulo **`Utils.AspNet`** (namespace `Utils.Results.AspNet`) é uma extensão essencial da biblioteca principal `Utils.Results`. Ele funciona como um **adaptador de *framework***, fornecendo uma ponte robusta e automatizada entre o domínio da aplicação (que retorna o tipo `Result<T>`) e a camada de apresentação Web (que requer respostas HTTP padronizadas como `IResult` ou `IActionResult`).

O objetivo deste módulo é **eliminar o código *boilerplate*** de conversão de `Result<T>` para respostas HTTP, garantindo que:

1.  O mapeamento de falhas siga o padrão **Problem Details (RFC 7807)**.
2.  O mapeamento de sucessos siga as convenções REST.

-----

## 1\. Tipos Adaptadores de Endpoint

As classes **`EndpointResult`** e **`EndpointResult<TValue>`** são o núcleo da integração com o ASP.NET Core. Elas implementam a interface `Microsoft.AspNetCore.Http.IResult` e utilizam operadores de conversão implícita para simplificar drasticamente o código do *endpoint*.

### `EndpointResult<TValue>` e `EndpointResult`

| Tipo | Mapeia o Resultado | Uso Principal |
| :--- | :--- | :--- |
| **`EndpointResult<TValue>`** | `Result<TValue>` | Endpoints que retornam um valor no sucesso (e.g., `GET`, `POST` com resposta). |
| **`EndpointResult`** | `Result` (ou `Result<Success>`) | Endpoints que retornam apenas um status (e.g., `DELETE`, `PUT` sem corpo de retorno). |

#### Conversão Implícita

Graças aos operadores de conversão implícita, os *endpoints* podem retornar diretamente os tipos de domínio, delegando a conversão HTTP ao adaptador:

```csharp
// Exemplo de Endpoint (Minimal API ou Controller)
// O tipo de retorno é o EndpointResult<T>
public async Task<EndpointResult<ProductDto>> HandleAsync(int productId, IProductService service)
{
    // O retorno implícito acontece aqui: Result<ProductDto> -> EndpointResult<ProductDto>
    return await service.RetrieveProduct(productId);
}
```

### Detalhes da Execução

O adaptador executa a lógica de mapeamento em tempo de execução:

1.  **Em caso de Sucesso (`IsSuccess = true`):** O resultado é convertido para um `SuccessResult<TValue>`, que usa o `SuccessDetails` para determinar o **Status Code** (e.g., `Success.Ok()` $\\to$ **200**).
2.  **Em caso de Falha (`IsFailure = true`):** O resultado é convertido para um `ErrorResult`, que utiliza o **`ErrorMappingService`** para determinar o **Status Code** e formatar a resposta como **Problem Details**.

-----

## 2\. Controle de Resposta: Métodos de Extensão

O namespace `Utils.Results.AspNet.Extensions` fornece métodos de extensão que permitem refinar o comportamento da resposta HTTP ao converter um `Result<T>` para `EndpointResult<T>`, principalmente controlando o `Content-Type`.

### `WithContentType` (Configuração de Content-Type)

Este método permite que o desenvolvedor especifique um `Content-Type` diferente do padrão JSON para o valor de sucesso (`TValue`). Isso é essencial para *endpoints* que retornam dados brutos ou formatos específicos.

| Método | Finalidade | Descrição |
| :--- | :--- | :--- |
| **`WithContentType<TValue>`** | **Override de Serialização** | Permite encadear a conversão de `Result<TValue>` para `EndpointResult<TValue>` e forçar o `Content-Type` da resposta HTTP (e.g., `"text/plain"`, `"application/xml"`). |
| **`ToEndpointResult<TValue>`**| **Conversão Explícita** | É um método explícito para conversão que não requer a conversão implícita do C\#. Funciona como *default* para `application/json`. |

**Exemplo de Uso em Controller:**

```csharp
[HttpGet("Example")]
public EndpointResult<string> Example()
{
    // Retorna o resultado "Example" com Content-Type: text/plain
    return Result.Success("Example").WithContentType("text/plain"); 
}
```

-----

## 3\. Mapeamento de Status HTTP

O módulo implementa serviços de mapeamento baseados em Injeção de Dependência (DI) para garantir que a tradução entre a lógica de domínio (`Error`/`Success`) e o *Status Code* HTTP seja centralizada e configurável.

### 3.1. Mapeamento de Falhas (`ErrorMappingService` e `ErrorResult`)

O **`ErrorMappingService`** é um serviço *singleton* que gerencia o mapeamento de tipos de erro (`System.Type`) para detalhes de resposta HTTP.

| Recurso | Descrição |
| :--- | :--- |
| **Padrão RFC 7807**| A classe `ErrorResult` serializa todas as instâncias de `Error` como objetos **Problem Details** (`application/problem+json`). |
| **Mapeamento de Status**| O serviço procura o mapeamento para o tipo de `Error` retornado, definindo o `Status Code` (e.g., `NotFoundError` $\\to$ **404 Not Found**). |
| **Extensão de Detalhes**| Os detalhes de erro de validação (`ErrorDetail`) são incluídos na extensão `"errors"` do objeto *Problem Details*. |
| **Fallback**| Se um erro não possuir mapeamento configurado, o *Status Code* padrão retornado é **500 Internal Server Error**. |

### 3.2. Mapeamento de Sucessos (`SuccessMappingService` e `SuccessResult<T>`)

Os `SuccessResult` e `SuccessResult<TValue>` são responsáveis por formatar respostas de sucesso. Eles utilizam o `SuccessMappingService` para consultar o mapeamento do `Success` de domínio.

| Recurso | Descrição |
| :--- | :--- |
| **Status Baseado em Sucesso**| O `Status Code` é determinado pelo tipo de `Success` retornado (e.g., `Success.Created()` $\\to$ **201 Created**). |
| **Resposta Simples**| Para resultados que não retornam valor e são mapeados para **204 No Content**, nenhum corpo de resposta é gerado. |
| **Serialização de Valor**| Em sucessos com valor (e.g., 200 OK, 201 Created), o `TValue` é serializado no corpo da resposta (JSON por padrão). |

-----

## 4\. Configuração e Inicialização

O módulo `Utils.AspNet` fornece métodos de extensão para o `IServiceCollection` e `WebApplication` para configurar o mapeamento no *pipeline* de inicialização.

### 4.1. Registro de Serviços

O registro é feito via `IServiceCollection`, permitindo a customização do mapeamento de erros e sucessos.

```csharp
public static IServiceCollection AddEndpointResultMappers(
    this IServiceCollection services,
    Action<SuccessMappingConfigurator, ErrorMappingConfigurator>? configure = null
)
```

**Exemplo de Configuração Customizada:**

O método `Map` permite vincular erros de domínio customizados a um contrato HTTP específico:

```csharp
builder.Services.AddEndpointResultMappers((successes, errors) =>
{
    // Mapeia o erro customizado OrderRejectedError para 422 Unprocessable Entity
    errors.Map<Business.OrderRejectedError>(
        HttpStatusCode.UnprocessableEntity, 
        "Pedido Rejeitado", 
        "urn:api-errors:order-rejected"
    );
});
```

### 4.2. Inicialização do Mapeamento

A inicialização deve ser forçada após a construção do `WebApplication` para garantir que os serviços *singleton* sejam instanciados e os mapeamentos carregados antes de qualquer requisição.

```csharp
public static void UseEndpointResultMappers(this WebApplication app)
```

### 4.3. Documentação de Erros (`OutputErrorsList`)

O método **`OutputErrorsList`** é uma ferramenta de desenvolvimento projetada para documentar automaticamente o vocabulário de erros da aplicação.

```csharp
public static void OutputErrorsList(this WebApplication app, string? filePath, bool withHttpMappings = true)
```

**Funcionalidade:** Ele varre todos os erros de domínio conhecidos pela biblioteca `Utils.Results` e seus módulos customizados, gerando um arquivo Markdown que lista o código interno do erro, o nome da classe, e o *Status Code* HTTP correspondente, conforme configurado no `ErrorMappingService`. É essencial para manter a documentação da API em sincronia com o código de domínio.