namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Structure
    {
        [JsonProperty("nature")]
        public string Nature { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("capital_currency")]
        public string CapitalCurrency { get; set; }
    }
}