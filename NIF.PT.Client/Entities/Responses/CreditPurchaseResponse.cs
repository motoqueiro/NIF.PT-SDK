namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class CreditPurchaseResponse
    {
        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("mb")]
        public AtmReference AtmReference { get; set; }
    }
}