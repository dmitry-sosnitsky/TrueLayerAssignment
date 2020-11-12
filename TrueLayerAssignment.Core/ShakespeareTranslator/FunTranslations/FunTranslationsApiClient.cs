using System.Threading.Tasks;
using RestSharp;
using TrueLayerAssignment.Core.Integrations;
using TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations.DataContracts;

namespace TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations
{
    /// <summary>
    /// Provides Shakespearian translation using funtranslations.com API
    /// </summary>
    public class FunTranslationsApiClient : ApiClientBase, IShakespeareTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunTranslationsApiClient"/> class
        /// </summary>
        /// <param name="baseAddress">Base API address</param>
        /// <param name="restClient">An instance of <see cref="IRestClient"/></param>
        public FunTranslationsApiClient(string baseAddress, IRestClient restClient)
            : base(baseAddress, restClient)
        { }

        /// <inheritdoc/>
        public async Task<string> GetTranslation(string text)
        {
            var request = new RestRequest("shakespeare", Method.POST);
            request.AddJsonBody(new TranslatorResquest(text));
            var response = await this.PerformRequest<TranslatorResponse>(request);

            return response.Contents.TranslatedText;
        }
    }
}
