using System.Net;
using System.Threading.Tasks;
using RestSharp;
using TrueLayerAssignment.Core.Integrations;
using TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts;

namespace TrueLayerAssignment.Core.PokemonSummary.PokeApi
{
    /// <summary>
    /// Provides Pokemon details from PokeApi.co service
    /// </summary>
    public class PokeApiClient : ApiClientBase, IPokemonSpeciesSummaryProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PokeApiClient"/> class
        /// </summary>
        /// <param name="baseAddress">Base API address</param>
        /// <param name="restClient">An instance of <see cref="IRestClient"/></param>
        public PokeApiClient(string baseAddress, IRestClient restClient)
            : base(baseAddress, restClient)
        { }

        /// <inheritdoc/>
        public async Task<PokemonSpeciesSummary> GetSpecies(string pokemonName)
        {
            var request = new RestRequest($"pokemon-species/{pokemonName}", Method.GET);
            try
            {
                var species = await this.PerformRequest<PokemonSpecies>(request);

                return new PokemonSpeciesSummary(species);
            }
            catch (ApiClientException e) when (e.ResponseCode == HttpStatusCode.NotFound)
            {
                throw new PokemonNotFoundException(pokemonName);
            }
        }
    }
}
