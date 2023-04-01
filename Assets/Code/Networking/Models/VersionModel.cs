using Newtonsoft.Json;

namespace Networking.Models
{
    public class VersionModel
    {
        [JsonProperty("api")] public string Api;
        [JsonProperty("client")] public string Client;

        public VersionModel(string api, string client)
        {
            Api = api;
            Client = client;
        }

        public override string ToString()
        {
            return $"[Api:{Api} | Client:{Client}]";
        }
    }
}