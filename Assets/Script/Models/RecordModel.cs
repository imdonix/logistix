using Newtonsoft.Json;
using System;
using UnityEngine;

public class RecordModel : IComparable<RecordModel>
{ 
    [JsonProperty("name")] public string Name;
    [JsonProperty("premium")] public bool Premium;
    [JsonProperty("score")] public int Score;

    public int CompareTo(RecordModel other)
    {
        return other.Score - Score;
    }

    public override bool Equals(object obj)
    {
        return (obj is RecordModel record && Name == record.Name) ||
               (obj is PlayerModel palyer && Name == palyer.Name);
    }

    public override int GetHashCode()
    {
        return Score * Name.Length;
    }

    public override string ToString()
    {
        return $"{(Premium ? '$' : '.')}{Name} : {Score}";
    }
}
