
using Newtonsoft.Json;

public class PlayerModel
{
    [JsonProperty("name")]          public string Name;
    [JsonProperty("wood")]          public int Wood;
    [JsonProperty("iron")]          public int Iron;
    [JsonProperty("completed")]     public int[] CompletedLeves;
    [JsonProperty("premium")]       public bool Premium;
}
