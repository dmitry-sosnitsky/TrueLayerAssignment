using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TrueLayerAssignment.Core;

namespace TrueLayerAssignment.Tests.Core
{
    public class PokemonDescriptionProviderTests
    {
        [Test]
        public void When_GetShakesperianDescription_is_called_Should_throw_PokemonNotFoundException()
        {
            // arrange
            var provider = new PokemonDescriptionProvider();

            // act
            Func<Task> func = () => provider.GetShakesperianDescription("Pikachu", GameVersion.Silver);

            // assert
            func.Should().Throw<PokemonNotFoundException>();
        }
    }
}
