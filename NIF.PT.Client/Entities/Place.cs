namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Place
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("pc4")]
        public string Pc4 { get; set; }

        [JsonProperty("pc3")]
        public string Pc3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}