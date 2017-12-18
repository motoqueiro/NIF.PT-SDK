namespace NIF.PT.Client.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using NIF.PT.Client.Entities;

    public partial class SearchResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("records")]
        public IEnumerable<Record> Records { get; set; }

        [JsonProperty("nif_validation")]
        public bool NifValidation { get; set; }

        [JsonProperty("is_nif")]
        public bool IsNif { get; set; }

        [JsonProperty("credits")]
        public Credits Credits { get; set; }
    }
}