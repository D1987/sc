using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Server.ModelGenerator {
    public class Generator {
        private GeneratorOptions _options;
        private string _basePath;

        public void Process(GeneratorOptions options) {
            _options = options ?? new GeneratorOptions();

            Stopwatch stopwatch = Stopwatch.StartNew();

            var convertTypes = new List<ConvertType> { ConvertType.Ts };

            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
            _basePath = AbsolutePath(_options.Source);
            Console.Write("Scanning for DTO objects in {0}...  ", _basePath);
            var strs = _options.Files.SelectMany(f => Directory.GetFiles(_basePath, f));
            var assemblies = strs.Select(Load).Where(a => a != null);
            var list = assemblies.SelectMany(GetAssemblyTypes).ToList();
            var types = new HashSet<Type>(list);
            var array = types.ToArray();
            foreach (var t in array) {
                RecursivelySearchModels(t, types);
            }

            Console.ForegroundColor = types.Count > 0 ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.WriteLine("Found {0}", types.Count);
            Console.ResetColor();
            foreach (var convertType in convertTypes) {
                var targetPath = GetDestinationPath(convertType);
                if (Directory.Exists(targetPath)) {
                    Directory.Delete(targetPath, true);
                }

                Directory.CreateDirectory(targetPath);
                EntityGenerator.Generate(targetPath, types, convertType);

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done in {0:N3}s", stopwatch.Elapsed.TotalSeconds);
            Console.ResetColor();
        }

        private string AbsolutePath(string relativePath) {
            return Path.IsPathRooted(relativePath)
                ? relativePath
                : Path.Combine(Environment.CurrentDirectory, relativePath);
        }

        private IEnumerable<Type> GetAssemblyTypes(Assembly a) {
            var filesName = new List<string>();
            foreach (var file in Directory.EnumerateFiles(AbsolutePath(_options.Models), "*", SearchOption.AllDirectories)) {
                filesName.Add(Path.GetFileNameWithoutExtension(file));
            }
            return a.GetTypes().Where(t => filesName.Any(x=> x.Contains(t.Name) || t.Name.Contains(x))).ToList();
        }

        private IEnumerable<Type> GetModelTypes(Type t) {
            if (t.IsModelType()) {
                if (!t.IsArray) {
                    yield return t;
                } else {
                    yield return t.GetElementType();
                }
            } else if (t.IsGenericType) {
                var genericArguments = t.GetGenericArguments();
                foreach (var type in (
                    from a in genericArguments
                    where a.IsModelType()
                    select a).SelectMany(GetModelTypes)) {
                    yield return type;
                }
            }
            if (t.BaseType != null && t.BaseType.IsModelType()) {
                yield return t.BaseType;
            }
        }

        private Assembly Load(string path) {
            Assembly assembly;
            try {
                assembly = Assembly.LoadFile(path);
            } catch {
                assembly = null;
            }
            return assembly;
        }

        private void RecursivelySearchModels(Type model, ISet<Type> visitedModels) {
            var types = (
                    from p in model.GetProperties()
                    select p.GetPropertyType()).SelectMany(GetModelTypes)
                .Where(t => !visitedModels.Contains(t) && t.IsModelType());
            foreach (var type in types) {
                visitedModels.Add(type);
                RecursivelySearchModels(type, visitedModels);
            }
        }

        private Assembly ResolveAssembly(object sender, ResolveEventArgs args) {
            Assembly assembly;
            try {
                var str = Path.Combine(_basePath,
                    string.Concat(args.Name.Substring(0, args.Name.IndexOf(",", StringComparison.Ordinal)), ".dll"));
                assembly = Assembly.LoadFile(str);
            } catch {
                Console.WriteLine(args.Name);
                assembly = null;
            }
            return assembly;
        }


        private string GetDestinationPath(ConvertType convertType) {
            switch (convertType) {
                case ConvertType.Ts:
                    return Path.GetFullPath(Path.Combine(AbsolutePath(_options.TsDestination), "generated"));
                default:
                    throw new ArgumentOutOfRangeException(nameof(convertType), convertType, null);
            }
        }

    }
}
