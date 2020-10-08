using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Server.ModelGenerator {
    public static class FileHandler {
        public static T ReadJson<T>(string filePath)
        where T : class {
            T t;
            try {
                if (!File.Exists(filePath)) {
                    return default(T);
                } else {
                    DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
                    byte[] bytes = Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
                    using (MemoryStream memoryStream = new MemoryStream(bytes)) {
                        t = (T)dataContractJsonSerializer.ReadObject(memoryStream);
                    }
                }
            } catch (Exception exception) {
                throw new Exception("Failed to parse json", exception);
            }
            return t;
        }
    }
}
