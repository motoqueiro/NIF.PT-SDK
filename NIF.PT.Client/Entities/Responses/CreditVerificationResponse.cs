namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public class CreditVerificationResponse
    {
        [JsonProperty("credits")]
        public Credits Credits { get; set; }
    }
}