using System.Threading.Tasks;

namespace TrueLayerAssignment.Core.PokemonSummary
{
    /// <summary>
    /// Provides Pokemon species details
    /// </summary>
    public interface IPokemonSpeciesSummaryProvider
    {
        /// <summary>
        /// Given a Pokemon name, retrieves species details
        /// </summary>
        /// <param name="pokemonName">Pokemon name</param>
        /// <returns>An instance of <see cref="PokemonSpeciesSummary"/> class</returns>
        Task<PokemonSpeciesSummary> GetSpecies(string pokemonName);
    }
}
