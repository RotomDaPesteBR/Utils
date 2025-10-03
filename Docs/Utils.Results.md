# Documentação da Biblioteca Utils

A biblioteca **`Utils`** é um conjunto de utilitários e padrões de software desenvolvidos em C\#. Seu objetivo é promover a criação de código **limpo, expressivo e robusto** em aplicações .NET. Atualmente, a principal funcionalidade da biblioteca reside no módulo de resultados funcionais, `Utils.Results`, que implementa o **Padrão Result** para o gerenciamento explícito e estruturado de sucessos e falhas operacionais.

-----

## Módulo Utils.Results: Gerenciamento Funcional de Erros

O namespace `Utils.Results` oferece uma abordagem alternativa ao uso de exceções (`try-catch`) para o controle do fluxo normal de erros de aplicação e domínio. Esta implementação utiliza a composição de funções e garante forte tipagem, assegurando que os resultados de sucesso (`Success`) e falha (`Error`) sejam tratados de maneira obrigatória e explícita.

### 1\. Tipos de Resultados (O Core da Biblioteca)

| Tipo | Uso | Descrição |
| :--- | :--- | :--- |
| **`Result<TValue>`** | Retorno de funções com valor. | Estrutura que representa o resultado de uma operação que pode conter um valor de sucesso (`TValue`) ou uma informação de falha (`Error`). |
| **`Result`** | Retorno de funções sem valor. | Um alias para `Result<Success>`, destinado a operações que apenas retornam um status (sucesso/falha), sem um dado de domínio específico. |
| **`Error`** | Informação de Falha. | Classe base tipada para estruturar o motivo da falha, incluindo códigos numéricos e detalhes adicionais (`ErrorDetail`). |
| **`Success`** | Informação de Sucesso. | Classe base para resultados de sucesso padronizados, fundamental para o mapeamento e padronização de respostas HTTP. |

-----

### 2\. Composição e Fluxo de Controle (Métodos de Extensão)

Os métodos de extensão em **`Utils.Results.ResultExtensions`** facilitam a composição de operações. Eles permitem encadear funções em um fluxo coeso que é interrompido automaticamente na ocorrência da primeira falha.

| Método | Finalidade | Descrição |
| :--- | :--- | :--- |
| **`Bind / BindAsync`** | **Encadeamento de Fluxo** | Encadeia funções que retornam um `Result`. Caso a operação anterior falhe, a próxima função é ignorada e o erro original é propagado. |
| **`Map`** | **Transformação de Sucesso** | Transforma o valor de sucesso (`TIn`) em um novo tipo (`TOut`), preservando o estado de falha caso ocorra um erro. |
| **`Tap / TapAsync`** | **Efeitos Colaterais** | Executa uma ação (e.g., log, auditoria, salvamento) *somente* se o resultado for sucesso. O método retorna o `Result` original, mantendo a capacidade de encadeamento. |
| **`OnFailure / OnFailureAsync`**| **Tratamento Local de Erros** | Executa uma ação (e.g., log, manipulação de erro) *somente* se o resultado for falha. Retorna o `Result` original. |
| **`MapError`** | **Transformação de Erros** | Permite alterar o objeto `Error` em caso de falha (e.g., converter um erro de infraestrutura em um erro de domínio), permitindo a continuidade do `Result`. |

**Exemplo de Encadeamento:**

```csharp
// Fluxo sequencial que interrompe no primeiro erro encontrado.
var resultadoFinal = ObterDadosInput()
    .Bind(ValidarDados)
    .BindAsync(dados => SalvarNoBanco(dados)) // O fluxo de falha propaga o erro de validação.
    .Tap(registro => Logger.Log("Registro criado com sucesso.")) // Executado somente em caso de sucesso.
    .OnFailure(e => Analytics.ReportError(e)); // Executado somente em caso de falha.
```

-----

### 3\. Estrutura de Erros (`Error`)

A classe **`Error`** é a base para todas as representações de falha. Seu design é focado na extensibilidade e forte tipagem.

#### Códigos de Erro

O código numérico completo do erro é estruturado como:

$$\text{Code} = (\text{CodePrefix} \times 1000) + \text{CodeSuffix}$$

  * **`CodePrefix`**: Define a **categoria** do erro (e.g., `10` para Aplicação, `20` para Domínio).
  * **`CodeSuffix`**: Define o erro **específico** dentro da categoria (e.g., `01` para Erro Interno).

#### Extensibilidade de Erros

Os construtores da classe `Error` são definidos como `protected`. Esta visibilidade permite que **assemblies externos** criem suas próprias hierarquias de erro de domínio, integrando-se ao sistema de código padronizado.

```csharp
// Exemplo de erro customizado definido por um consumidor da biblioteca:
public sealed class UserNotFound : Error
{
    // Código 2005 (Prefixo 20: Domínio, Sufixo 05: Usuário não encontrado)
    public UserNotFound(int userId)
        : base(codePrefix: 20, codeSuffix: 05, $"Usuário com ID {userId} não foi encontrado.") { }
}
```

#### Erros de Validação (`ErrorDetail`)

A estrutura **`ErrorDetail`** é utilizada para fornecer múltiplos contextos de erro dentro de uma única falha (essencial para validações). A implementação suporta conversão implícita de tuplas, otimizando a criação:

```csharp
var details = new List<ErrorDetail>
{
    ("Email", "Formato de e-mail inválido."),
    ("Password", "A senha deve ter no mínimo 8 caracteres.")
};
```

-----

### 4\. Estrutura de Sucesso (`Success`)

A classe **`Success`** padroniza as respostas positivas. É particularmente útil para operações que retornam um status, mas não um valor de domínio.

#### Fábricas Padrão

A classe expõe fábricas estáticas que criam instâncias seladas (sealed) de sucesso, garantindo consistência e facilitando o mapeamento para respostas HTTP:

| Fábrica | Código Interno | Mapeamento HTTP Implícito |
| :--- | :--- | :--- |
| `Success.Ok()` | 100 | 200 OK |
| `Success.Created()` | 101 | 201 Created |
| `Success.NoContent()` | 103 | 204 No Content |

**Extensibilidade:** O construtor de `Success` é `protected`, permitindo que usuários definam tipos de sucesso customizados quando necessário.

-----

## 5\. Extensibilidade: Criando Módulos de Erro Customizados

A arquitetura da classe `Error` suporta um **Padrão de Módulo** que permite aos consumidores definir seus próprios módulos de erro de domínio. Estes módulos funcionam como fábricas de erros tipados, integrando-se à sintaxe fluente de `Error`.

### 5.1. O Padrão de Módulo de Erro

Para estabelecer um novo módulo de erro (exemplo: `Business` com `CodePrefix = 12`):

1.  **Módulo Base:** Defina uma classe pública (`Business`) que herda de `Error.ErrorModule` e declare o `CodePrefix`.
2.  **Erro Concreto:** Crie a classe de erro aninhada (`OrderRejectedError`) que herda de `Error`, utilizando o construtor base com o prefixo do módulo.
3.  **Método de Extensão:** Crie uma classe estática de extensões (`BusinessErrorExtensions`) estendendo o tipo **`Error.ErrorModule<TModule>`** para expor a fábrica do erro.

### 5.2. Exemplo de Implementação (`Business` Module)

```csharp
// Definição do Módulo e Prefixo (Código 12xx)
public class Business : Error.ErrorModule
{
    public new const int CodePrefix = 12;

    // Classe de Erro Concreta (Prefix: 12, Suffix: 01)
    public class OrderRejectedError : Error
    {
        internal OrderRejectedError(string message, List<ErrorDetail>? details = null)
            : base(Business.CodePrefix, 01, message, details) { }
    }
}

// Método de Extensão (A Fábrica Fluente)
public static class BusinessErrorExtensions
{
    // Estende Error.ErrorModule<Business> para criar a sintaxe fluente
    public static Error OrderRejected(
        this Error.ErrorModule<Business> _,
        string message,
        List<ErrorDetail>? details = null
    )
    {
        return new OrderRejectedError(message, details);
    }
}
```

### 5.3. Uso na Aplicação

Este padrão permite que o erro customizado seja invocado de forma direta, como se fosse um método estático da própria fábrica de erros:

```csharp
using static Utils.Results.Business; // Permite o acesso direto ao método

// Uso na lógica de aplicação:
var erroCustomizado = OrderRejected(
    "Erro de validação de dados de negócio.", 
    [new("Estoque", "Produto esgotado.")]
);

// Retorno em uma função que falha:
return Result.Failure(erroCustomizado);
```

Esta técnica promove a clareza e a delegacão da responsabilidade de definição de erros para o módulo de domínio específico, mantendo a consistência da API central.