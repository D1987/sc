using System.Runtime.Serialization;

namespace Server.ModelGenerator {
    [DataContract]
    public class GeneratorOptions {
        [DataMember]
        public string Namespace { get; set; }

        [DataMember]
        public string TsDestination { get; set; }

        [DataMember]
        public string[] Files { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Models { get; set; }
    }
}
