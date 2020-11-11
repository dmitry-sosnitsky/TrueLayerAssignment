using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TrueLayerAssignment.Core;
using TrueLayerAssignment.Core.PokemonSummary;
using TrueLayerAssignment.Core.PokemonSummary.PokeApi.DataContracts;

namespace TrueLayerAssignment.Tests.Core
{
    public class PokemonDescriptionProviderTests
    {
        [Test]
        public async Task When_multiple_english_descriptions_exist_And_any_version_is_requested_Should_return_first_description()
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
                    },
                    new TextEntry
                    {
                        Text = "Description 2",
                        Language = new Language
                        {
                            Name = "en"
                        },
                        Version = new TextVersion
                        {
                            Name = "Silver"
                        }
                    }
                }
            };
            var pokemonSpeciesSummaryProviderMock = new Mock<IPokemonSpeciesSummaryProvider>();
            pokemonSpeciesSummaryProviderMock.Setup(x => x.GetSpecies(It.IsAny<string>())).ReturnsAsync(new PokemonSpeciesSummary(pokemonSpecies));
            var provider = new PokemonDescriptionProvider(pokemonSpeciesSummaryProviderMock.Object);

            // act
            var result = await provider.GetShakesperianDescription("Pikachu", GameVersion.Any);

            // assert
            result.Should().Be("Description 1");
        }

        [Test]
        public async Task When_descriptions_in_sevral_languages_exist_And_any_version_is_requested_Should_return_english_description()
        {
            // arrange
            var pokemonSpecies = new PokemonSpecies
            {
                Name = "Pikachu",
                TextEntries = new[]
                {
                    new TextEntry
                    {
                        Text = "Japanese description",
                        Language = new Language
                        {
                            Name = "ja"
                        },
                        Version = new TextVersion
                        {
                            Name = "Gold"
                        }
                    },
                    new TextEntry
                    {
                        Text = "English description",
                        Language = new Language
                        {
                            Name = "en"
                        },
                        Version = new TextVersion
                        {
                            Name = "Silver"
                        }
                    }
                }
            };
            var pokemonSpeciesSummaryProviderMock = new Mock<IPokemonSpeciesSummaryProvider>();
            pokemonSpeciesSummaryProviderMock.Setup(x => x.GetSpecies(It.IsAny<string>())).ReturnsAsync(new PokemonSpeciesSummary(pokemonSpecies));
            var provider = new PokemonDescriptionProvider(pokemonSpeciesSummaryProviderMock.Object);

            // act
            var result = await provider.GetShakesperianDescription("Pikachu", GameVersion.Any);

            // assert
            result.Should().Be("English description");
        }

        [Test]
        public async Task When_descriptions_for_multiple_versions_exist_Should_return_description_for_requested_version()
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
                    },
                    new TextEntry
                    {
                        Text = "Description 2",
                        Language = new Language
                        {
                            Name = "en"
                        },
                        Version = new TextVersion
                        {
                            Name = "Silver"
                        }
                    }
                }
            };
            var pokemonSpeciesSummaryProviderMock = new Mock<IPokemonSpeciesSummaryProvider>();
            pokemonSpeciesSummaryProviderMock.Setup(x => x.GetSpecies(It.IsAny<string>())).ReturnsAsync(new PokemonSpeciesSummary(pokemonSpecies));
            var provider = new PokemonDescriptionProvider(pokemonSpeciesSummaryProviderMock.Object);

            // act
            var result = await provider.GetShakesperianDescription("Pikachu", GameVersion.Silver);

            // assert
            result.Should().Be("Description 2");
        }

        [Test]
        public void When_descriptions_for_version_does_not_exist_Should_throw_exception()
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
            var pokemonSpeciesSummaryProviderMock = new Mock<IPokemonSpeciesSummaryProvider>();
            pokemonSpeciesSummaryProviderMock.Setup(x => x.GetSpecies(It.IsAny<string>())).ReturnsAsync(new PokemonSpeciesSummary(pokemonSpecies));
            var provider = new PokemonDescriptionProvider(pokemonSpeciesSummaryProviderMock.Object);

            // act
            Func<Task> func = () => provider.GetShakesperianDescription("Pikachu", GameVersion.Silver);

            // assert
            func.Should().Throw<DescriptionForVersionNotFoundException>();
        }
    }
}
