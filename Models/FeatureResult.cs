using System.Text.Json.Serialization;

namespace moneyLionAssignment.Models
{
    public class FeatureResult
    {
        [JsonPropertyName("canAccess")]
        public bool CanAccess { get; set; }
    }
}