using System;
using System.Collections.Generic;
using System.Linq;
using TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts;

namespace TrueLayerAssignment.Core.PokemonSummary
{
    /// <summary>
    /// Information about a Pokemon cpecies
    /// </summary>
    public class PokemonSpeciesSummary
    {
        internal PokemonSpeciesSummary(PokemonSpecies species)
        {
            this.Name = species.Name;
            this.Descriptions = species.TextEntries
                .Where(x => x.Language.IsEnglish)
                .Select(x => new PokemonSpeciesDescription(x.Text, x.Version.Name))
                .ToList();
        }

        /// <summary>
        /// Species name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Descriptions from differnrt game versions
        /// </summary>
        public IReadOnlyCollection<PokemonSpeciesDescription> Descriptions { get; }

        /// <summary>
        /// Gets the description from a specific game version
        /// </summary>
        /// <param name="version">Game version</param>
        /// <returns>Description text, or null (if description for version is not found)</returns>
        public string GetDescription(GameVersion version)
        {
            switch (version)
            {
                case GameVersion.Any:
                    return this.Descriptions.FirstOrDefault()?.Description;
                default:
                    return this.Descriptions.FirstOrDefault(x => x.GameVersion.Equals(version.ToString(), StringComparison.InvariantCultureIgnoreCase))?.Description;
            }
        }
    }
}
