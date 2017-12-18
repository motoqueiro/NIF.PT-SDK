namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Welcome
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("records")]
        public Records Records { get; set; }

        [JsonProperty("nif_validation")]
        public bool NifValidation { get; set; }

        [JsonProperty("is_nif")]
        public bool IsNif { get; set; }

        [JsonProperty("credits")]
        public Credits Credits { get; set; }
    }
}