using System;
using System.Linq;
using System.Text.Json.Serialization;
using TrueLayerAssignment.Core;

namespace TrueLayerAssignment.Web.DataContracts
{
    /// <summary>
    /// Request to get the Shakespearian description of a Pokemon
    /// </summary>
    public class GetShakespearianDescriptionRequest
    {
        /// <summary>
        /// Pokemon name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The game version the description should be extracted from
        /// </summary>
        [JsonPropertyName("version")]
        public GameVersion Version { get; set; } = GameVersion.Any;

        /// <summary>
        /// Performs validation of current request
        /// </summary>
        /// <returns>An instance of <see cref="ValidationResult"/> class</returns>
        public ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return ValidationResult.Failed("Name is empty");
            }

            if (this.Name.Length > 100)
            {
                return ValidationResult.Failed("Name too long");
            }

            if (!Enum.IsDefined(typeof(GameVersion), this.Version))
            {
                return ValidationResult.Failed("Invalid version");
            }

            return ValidationResult.Ok();
        }
    }
}
