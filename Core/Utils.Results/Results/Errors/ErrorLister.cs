using System.Reflection;
using System.Text;
using LightningArc.Utils.Results.Errors;
using Runtime = System.Runtime;

namespace LightningArc.Utils.Results
{
    partial class Error
    {
        /// <summary>
        /// Generates a list of all application errors, grouped by module.
        /// </summary>
        /// <remarks>
        /// The method scans all loaded assemblies for classes that inherit from <see cref="ErrorModule"/>
        /// and their respective factory methods to list all available errors.
        /// </remarks>
        /// <returns>
        /// A dictionary where each key represents the name of an error module.
        /// The value is an inner dictionary where the key is the error type
        /// and the value is an <see cref="ErrorInformation"/> instance with the error code and name.
        /// </returns>
        public static Dictionary<string, Dictionary<Type, ErrorInformation>> GetErrorList()
        {
            var modulesDict = new Dictionary<string, Dictionary<Type, ErrorInformation>>();
            var allModules = GetBuiltInErrorModules().Union(GetCustomErrorModules());

            var sortedModules = allModules
                .Select(module =>
                {
                    FieldInfo? codePrefixProperty = module.GetField("CodePrefix", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                    int codePrefixValue = (int)(codePrefixProperty?.GetValue(null) ?? -1);
                    return new { Module = module, CodePrefix = codePrefixValue };
                })
                .OrderBy(x => x.CodePrefix);

            foreach (var sortedModule in sortedModules)
            {
                Type module = sortedModule.Module;
                var errorsDict = new Dictionary<Type, ErrorInformation>();
                modulesDict[module.Name.ToUpperInvariant()] = errorsDict;

                var errorFactories = GetErrorsForModule(module);

                foreach (MethodInfo factoryMethod in errorFactories)
                {
                    object?[] args = CreateDefaultArgs(factoryMethod);

                    if (factoryMethod.Invoke(null, args) is Error errorInstance)
                    {
                        errorsDict[errorInstance.GetType()] = new ErrorInformation()
                        {
                            Code = errorInstance.Code,
                            Name = factoryMethod.Name,
                            Message = errorInstance.Message,
                        };
                    }
                }
            }

            return modulesDict;
        }

        /// <summary>
        /// Generates a formatted string containing a list of all application errors,
        /// grouped and sorted by their code prefix. The method scans all loaded assemblies
        /// for classes that inherit from <see cref="ErrorModule"/>.
        /// </summary>
        /// <returns>A string with the complete error report.</returns>
        public static string GetErrorListAsMarkdown()
        {
            StringBuilder report = new();
            var allModules = GetBuiltInErrorModules().Union(GetCustomErrorModules());

            var sortedModules = allModules
                .Select(module =>
                {
                    PropertyInfo? codePrefixProperty = module.GetProperty("CodePrefix", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                    int codePrefixValue = (int)(codePrefixProperty?.GetValue(null) ?? -1);
                    return new { Module = module, CodePrefix = codePrefixValue };
                })
                .OrderBy(x => x.CodePrefix);

            foreach (var sortedModule in sortedModules)
            {
                Type module = sortedModule.Module;
                report.AppendLine($"## {module.Name.ToUpperInvariant()}");
                report.AppendLine();

                var errorFactories = GetErrorsForModule(module);

                foreach (MethodInfo factoryMethod in errorFactories)
                {
                    object?[] args = CreateDefaultArgs(factoryMethod);

                    if (factoryMethod.Invoke(null, args) is Error errorInstance)
                    {
                        report.AppendLine($"- `{errorInstance.Code}` - `{factoryMethod.Name}`: {errorInstance.Message}");
                    }
                    else
                    {
                        report.AppendLine($"- **REFLECTION ERROR**: Method `{factoryMethod.Name}` did not return an Error instance.");
                    }
                }
                report.AppendLine();
            }

            return report.ToString();
        }

        private static object?[] CreateDefaultArgs(MethodInfo factoryMethod)
        {
            var parameters = factoryMethod.GetParameters();
            object?[] args = new object?[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo p = parameters[i];
                if (p.HasDefaultValue)
                {
                    args[i] = p.DefaultValue;
                }
                else if (p.ParameterType == typeof(string))
                {
                    args[i] = string.Empty;
                }
                else if (p.ParameterType.IsValueType)
                {
                    args[i] = Activator.CreateInstance(p.ParameterType);
                }
                else
                {
                    args[i] = null;
                }
            }
            return args;
        }

        private static IEnumerable<Type> GetBuiltInErrorModules() => typeof(Error)
                .GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
                .Where(t => t.IsSubclassOf(typeof(ErrorModule)));

        private static IEnumerable<Type> GetCustomErrorModules() => AppDomain
                .CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    t.IsPublic
                    && !t.IsAbstract
                    && t.IsSubclassOf(typeof(ErrorModule))
                    && !t.IsNested
                );

        private static IEnumerable<MethodInfo> GetErrorsForModule(Type moduleType)
        {
            var builtInFactories = moduleType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.ReturnType == typeof(Error));

            var extensionFactories = AppDomain
                .CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSealed && t.IsAbstract && t.IsPublic)
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(m => m.IsDefined(typeof(Runtime.CompilerServices.ExtensionAttribute), false))
                .Where(m =>
                {
                    var parameters = m.GetParameters();
                    if (parameters.Length == 0) return false;

                    Type firstParamType = parameters[0].ParameterType;

                    if (firstParamType.IsGenericType && firstParamType.GetGenericTypeDefinition() == typeof(ErrorModule<>))
                    {
                        return firstParamType.GetGenericArguments().Contains(moduleType);
                    }

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