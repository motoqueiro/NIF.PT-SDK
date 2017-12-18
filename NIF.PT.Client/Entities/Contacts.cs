namespace NIF.PT.Client.Entities
{
    using Newtonsoft.Json;

    public partial class Contacts
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }
    }
}