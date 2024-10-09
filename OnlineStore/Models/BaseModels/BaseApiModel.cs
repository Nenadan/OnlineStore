using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace OnlineStore.Models.BaseModels
{
    public class BaseApiModel<T>
    {
        public T Id { get; set; }
        public string DocType { get; set; }
        [JsonProperty("id")]
        public string CosmosDbId => $"{DocType}|{Id}";
    }
}
