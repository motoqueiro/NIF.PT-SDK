namespace NIF.PT.Client.Responses
{
    using Newtonsoft.Json;
    using NIF.PT.Client.Entities;

    public class CreditPurchaseResponse
    {
        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("mb")]
        public ATMReference AtmReference { get; set; }
    }
}