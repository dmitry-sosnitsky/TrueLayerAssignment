using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace TrueLayerAssignment.Core.Integrations
{
    /// <summary>
    /// Base class for clients communicating with REST API endpoints
    /// </summary>
    public abstract class ApiClientBase
    {
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientBase"/> class
        /// </summary>
        /// <param name="baseAddress">Base API address</param>
        /// <param name="restClient">An instance of <see cref="IRestClient"/></param>
        /// <exception cref="ArgumentException">Thrown when <see cref="baseAddress"/> is not a valid absolute URI</exception>
        /// /// <exception cref="ArgumentNullException">Thrown when <see cref="restClient"/> is null</exception>
        public ApiClientBase(string baseAddress, IRestClient restClient)
        {
            this.restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));

            if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
            {
                throw new ArgumentException($"Invalid address '{baseAddress}'", nameof(baseAddress));
            }

            this.restClient.BaseUrl = uri;
            this.restClient.UseSerializer<NewtonsoftJsonSerializer>();
        }

        protected async Task<TResponse> PerformRequest<TResponse>(IRestRequest request)
        {
            var response = await this.restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new ApiClientException(restClient, request, response);
            }

            try
            {
                return JsonConvert.DeserializeObject<TResponse>(response.Content);
            }
            catch (Exception e)
            {
                throw new ApiClientException($"Error deserializing response into type '{typeof(TResponse).Name}'", e);
            }
        }
    }
}
