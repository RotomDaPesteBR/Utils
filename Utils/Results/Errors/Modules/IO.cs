namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de entrada/saída (I/O).
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem durante operações com arquivos,
        /// diretórios ou streams, como arquivos não encontrados, permissões negadas ou problemas de disco.
        /// O prefixo de código para erros deste módulo é 11.
        /// </remarks>
        public partial class IO : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.IO;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo IO (prefixo 11).
            /// Estes valores são usados para compor o código de erro completo (ex: 11001, 11002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. O arquivo especificado não existe no caminho fornecido.
                /// </summary>
                FileNotFound = 1,

                /// <summary>
                /// Código '2'. O usuário ou processo não tem as permissões necessárias para ler/escrever/acessar o recurso.
                /// </summary>
                PermissionDenied = 2,

                /// <summary>
                /// Código '3'. O arquivo foi lido, mas seu conteúdo está em um formato inválido ou ilegível.
                /// </summary>
                CorruptedFile = 3,

                /// <summary>
                /// Código '4'. A operação de escrita falhou porque não há espaço em disco disponível.
                /// </summary>
                DiskFull = 4,

                /// <summary>
                /// Código '5'. O caminho do diretório especificado não existe.
                /// </summary>
                DirectoryNotFound = 5,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de arquivo não encontrado (Sufixo: 01).
            /// </summary>
            internal class FileNotFoundError : Error
            {
                internal FileNotFoundError(string message, List<ErrorDetail>? details = null)
                    : base(IO.CodePrefix, (int)Codes.FileNotFound, message, details) { }
            }

            /// <summary>
            /// Representa um erro de permissão negada (Sufixo: 02).
            /// </summary>
            internal class PermissionDeniedError : Error
            {
                internal PermissionDeniedError(string message, List<ErrorDetail>? details = null)
                    : base(IO.CodePrefix, (int)Codes.PermissionDenied, message, details) { }
            }

            /// <summary>
            /// Representa um erro de arquivo corrompido (Sufixo: 03).
            /// </summary>
            internal class CorruptedFileError : Error
            {
                internal CorruptedFileError(string message, List<ErrorDetail>? details = null)
                    : base(IO.CodePrefix, (int)Codes.CorruptedFile, message, details) { }
            }

            /// <summary>
            /// Representa um erro de disco cheio (Sufixo: 04).
            /// </summary>
            internal class DiskFullError : Error
            {
                internal DiskFullError(string message, List<ErrorDetail>? details = null)
                    : base(IO.CodePrefix, (int)Codes.DiskFull, message, details) { }
            }

            /// <summary>
            /// Representa um erro de caminho não encontrado (Sufixo: 05).
            /// </summary>
            internal class DirectoryNotFoundError : Error
            {
                internal DirectoryNotFoundError(string message, List<ErrorDetail>? details = null)
                    : base(IO.CodePrefix, (int)Codes.DirectoryNotFound, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de arquivo não encontrado (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Arquivo não encontrado."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um arquivo não encontrado.</returns>
            public static Error FileNotFound(
                string message = "Arquivo não encontrado.",
                List<ErrorDetail>? details = null
            ) => new FileNotFoundError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de permissão negada (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Permissão negada para acessar o arquivo ou diretório."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma permissão negada.</returns>
            public static Error PermissionDenied(
                string message = "Permissão negada para acessar o arquivo ou diretório.",
                List<ErrorDetail>? details = null
            ) => new PermissionDeniedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de arquivo corrompido (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Arquivo corrompido ou ilegível."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um arquivo corrompido.</returns>
            public static Error CorruptedFile(
                string message = "Arquivo corrompido ou ilegível.",
                List<ErrorDetail>? details = null
            ) => new CorruptedFileError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de disco cheio (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Não há espaço em disco suficiente para completar a operação."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um disco cheio.</returns>
            public static Error DiskFull(
                string message = "Não há espaço em disco suficiente para completar a operação.",
                List<ErrorDetail>? details = null
            ) => new DiskFullError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de caminho não encontrado (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O caminho do diretório não foi encontrado."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um caminho inválido.</returns>
            public static Error DirectoryNotFound(
                string message = "O caminho do diretório não foi encontrado.",
                List<ErrorDetail>? details = null
            ) => new DirectoryNotFoundError(message, details);
        }
    }
}