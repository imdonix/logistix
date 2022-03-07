
using Newtonsoft.Json;

namespace Networking.Models
{
    public class LevelModel
    {
        [JsonProperty("id")] public int ID;
        [JsonProperty("unlocks")] public int[] Unlocks;
        [JsonProperty("name")] public string Name;
        [JsonProperty("boxes")] public int[] BoxIDs;
        [JsonProperty("maxlost")] public int MaxLost;
        [JsonProperty("premium")] public bool IsPremiumOnly;

        [JsonProperty("reward_wood")] public int WoodReward;
        [JsonProperty("reward_iron")] public int IronReward;

        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }
    }
}
