namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Record
    {
        [JsonProperty("nif")]
        public long Nif { get; set; }

        [JsonProperty("seo_url")]
        public string SeoUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("pc4")]
        public string Pc4 { get; set; }

        [JsonProperty("pc3")]
        public string Pc3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("activity")]
        public string Activity { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("cae")]
        public string Cae { get; set; }

        [JsonProperty("contacts")]
        public Contacts Contacts { get; set; }

        [JsonProperty("structure")]
        public Structure Structure { get; set; }

        [JsonProperty("geo")]
        public Geo Geo { get; set; }

        [JsonProperty("place")]
        public Place Place { get; set; }

        [JsonProperty("racius")]
        public string Racius { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("portugalio")]
        public string Portugalio { get; set; }
    }
}