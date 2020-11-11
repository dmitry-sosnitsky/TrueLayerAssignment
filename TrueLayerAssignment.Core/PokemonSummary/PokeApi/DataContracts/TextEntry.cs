using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts
{
    internal class TextEntry
    {
        [JsonProperty("flavor_text", Required = Required.Always)]
        public string Text { get; set; }

        [JsonProperty("language", Required = Required.Always)]
        public Language Language { get; set; }

        [JsonProperty("version", Required = Required.Always)]
        public TextVersion Version { get; set; }
    }
}
