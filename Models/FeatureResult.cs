using Newtonsoft.Json;

namespace moneyLionAssignment.Models
{
    public class FeatureResult
    {
        [JsonProperty("canAccess")]
        public bool CanAccess { get; set; }
    }
}