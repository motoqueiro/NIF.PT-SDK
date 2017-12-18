namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class Credits
    {
        [JsonProperty("used")]
        public string Used { get; set; }

        [JsonProperty("left")]
        public object[] Left { get; set; }
    }
}