using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations.DataContracts
{
    internal class TranslatorResponse
    {
        [JsonProperty("success", Required = Required.Always)]
        public SuccessfulTranslationsCount SuccessfulTranslationsCount { get; set; }

        [JsonProperty("contents", Required = Required.Always)]
        public TranslationContents Contents { get; set; }
    }
}
