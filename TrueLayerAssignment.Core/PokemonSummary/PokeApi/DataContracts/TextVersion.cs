using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts
{
    internal class TextVersion
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
    }
}
