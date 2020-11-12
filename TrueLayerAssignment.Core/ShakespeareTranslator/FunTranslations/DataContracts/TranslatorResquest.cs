using Newtonsoft.Json;

namespace TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations.DataContracts
{
    internal class TranslatorResquest
    {
        public TranslatorResquest(string text)
        {
            this.Text = text;
        }

        [JsonProperty("text", Required = Required.Always)]
        public string Text { get; }
    }
}
