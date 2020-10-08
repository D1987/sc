using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Server.ModelGenerator {
    public static class EntityGenerator {
        private static string CreateModelTsString(Type t, ConvertType convertType) {
            string str = string.Concat((t.IsAbstract ? "export abstract class " : "export class "), t.Name).Replace("`1", "<T>").Replace("`2", "<T, U>");
            string str1 = (!(t.BaseType != null) || !t.BaseType.IsModelType() ? "" : t.BaseType.Name).Replace("`1", "").Replace("`2", "");
            string str3 = (!(t.BaseType != null) || !t.BaseType.IsModelType() ? "" : t.BaseType.Name).Replace("`1", "<number>").Replace("`2", "<number, number>");
            string str4 = (!(t.BaseType != null) || !t.BaseType.IsModelType() ? "" : t.BaseType.Name).Replace("`1", "<T>").Replace("`2", "<T, U>");
            NameAndType[] allPropertiesInType = t.GetAllPropertiesInType();
            StringBuilder stringBuilder = new StringBuilder();
            if (((IEnumerable<NameAndType>)allPropertiesInType).Any((NameAndType p) => {
                if (p.Type == "moment.Moment") {
                    return true;
                }
                return p.Type == "moment.Moment?";
            })) {
                stringBuilder.AppendLine("import * as moment from 'moment';");
                stringBuilder.AppendLine();
            }
            List<string> import = new List<string>();
            if (!string.IsNullOrWhiteSpace(str1)) {
                import.Add(str1);
            }
            import.AddRange(FindTypesToImport(t));
            for (int i = 0; i < import.Count; i++) {
                string str2 = import[i];
                stringBuilder.AppendLine(string.Format("import {{ {0} }} from './{1}';", str2, GetFileName(str2).Replace("-Model", ".model").ToLower()));
            }

            stringBuilder.AppendLine();
            stringBuilder.Append(str);
            if (!string.IsNullOrWhiteSpace(str1)) {
                stringBuilder.Append(string.Concat(" extends ", t.IsAbstract ? str4 : str3));
            }
            stringBuilder.AppendLine(" {");
            var declaredPropertiesInType = t.GetDeclaredPropertiesInType(convertType);
            foreach (NameAndType nameAndType in declaredPropertiesInType) {
                stringBuilder.AppendLine(string.Format(TabToSpace(1) + "public {0}: {1};", GetLower(nameAndType.Name), nameAndType.Type));
            }
            if (!t.IsAbstract) {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine(TabToSpace(1) + "public constructor(");
                stringBuilder.AppendLine(string.Format(TabToSpace(2) + "fields?: Partial<{0}>) {{", t.Name));
                stringBuilder.AppendLine();
                if (!string.IsNullOrWhiteSpace(str1)) {
                    stringBuilder.AppendLine(TabToSpace(2) + "super(fields);");
                }
                stringBuilder.AppendLine(TabToSpace(2) + "if (fields) {");
                NameAndType[] modelPropertiesInType = GetModelPropertiesInType(t);
                stringBuilder.AppendLine(string.Join("\n",
                    from prop in modelPropertiesInType
                    select string.Format(TabToSpace(3) + "if (fields.{0}) {{ fields.{0} = new {1}(fields.{0}); }}", GetLower(prop.Name), prop.Type)));
                stringBuilder.AppendLine(string.Join("\n",
                    from x in allPropertiesInType
                    where x.Type == "moment.Moment"
                    select x into prop
                    select string.Format(TabToSpace(3) + "if (fields.{0}) {{ fields.{0} = moment.utc(fields.{0}); }}", GetLower(prop.Name))));
                stringBuilder.AppendLine(TabToSpace(3) + "Object.assign(this, fields);");
                stringBuilder.AppendLine(TabToSpace(2) + "}");
                stringBuilder.AppendLine(TabToSpace(1) + "}");
            }
            if (t.IsAbstract) {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine(TabToSpace(1) + "public constructor(");
                stringBuilder.AppendLine(string.Format(TabToSpace(2) + "fields?: Partial<{0}>) {{", t.Name.Replace("`1", "<T>").Replace("`2", "<T, U>")));
                stringBuilder.AppendLine();
                if (!string.IsNullOrWhiteSpace(str1)) {
                    stringBuilder.AppendLine(TabToSpace(2) + "super(fields);");
                }
                stringBuilder.AppendLine(TabToSpace(2) + "if (fields) {");
                NameAndType[] modelPropertiesInType = GetModelPropertiesInType(t);
                stringBuilder.AppendLine(string.Join("",
                    from prop in modelPropertiesInType
                    select string.Format(TabToSpace(3) + "if (fields.{0}) {{ fields.{0} = new {1}(fields.{0}); }}", GetLower(prop.Name), prop.Type)));
                stringBuilder.AppendLine(TabToSpace(3) + "Object.assign(this, fields);");
                stringBuilder.AppendLine(TabToSpace(2) + "}");
                stringBuilder.AppendLine(TabToSpace(1) + "}");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private static string[] FindTypesToImport(Type parentType) {
            return (
                from p in (
                    from p in parentType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    select p.GetPropertyType() into t
                    where t.IsModelType()
                    select t into x
                    where x != parentType
                    select x).Distinct()
                orderby p.Name
                select p.Name).ToArray();
        }

        public static List<string> Generate(string targetPath, HashSet<Type> allModels, ConvertType convertType) {
            var generatedResult = new List<string>();
            foreach (var type in
                from m in allModels
                where convertType != ConvertType.Ts ? !m.IsAbstract : allModels.Any()
                orderby m.Name
                select m
            ) {
                var name = convertType == ConvertType.Ts
                    ? GetFileName(type.Name).Replace("-Model", ".model").ToLower()
                    : type.Name;
                Utils.WriteIfChanged(GetFileString(convertType, type), Path.Combine(targetPath, string.Concat(name, GetFileType(convertType))).Replace("`1", "").Replace("`2", ""));
                generatedResult.Add(name);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Created {0} {1} models.", generatedResult.Count, convertType);
            Console.ResetColor();
            return generatedResult;
        }

        private static NameAndType[] GetModelPropertiesInType(Type t) {
            return (
                from p in (
                    from x in t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    where x.PropertyType.IsModelTypeNoAbstract()
                    select x into p
                    select new NameAndType() {
                        Name = p.Name,
                        Type = p.PropertyType.ToTypeScriptType()
                    }).Distinct()
                orderby p.Name
                select p).ToArray();
        }

        private static string GetFileString(ConvertType convertType, Type type) {
            switch (convertType) {
                case ConvertType.Ts:
                    return CreateModelTsString(type, convertType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(convertType), convertType, null);
            }
        }

        private static string GetFileType(ConvertType convertType) {
            switch (convertType) {
                case ConvertType.Ts:
                    return ".ts";
                default:
                    throw new ArgumentOutOfRangeException(nameof(convertType), convertType, null);
            }
        }

        private static string GetFileName(string name) {
            var temp = "";
            var results = new List<string>();

            foreach (char c in name) {
                if (char.IsUpper(c) && temp != "") {
                    results.Add(temp);
                    temp = c.ToString();
                } else {
                    temp = temp + c;
                }
            }
            results.Add(temp);
            return string.Join("-", results);
        }

        private static string GetLower(string str) {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string TabToSpace(int count) {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++) {
                stringBuilder.Append("    ");
            }
            return stringBuilder.ToString();
        }
    }
}