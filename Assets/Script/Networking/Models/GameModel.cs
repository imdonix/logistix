
using Newtonsoft.Json;

namespace Networking.Models
{
    public class GameModel
    {
        [JsonProperty("id")] public string ID;
        [JsonProperty("unlocks")] public int[] Unlocks;

        [JsonProperty("name")] public string Name;
        [JsonProperty("des")] public string Description;

        [JsonProperty("boxes")] public int[] BoxIDs;
        [JsonProperty("maxlost")] public int MaxLost;
    }
}