using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts
{
    internal class PokemonSpecies
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("flavor_text_entries", Required = Required.Always)]
        public IReadOnlyCollection<TextEntry> TextEntries { get; set; }
    }
}
