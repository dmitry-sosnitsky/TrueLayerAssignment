using System;

namespace TrueLayerAssignment.Core
{
    /// <summary>
    /// Exception thrown when no Pokemon with the requested name could be found
    /// </summary>
    public class PokemonNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonNotFoundException"/> class
        /// </summary>
        /// <param name="pokemonName">Requested Pokemon name</param>
        public PokemonNotFoundException(string pokemonName)
            : base($"Pokemon with name '{pokemonName}' not found")
        { }
    }
}