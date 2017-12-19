namespace NIF.PT.Client.Converters
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NIF.PT.Client.Entities;

    public class CreditVerificationJsonConverter
        : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CreditVerification);
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var creditsToken = JToken.Load(reader);
            var creditToken = creditsToken.SelectToken("credits");
            if (creditToken == null)
            {
                return null;
            }

            return creditToken.ToObject<CreditVerification>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}