namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Credits
    {
        [JsonProperty("used")]
        public string Used { get; set; }

        [JsonProperty("left")]
        public object[] Left { get; set; }
    }
}