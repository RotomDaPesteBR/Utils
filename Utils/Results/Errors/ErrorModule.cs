namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa a classe base para todos os módulos de erro na aplicação.
        /// </summary>
        /// <remarks>
        /// Cada categoria de erro (ex: Aplicação, Banco de Dados, Negócio) deve herdar desta classe.
        /// Ela define a estrutura básica para a composição do código de erro por meio de um prefixo.
        /// </remarks>
        public abstract class ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// Por exemplo, a categoria 'Aplicação' pode ter o prefixo 1.
            /// </remarks>
            public const int CodePrefix = 0;
        }

        /// <summary>
        /// Representa uma fábrica para a criação de erros de um módulo específico.
        /// </summary>
        /// <typeparam name="TModule">O tipo do módulo de erro, que deve herdar de <see cref="ErrorModule"/>.</typeparam>
        /// <remarks>
        /// Esta classe é utilizada para permitir a criação de métodos de extensão que constroem
        /// erros específicos de um módulo de forma fluida (ex: <c>Error.Custom.OrderRejected(...)</c>).
        /// </remarks>
        public class ErrorModule<TModule>
            where TModule : ErrorModule { }

        /// <summary>
        /// Fornece um ponto de entrada para a criação de erros de módulos customizados.
        /// </summary>
        /// <typeparam name="TModule">O tipo do módulo de erro, que deve herdar de <see cref="ErrorModule"/>.</typeparam>
        /// <returns>Uma nova instância de <see cref="ErrorModule{TModule}"/>, que serve como uma fábrica de erros para o módulo especificado.</returns>
        /// <remarks>
        /// Use este método em conjunto com métodos de extensão para criar erros específicos de um módulo,
        /// por exemplo: <c>Error.Custom&lt;BusinessErrors&gt;().OrderRejected(...)</c>.
        /// </remarks>
        public static ErrorModule<TModule> Custom<TModule>()
            where TModule : ErrorModule => new();
    }
}
