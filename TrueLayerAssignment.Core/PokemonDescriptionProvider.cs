using System;
using System.Threading.Tasks;

namespace TrueLayerAssignment.Core
{
    /// <inheritdoc/>
    public class PokemonDescriptionProvider : IPokemonDescriptionProvider
    {
        /// <inheritdoc/>
        /// <exception cref="PokemonNotFoundException">Thrown when Pokemon with name <see cref="pokemonName"/> cannot be found.</exception>
        public Task<string> GetShakesperianDescription(string pokemonName, GameVersion version)
        {
            throw new PokemonNotFoundException(pokemonName);
        }
    }
}
