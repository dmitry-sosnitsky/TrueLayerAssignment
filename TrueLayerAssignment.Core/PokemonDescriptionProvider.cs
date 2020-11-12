using System;
using System.Threading.Tasks;
using TrueLayerAssignment.Core.PokemonSummary;
using TrueLayerAssignment.Core.ShakespeareTranslator;

namespace TrueLayerAssignment.Core
{
    /// <inheritdoc/>
    public class PokemonDescriptionProvider : IPokemonDescriptionProvider
    {
        private readonly IPokemonSpeciesSummaryProvider pokemonSpeciesSummaryProvider;
        private readonly IShakespeareTranslator shakespeareTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonDescriptionProvider"/> class
        /// </summary>
        /// <param name="pokemonSpeciesSummaryProvider">Pokemon species summary provider</param>
        /// <param name="shakespeareTranslator">Shakespeare translator</param>
        public PokemonDescriptionProvider(IPokemonSpeciesSummaryProvider pokemonSpeciesSummaryProvider, IShakespeareTranslator shakespeareTranslator)
        {
            this.pokemonSpeciesSummaryProvider = pokemonSpeciesSummaryProvider ?? throw new ArgumentNullException(nameof(pokemonSpeciesSummaryProvider));
            this.shakespeareTranslator = shakespeareTranslator ?? throw new ArgumentNullException(nameof(shakespeareTranslator));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when<see cref="pokemonName"/> is null.</exception>
        /// <exception cref="DescriptionForVersionNotFoundException">Thrown when description for version <see cref="version"/> cannot be found.</exception>
        /// <exception cref="PokemonNotFoundException">Thrown when Pokemon with name <see cref="pokemonName"/> cannot be found.</exception>
        public async Task<string> GetShakesperianDescription(string pokemonName, GameVersion version)
        {
            if (pokemonName == null)
            {
                throw new ArgumentNullException(nameof(pokemonName));
            }

            var speciesSummary = await this.pokemonSpeciesSummaryProvider.GetSpecies(pokemonName);
            string plainDescription = speciesSummary.GetDescription(version);
            if (plainDescription == null)
            {
                throw new DescriptionForVersionNotFoundException(version);
            }

            string shakesperianDescription = await this.shakespeareTranslator.GetTranslation(plainDescription);

            return shakesperianDescription;
        }
    }
}
