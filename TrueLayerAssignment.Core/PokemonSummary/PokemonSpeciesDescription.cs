namespace TrueLayerAssignment.Core.PokemonSummary
{
    /// <summary>
    /// Description of a Pokemon species
    /// </summary>
    public class PokemonSpeciesDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PokemonSpeciesDescription"/> class
        /// </summary>
        /// <param name="description">Description text</param>
        /// <param name="name">Pokemon name</param>
        public PokemonSpeciesDescription(string description, string name)
        {
            this.Description = description;
            this.GameVersion = name;
        }

        /// <summary>
        /// Description text
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Game version this description should be extracted from
        /// </summary>
        public string GameVersion { get; set; }
    }
}
