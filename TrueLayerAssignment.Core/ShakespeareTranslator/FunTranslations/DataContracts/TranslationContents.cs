using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations.DataContracts
{
    internal class TranslationContents
    {
        [JsonProperty("translated", Required = Required.Always)]
        public string TranslatedText { get; set; }

        [JsonProperty("text", Required = Required.Always)]
        public string OriginalText { get; set; }
    }
}
