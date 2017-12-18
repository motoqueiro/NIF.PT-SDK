namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Geo
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("parish")]
        public string Parish { get; set; }
    }
}