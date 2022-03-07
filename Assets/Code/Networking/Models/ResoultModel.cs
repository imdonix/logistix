using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Networking.Models
{
    public class ResoultModel
    {

        [JsonProperty("mapid")] public int ID;
        [JsonProperty("iswin")] public bool IsWin;
        [JsonProperty("score")] public int Score;
        [JsonProperty("lostboxes")] public int LostBoxes;
        [JsonProperty("time")] public int Time;
        [JsonProperty("email")] public string Email;
        [JsonProperty("iron")] public int Iron;
        [JsonProperty("wood")] public int Wood;
        [JsonProperty("usedmultiplies")] public bool Multiplies;


        public override string ToString()
        {
            return $"[ID:{ID} | {GetResoultString()} | LostB:{LostBoxes} | Score:{Score} | Time:{Time}]";
        }

        public static implicit operator JToken(ResoultModel mod)
        {
            return JsonConvert.SerializeObject(mod);
        }

        private string GetResoultString()
        {
            return IsWin ? "Win" : "Lose";
        }
    }
}