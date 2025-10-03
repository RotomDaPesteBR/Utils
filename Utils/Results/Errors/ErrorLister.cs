using System.Reflection;
using System.Text;
using LightningArc.Utils.Results.Errors;
using Runtime = System.Runtime;

namespace LightningArc.Utils.Results
{
    partial class Error
    {
        /// <summary>
        /// Gera uma lista de todos os erros da aplicação, agrupados por módulo.
        /// </summary>
        /// <remarks>
        /// O método escaneia todos os assemblies carregados em busca de classes
        /// que herdam de <see cref="ErrorModule"/> e seus respectivos métodos de criação
        /// para listar todos os erros disponíveis.
        /// </remarks>
        /// <returns>
        /// Um dicionário onde cada chave representa o nome de um módulo de erro.
        /// O valor é um dicionário interno, onde a chave é o tipo do erro
        /// e o valor é uma instancia de <see cref="ErrorInformation"/> com o código e nome do erro.
        /// </returns>
        public static Dictionary<string, Dictionary<Type, ErrorInformation>> GetErrorList()
        {
            var modulesDict = new Dictionary<string, Dictionary<Type, ErrorInformation>>();

            // Unifica os módulos embutidos e os customizados
            var allModules = GetBuiltInErrorModules().Union(GetCustomErrorModules());

            // Ordena os módulos unificados pelo CodePrefix
            var sortedModules = allModules
                .Select(module =>
                {
                    var codePrefixProperty = module.GetField(
                        "CodePrefix",
                        BindingFlags.Static | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public
                    );
                    var codePrefixValue = (int)(codePrefixProperty?.GetValue(null) ?? -1);
                    return new { Module = module, CodePrefix = codePrefixValue };
                })
                .OrderBy(x => x.CodePrefix);

            foreach (var sortedModule in sortedModules)
            {
                var module = sortedModule.Module;
                var errorsDict = new Dictionary<Type, ErrorInformation>();
                modulesDict[module.Name.ToUpperInvariant()] = errorsDict;

                var errorFactories = GetErrorsForModule(module);

                foreach (var factoryMethod in errorFactories)
                {
                    var parameters = factoryMethod.GetParameters();
                    var args = new object?[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].ParameterType == typeof(string))
                        {
                            args[i] = string.Empty;
                        }
                        else
                        {
                            args[i] = null;
                        }
                    }


                    if (factoryMethod.Invoke(null, args) is Error errorInstance)
                    {
                        errorsDict[errorInstance.GetType()] = new ErrorInformation()
                        {
                            Code = errorInstance.Code,
                            Name = factoryMethod.Name,
                        };
                    }
                }
            }

            return modulesDict;
        }

        /// <summary>
        /// Gera uma string formatada contendo uma lista de todos os erros da aplicação,
        /// agrupados e ordenados por seu prefixo de código. O método escaneia todos
        /// os assemblies carregados em busca de classes que herdam de <see cref="ErrorModule"/>.
        /// </summary>
        /// <returns>Uma string com o relatório completo de erros.</returns>
        public static string GetErrorListAsMarkdown()
        {
            var report = new StringBuilder();

            // Unifica os módulos embutidos e os customizados
            var allModules = GetBuiltInErrorModules().Union(GetCustomErrorModules());

            // Ordena os módulos unificados pelo CodePrefix
            var sortedModules = allModules
                .Select(module =>
                {
                    var codePrefixProperty = module.GetProperty(
                        "CodePrefix",
                        BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public
                    );
                    var codePrefixValue = (int)(codePrefixProperty?.GetValue(null) ?? -1);
                    return new { Module = module, CodePrefix = codePrefixValue };
                })
                .OrderBy(x => x.CodePrefix);

            foreach (var sortedModule in sortedModules)
            {
                var module = sortedModule.Module;
                report.AppendLine($"## {module.Name.ToUpperInvariant()}");
                report.AppendLine();
                //report.AppendLine(new string('-', 30)); // module.Name.Length * 2

                // A lógica para encontrar os métodos de fábrica agora é unificada,
                // já que todos os métodos serão do tipo 'MethodInfo'.
                var errorFactories = GetErrorsForModule(module);

                foreach (var factoryMethod in errorFactories)
                {
                    var parameters = factoryMethod.GetParameters();
                    var args = new object?[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].ParameterType == typeof(string))
                        {
                            args[i] = string.Empty;
                        }
                        else
                        {
                            args[i] = null;
                        }
                    }


                    if (factoryMethod.Invoke(null, args) is Error errorInstance)
                    {
                        report.AppendLine($"- `{errorInstance.Code}` - `{factoryMethod.Name}`");
                        // report.AppendLine($"  {errorInstance.Code} - {factoryMethod.Name}");
                    }
                    else
                    {
                        report.AppendLine(
                            $"- **ERRO DE REFLEXÃO**: O método `{factoryMethod.Name}` não retornou uma instância de Error."
                        );
                        // report.AppendLine($"  ERRO DE REFLEXÃO: O método '{factoryMethod.Name}' não retornou uma instância de Error.");
                    }
                }

                report.AppendLine();
            }

            return report.ToString();
        }

        private static IEnumerable<Type> GetBuiltInErrorModules()
        {
            return typeof(Error)
                .GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
                .Where(t => t.IsSubclassOf(typeof(ErrorModule)));
        }

        private static IEnumerable<Type> GetCustomErrorModules()
        {
            return AppDomain
                .CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    t.IsPublic
                    && !t.IsAbstract
                    && t.IsSubclassOf(typeof(ErrorModule))
                    && !t.IsNested
                ); // Filtra para remover os módulos embutidos já tratados.
        }

        private static IEnumerable<MethodInfo> GetErrorsForModule(Type moduleType)
        {
            // Tenta encontrar métodos de fábrica na própria classe do módulo (padrão)
            var builtInFactories = moduleType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.ReturnType == typeof(Error));

            // Lógica para encontrar métodos de extensão mais robusta
            var extensionFactories = AppDomain
                .CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSealed && t.IsAbstract && t.IsPublic) // Busca por classes estáticas
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(m => m.IsDefined(typeof(Runtime.CompilerServices.ExtensionAttribute), false))
                .Where(m =>
                {
                    var parameters = m.GetParameters();
                    if (parameters.Length == 0)
                        return false;

                    var firstParamType = parameters[0].ParameterType;

                    // Checa se o método de extensão se aplica a este módulo.
                    // Para módulos customizados como BusinessErrors, o tipo do primeiro parâmetro
                    // será o módulo de extensão genérico com BusinessErrors como o parâmetro de tipo.
                    if (
                        firstParamType.IsGenericType
                        && firstParamType.GetGenericTypeDefinition() == typeof(Error.ErrorModule<>)
                    )
                    {
                        // Verifica se o argumento genérico é o tipo do nosso módulo atual.
                        return firstParamType.GetGenericArguments().Contains(moduleType);
                    }

                    // Para módulos embutidos, o tipo do parâmetro de extensão é o próprio módulo (no caso de extensões futuras)
                    if (firstParamType == moduleType)
                    {
                        return true;
                    }

                    return false;
                })
                .Where(m => m.ReturnType == typeof(Error));

            return builtInFactories.Union(extensionFactories);
        }
    }
}
