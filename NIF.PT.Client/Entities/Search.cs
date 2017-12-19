namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;
    using NIF.PT.Client.Converters;

    public partial class Search
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("records"), JsonConverter(typeof(RecordJsonConverter))]
        public Record Record { get; set; }

        [JsonProperty("nif_validation")]
        public bool NifValidation { get; set; }

        [JsonProperty("is_nif")]
        public bool IsNif { get; set; }

        [JsonProperty("credits")]
        public Credits Credits { get; set; }
    }
}