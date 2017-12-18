namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class CreditPurchase
    {
        [JsonProperty("credits")]
        public long Credits { get; set; }

        [JsonProperty("mb")]
        public ATMReference Reference { get; set; }
    }
}