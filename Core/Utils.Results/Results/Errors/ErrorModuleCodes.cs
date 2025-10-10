namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Define os prefixos de código (o primeiro dígito ou mais) para cada módulo de erro.
        /// Os valores são atribuídos sequencialmente de acordo com a prioridade e universalidade do módulo.
        /// </summary>
        public enum ModuleCodes
        {
            /// <summary>
            /// Prefix '1'. Módulo de erros gerais de fluxo da aplicação e lógica de alto nível.
            /// </summary>
            Application = 1,

            /// <summary>
            /// Prefix '2'. Módulo de erros críticos de infraestrutura, ambiente e inicialização.
            /// </summary>
            System = 2,

            /// <summary>
            /// Prefix '3'. Módulo de erros de persistência e banco de dados.
            /// </summary>
            Database = 3,

            /// <summary>
            /// Prefix '4'. Módulo de erros de validação de entrada de dados e regras de negócio. (Novo módulo)
            /// </summary>
            Validation = 4,

            /// <summary>
            /// Prefix '5'. Módulo de erros de gerenciamento de recursos (ex: não encontrado, já existe).
            /// </summary>
            Resource = 5,

            /// <summary>
            /// Prefix '6'. Módulo de erros de autenticação (identidade) e autorização (permissão).
            /// </summary>
            Authentication = 6,

            /// <summary>
            /// Prefix '7'. Módulo de erros de processamento da requisição HTTP (ex: payload muito grande).
            /// </summary>
            Request = 7,

            /// <summary>
            /// Prefix '8'. Módulo de erros ao interagir com serviços externos/terceiros.
            /// </summary>
            External = 8,

            /// <summary>
            /// Prefix '9'. Módulo de erros de comunicação de rede de baixo nível.
            /// </summary>
            Network = 9,

            /// <summary>
            /// Prefix '10'. Módulo de erros de concorrência e bloqueios de recursos.
            /// </summary>
            Concurrency = 10,

            /// <summary>
            /// Prefix '11'. Módulo de erros de operações de Input/Output (I/O) de disco.
            /// </summary>
            IO = 11,
        }
    }
}