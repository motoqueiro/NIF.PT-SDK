namespace NIF.PT.Client.Converters
{
    using System;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NIF.PT.Client.Entities;

    public class RecordJsonConverter
        : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Record);
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var recordsToken = JToken.Load(reader);
            if (!recordsToken.HasValues
                || !Regex.IsMatch(recordsToken.First.Path, @"^\d{9}$"))
            {
                return null;
            }

            return recordsToken.First.First.ToObject<Record>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}