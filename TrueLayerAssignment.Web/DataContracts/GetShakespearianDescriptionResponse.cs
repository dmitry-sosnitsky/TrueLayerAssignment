using System.Text.Json.Serialization;

namespace TrueLayerAssignment.Web.DataContracts
{
    /// <summary>
    /// Response containing the Shakespearian description of a Pokemon
    /// </summary>
    public class GetShakespearianDescriptionResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetShakespearianDescriptionResponse"/> class
        /// </summary>
        /// <param name="name">Pokemon name</param>
        /// <param name="description">Pokemon description</param>
        public GetShakespearianDescriptionResponse(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Pokemon name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Pokemon description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
