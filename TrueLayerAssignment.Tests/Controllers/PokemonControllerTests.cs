using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TrueLayerAssignment.Core;
using TrueLayerAssignment.Web.Controllers;
using TrueLayerAssignment.Web.DataContracts;

namespace TrueLayerAssignment.Tests.Controllers
{
    public class PokemonControllerTests
    {
        [Test]
        public async Task When_provider_call_is_successful_Should_return_correct_result()
        {
            // arrange
            var request = new GetShakespearianDescriptionRequest
            {
                Name = "Pikachu",
                Version = GameVersion.Any
            };
            var pokemonDescriptionProviderMock = new Mock<IPokemonDescriptionProvider>();
            pokemonDescriptionProviderMock
                .Setup(x => x.GetShakesperianDescription(request.Name, request.Version))
                .ReturnsAsync("description");
            var controller = new PokemonController(pokemonDescriptionProviderMock.Object);

            // act
            var result = await controller.GetShakespearianDescription(request);

            // assert
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var getShakespearianDescriptionResponse = okObjectResult.Value.Should().BeOfType<GetShakespearianDescriptionResponse>().Subject;
            getShakespearianDescriptionResponse.Name.Should().Be(request.Name);
            getShakespearianDescriptionResponse.Description.Should().Be("description");
        }

        [Test]
        public async Task When_request_has_empty_name_Should_return_validation_error()
        {
            // arrange
            var controller = new PokemonController(new Mock<IPokemonDescriptionProvider>().Object);

            // act
            var result = await controller.GetShakespearianDescription(new GetShakespearianDescriptionRequest());

            // assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var validationResult = badRequestResult.Value.Should().BeOfType<ValidationResult>().Subject;
            validationResult.ErrorMessage.Should().Be("Name is empty");
        }

        [Test]
        public async Task When_request_has_long_name_Should_return_validation_error()
        {
            // arrange
            var controller = new PokemonController(new Mock<IPokemonDescriptionProvider>().Object);
            var request = new GetShakespearianDescriptionRequest
            {
                Name = new string('x', 101)
            };

            // act
            var result = await controller.GetShakespearianDescription(request);

            // assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var validationResult = badRequestResult.Value.Should().BeOfType<ValidationResult>().Subject;
            validationResult.ErrorMessage.Should().Be("Name too long");
        }

        [Test]
        public async Task When_request_has_invalid_version_Should_return_validation_error()
        {
            // arrange
            var controller = new PokemonController(new Mock<IPokemonDescriptionProvider>().Object);
            var request = new GetShakespearianDescriptionRequest
            {
                Name = "Pikachu",
                Version = (GameVersion)99
            };

            // act
            var result = await controller.GetShakespearianDescription(request);

            // assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var validationResult = badRequestResult.Value.Should().BeOfType<ValidationResult>().Subject;
            validationResult.ErrorMessage.Should().Be("Invalid version");
        }

        [Test]
        public async Task When_provider_call_throws_PokemonNotFoundException_Should_return_not_found()
        {
            // arrange
            var pokemonDescriptionProviderMock = new Mock<IPokemonDescriptionProvider>();
            pokemonDescriptionProviderMock
                .Setup(x => x.GetShakesperianDescription(It.IsAny<string>(), It.IsAny<GameVersion>()))
                .ThrowsAsync(new PokemonNotFoundException("xyz"));
            var controller = new PokemonController(pokemonDescriptionProviderMock.Object);

            // act
            var result = await controller.GetShakespearianDescription(new GetShakespearianDescriptionRequest { Name = "xyz" });

            // assert
            var notFountResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            var validationResult = notFountResult.Value.Should().BeOfType<ValidationResult>().Subject;
            validationResult.ErrorMessage.Should().Be("Pokemon with name 'xyz' not found");
        }
    }
}
