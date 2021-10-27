using System.Text.Json.Serialization;

namespace HC.LogProxy.Dal.Dto
{
    public class Fields
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("Summary")]
        public string? Summary { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("receivedAt")]
        public string? ReceivedAt { get; set; }
    }
}