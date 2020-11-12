using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations.DataContracts
{
    internal class SuccessfulTranslationsCount
    {
        [JsonProperty("total", Required = Required.Always)]
        public int Total { get; set; }
    }
}
