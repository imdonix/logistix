using Newtonsoft.Json;

namespace Networking.Models
{
    public class InviteModel
    {
        [JsonProperty("count")] public int count;
        [JsonProperty("reward")] public int unlocks;
    }
}