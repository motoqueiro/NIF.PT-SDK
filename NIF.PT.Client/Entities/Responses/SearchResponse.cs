﻿namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;
    using NIF.PT.Client.Converters;

    public class SearchResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("records"), JsonConverter(typeof(RecordJsonConverter))]
        public Record Record { get; set; }

        [JsonProperty("nif_validation")]
        public bool NifValidation { get; set; }

        [JsonProperty("is_nif")]
        public bool IsNif { get; set; }

        [JsonProperty("credits")]
        public Credits Credits { get; set; }
    }
}