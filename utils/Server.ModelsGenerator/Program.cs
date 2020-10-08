using System;
using System.IO;

namespace Server.ModelGenerator {
    class Program {
        static void Main(string[] args) {
            var generator = new Generator();
            var file = Directory.GetCurrentDirectory() + "/config.json";
            try {
                var options = File.Exists(file) ? FileHandler.ReadJson<GeneratorOptions>(file) : null;
                generator.Process(options);
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
