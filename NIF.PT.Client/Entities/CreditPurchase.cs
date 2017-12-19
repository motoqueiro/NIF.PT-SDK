namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class CreditPurchase
    {
        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("mb")]
        public ATMReference AtmReference { get; set; }
    }
}