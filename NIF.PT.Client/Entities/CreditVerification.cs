namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class CreditVerification
    {
        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("hour")]
        public int Hour { get; set; }

        [JsonProperty("minute")]
        public int Minute { get; set; }

        [JsonProperty("paid")]
        public int Paid { get; set; }
    }
}