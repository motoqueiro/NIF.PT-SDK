namespace NIF.PT.Client.Responses
{
    using Newtonsoft.Json;
    using NIF.PT.Client.Entities;

    public class CreditVerificationResponse
    {
        [JsonProperty("credits")]
        public CreditVerification Credits { get; set; }
    }
}