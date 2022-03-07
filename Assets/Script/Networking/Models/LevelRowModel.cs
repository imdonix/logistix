using Newtonsoft.Json;

namespace Networking.Models
{
    public class LevelRowModel
    {
        [JsonProperty("levels")] public LevelModel[] Levels;
        [JsonProperty("color")] public string Color;
    }
}