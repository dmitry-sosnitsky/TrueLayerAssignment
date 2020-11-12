using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrueLayerAssignment.Core;
using TrueLayerAssignment.Web.DataContracts;

namespace TrueLayerAssignment.Web.Controllers
{
    /// <summary>
    /// Controller responsible for retieveing Pokemon descriptions
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonDescriptionProvider pokemonDescriptionProvider;

        public PokemonController(IPokemonDescriptionProvider pokemonDescriptionProvider)
        {
            this.pokemonDescriptionProvider = pokemonDescriptionProvider ?? throw new ArgumentNullException(nameof(pokemonDescriptionProvider));
        }

        /// <summary>
        /// Retrieves the Shakespearian description of a pokemon
        /// </summary>
        /// <param name="request">Request containing pokemon name and game version</param>
        /// <returns>Action result</returns>
        [HttpGet("{name}/{version?}")]
        public async Task<IActionResult> GetShakespearianDescription([FromRoute] GetShakespearianDescriptionRequest request)
        {
            var validationResult = request.Validate();
            if (!validationResult.Success)
            {
                return this.BadRequest(validationResult);
            }

            try
            {
                string description = await this.pokemonDescriptionProvider.GetShakesperianDescription(request.Name, request.Version);
                var response = new GetShakespearianDescriptionResponse(request.Name, description);

                return this.Ok(response);
            }
            catch (Exception e) when (e is PokemonNotFoundException || e is DescriptionForVersionNotFoundException)
            {
                return this.NotFound(ValidationResult.Failed(e.Message));
            }
        }
    }
}
