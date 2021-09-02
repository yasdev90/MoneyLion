using Newtonsoft.Json;
namespace moneyLionAssignment.Models
{
    public class Feature
    {
        [JsonProperty("featureName")]
        public string FeatureName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("enable")]
        public bool IsEnabled { get; set; }
    }
}