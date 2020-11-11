using System.Threading.Tasks;

namespace TrueLayerAssignment.Core
{
    /// <summary>
    /// Provides description of a Pokemon
    /// </summary>
    public interface IPokemonDescriptionProvider
    {
        /// <summary>
        /// Gets the Shakesperian-style description of a pokemon
        /// </summary>
        /// <param name="pokemonName">The name of the pokemon</param>
        /// <param name="version">The game version the description should be extracted from</param>
        /// <returns>Shakesperian-style description of a pokemon</returns>
        Task<string> GetShakesperianDescription(string pokemonName, GameVersion version);
    }
}
