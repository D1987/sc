using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Server.ModelGenerator {
    public static class Utils {
        private static readonly string IgnoreAttribute = typeof(JsonIgnoreAttribute).FullName;

        public static NameAndType[] GetAllPropertiesInType(this Type t) {
            return (
                from p in (
                    from p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(x => !x.CustomAttributes.Any() || x.CustomAttributes.All(a => !IgnoreAttribute.Equals(a.AttributeType.FullName)))
                    select new NameAndType() {
                        Name = p.Name,
                        Type = p.PropertyType.ToTypeScriptType()
                    }).Distinct()
                orderby p.Name
                select p).ToArray();
        }

        public static List<NameAndType> GetDeclaredPropertiesInType(this Type t, ConvertType convertType) {
            return (
                from p in (
                    from p in t.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                        .Where(x => !x.CustomAttributes.Any() || x.CustomAttributes.All(a => !IgnoreAttribute.Equals(a.AttributeType.FullName)))
                    select new NameAndType() {
                        Name = p.Name,
                        Type = p.PropertyType.ToCustomType(convertType),
                        Prefix = p.PropertyType.ToCustomPrefix(convertType),
                        Submodel = p.PropertyType.ToSubmodel(convertType)
                    }).Distinct()
                orderby p.Name
                select p).ToList();
        }

        public static Type GetPropertyType(this PropertyInfo pi) {
            if (!pi.PropertyType.IsGenericType) {
                return pi.PropertyType;
            }
            return pi.PropertyType.GetGenericArguments()[0];
        }

        public static bool IsModelType(this Type t) {
            if (!t.IsClass || t.Namespace == null || t == typeof(string)) {
                return false;
            }
            return (t.FullName == null ? false : !t.FullName.StartsWith("System."));
        }

        public static bool IsModelTypeNoAbstract(this Type t) {
            if (t.IsAbstract) {
                return false;
            }
            return t.IsModelType();
        }

        public static string ToCustomType(this Type t, ConvertType convertType) {
            switch (convertType) {
                case ConvertType.Ts:
                    return ToTypeScriptType(t);
                default:
                    throw new ArgumentOutOfRangeException(nameof(convertType), convertType, null);
            }
        }

        public static string ToCustomPrefix(this Type t, ConvertType convertType) {
            switch (convertType) {
                case ConvertType.Ts:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(convertType), convertType, null);
            }
        }

        public static string ToSubmodel(this Type t, ConvertType convertType) {
            switch (convertType) {
                case ConvertType.Ts:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(convertType), convertType, null);
            }
        }

        public static string ToTypeScriptType(this Type t) {
            if (t.IsModelType()) {
                return t.Name;
            }
            if (t == typeof(bool)) {
                return "boolean";
            }
            if (t == typeof(byte) || t == typeof(sbyte) || t == typeof(ushort) || t == typeof(short) || t == typeof(uint) || t == typeof(int) || t == typeof(ulong) || t == typeof(long) || t == typeof(float) || t == typeof(double) || t == typeof(decimal)) {
                return "number";
            }
            if (t == typeof(string) || t == typeof(char)) {
                return "string";
            }
            if (t.Name == "List`1" || t.IsGenericType && typeof(IEnumerable<object>).IsAssignableFrom(t)) {
                return string.Concat(t.GetGenericArguments()[0].ToTypeScriptType(), "[]");
            }
            if (t.Name == "Nullable`1") {
                return t.GetGenericArguments()[0].ToTypeScriptType();
            }
            if (t == typeof(DateTime)) {
                return "moment.Moment";
            }
            if (t.IsGenericParameter) {
                return "T";
            }
            return "any";
        }

        public static bool WriteIfChanged(string text, string path) {
            if (File.Exists(path) && string.Equals(text, File.ReadAllText(path))) {
                return false;
            }
            File.WriteAllText(path, text);
            return true;
        }

    }
}