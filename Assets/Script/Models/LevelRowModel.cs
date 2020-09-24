using Newtonsoft.Json;

public class LevelRowModel
{
    [JsonProperty("levels")]    public LevelModel[] Levels;
    [JsonProperty("color")]     public string Color;
}