using System;
using System.Net;
using RestSharp;

namespace TrueLayerAssignment.Core.Integrations
{
    [Serializable]
    internal class ApiClientException : Exception
    {
        public ApiClientException(IRestClient restClient, IRestRequest request, IRestResponse response)
            : base($"Error sending request '{request.Method} {restClient.BuildUri(request)}', status {response.StatusCode}, response '{response.Content ?? "<EMPTY>"}'", response.ErrorException)
        {
            this.ResponseCode = response.StatusCode;
        }

        public ApiClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public HttpStatusCode ResponseCode { get; }
    }
}