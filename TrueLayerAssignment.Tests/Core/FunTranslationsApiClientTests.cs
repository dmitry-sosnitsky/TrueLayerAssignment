using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TrueLayerAssignment.Core.Integrations;
using TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations;
using TrueLayerAssignment.Core.ShakespeareTranslator.FunTranslations.DataContracts;

namespace TrueLayerAssignment.Tests.Core
{
    public class FunTranslationsApiClientTests
    {
        [Test]
        public async Task When_response_is_successful_Should_return_correct_result()
        {
            // arrange
            var translatorResponse = new TranslatorResponse
            {
                SuccessfulTranslationsCount = new SuccessfulTranslationsCount
                {
                    Total = 1
                },
                Contents = new TranslationContents
                {
                    OriginalText = "text",
                    TranslatedText = "translated text"
                }
            };
            var restCLientMock = new Mock<IRestClient>();
            restCLientMock
                .Setup(x => x.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = HttpStatusCode.OK, Content = JsonConvert.SerializeObject(translatorResponse), ResponseStatus = ResponseStatus.Completed });
            var client = new FunTranslationsApiClient("http://localhost", restCLientMock.Object);

            // act
            var result = await client.GetTranslation("text");

            // assert
            result.Should().Be("translated text");
        }

        [Test]
        public void When_response_is_error_Should_throw_correct_exception()
        {
            // arrange
            var restCLientMock = new Mock<IRestClient>();
            restCLientMock
                .Setup(x => x.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = HttpStatusCode.InternalServerError, ResponseStatus = ResponseStatus.Completed });
            var client = new FunTranslationsApiClient("http://localhost", restCLientMock.Object);

            // act
            Func<Task> func = () => client.GetTranslation("");

            // assert
            func.Should().Throw<ApiClientException>();
        }
    }
}
