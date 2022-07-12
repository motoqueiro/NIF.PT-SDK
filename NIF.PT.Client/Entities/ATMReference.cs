namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class AtmReference
    {
        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }
}