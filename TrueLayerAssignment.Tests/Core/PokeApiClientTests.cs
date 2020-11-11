using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TrueLayerAssignment.Core;
using TrueLayerAssignment.Core.Integrations;
using TrueLayerAssignment.Core.PokemonSummary.PokeApi;
using TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts;

namespace TrueLayerAssignment.Tests.Core
{
    public class PokeApiClientTests
    {
        [Test]
        public async Task When_response_is_successful_Should_return_correct_result()
        {
            // arrange
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "Pikachu",
                TextEntries = new[]
                {
                    new TextEntry
                    {
                        Text = "Description 1",
                        Language = new Language
                        {
                            Name = "en"
                        },
                        Version = new TextVersion
                        {
                            Name = "Gold"
                        }
                    }
                }
            };
            var restCLientMock = new Mock<IRestClient>();
            restCLientMock
                .Setup(x => x.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = HttpStatusCode.OK, Content = JsonConvert.SerializeObject(pokemonSpecies), ResponseStatus = ResponseStatus.Completed });
            var client = new PokeApiClient("http://localhost", restCLientMock.Object);

            // act
            var result = await client.GetSpecies("Pikachu");

            // assert
            result.Name.Should().Be("Pikachu");
            result.Descriptions.Count.Should().Be(1);
            result.Descriptions.Single().Description.Should().Be("Description 1");
        }

        [Test]
        public void When_response_is_not_found_Should_throw_correct_exception()
        {
            // arrange
            var restCLientMock = new Mock<IRestClient>();
            restCLientMock
                .Setup(x => x.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = HttpStatusCode.NotFound, ResponseStatus = ResponseStatus.Completed });
            var client = new PokeApiClient("http://localhost", restCLientMock.Object);

            // act
            Func<Task> func = () => client.GetSpecies("");

            // assert
            func.Should().Throw<PokemonNotFoundException>();
        }

        [Test]
        public void When_response_is_error_Should_throw_correct_exception()
        {
            // arrange
            var restCLientMock = new Mock<IRestClient>();
            restCLientMock
                .Setup(x => x.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = HttpStatusCode.InternalServerError, ResponseStatus = ResponseStatus.Completed });
            var client = new PokeApiClient("http://localhost", restCLientMock.Object);

            // act
            Func<Task> func = () => client.GetSpecies("");

            // assert
            func.Should().Throw<ApiClientException>();
        }
    }
}
