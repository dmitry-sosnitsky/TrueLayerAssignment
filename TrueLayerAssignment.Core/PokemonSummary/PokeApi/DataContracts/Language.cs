using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts
{
    internal class Language
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        public bool IsEnglish => this.Name == "en";
    }
}
