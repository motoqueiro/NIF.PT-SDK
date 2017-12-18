namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class CreditVerification
    {
        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("day")]
        public long Day { get; set; }

        [JsonProperty("hour")]
        public long Hour { get; set; }

        [JsonProperty("minute")]
        public long Minute { get; set; }

        [JsonProperty("paid")]
        public long Paid { get; set; }
    }
}