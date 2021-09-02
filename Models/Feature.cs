using System.Text.Json.Serialization;

namespace moneyLionAssignment.Models
{
    public class Feature
    {
        [JsonPropertyName("featureName")]
        public string FeatureName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }


        [JsonPropertyName("enable")]
        public bool IsEnabled { get; set; }
    }
}