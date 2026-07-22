using System.Net;
using System.Text;
using Umbraco.Automate.Core.Actions;
using Umbraco.Community.Automate.ClickUp;
using Xunit;

namespace Umbraco.Community.Automate.ClickUp.Tests;

public sealed class HttpResponseMessageExtensionsTests
{
    public sealed class ToClickUpFailureAsync
    {
        [Theory]
        [InlineData(HttpStatusCode.Unauthorized, StepRunErrorCategory.Authentication)]
        [InlineData(HttpStatusCode.Forbidden, StepRunErrorCategory.Authentication)]
        [InlineData((HttpStatusCode)429, StepRunErrorCategory.RateLimiting)]
        [InlineData(HttpStatusCode.BadRequest, StepRunErrorCategory.Validation)]
        [InlineData(HttpStatusCode.NotFound, StepRunErrorCategory.Validation)]
        [InlineData(HttpStatusCode.InternalServerError, StepRunErrorCategory.ServiceUnavailable)]
        [InlineData(HttpStatusCode.ServiceUnavailable, StepRunErrorCategory.ServiceUnavailable)]
        [InlineData(HttpStatusCode.Conflict, StepRunErrorCategory.InvalidResponse)]
        public async Task WhenCalledWithStatusCode_MapsToExpectedCategory(
            HttpStatusCode statusCode, StepRunErrorCategory expectedCategory)
        {
            using var response = new HttpResponseMessage(statusCode);

            var result = await response.ToClickUpFailureAsync(CancellationToken.None);

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(expectedCategory, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenErrorBodyHasErrField_UsesItInExceptionMessage()
        {
            using var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("""{ "err": "List not found" }""", Encoding.UTF8, "application/json"),
            };

            var result = await response.ToClickUpFailureAsync(CancellationToken.None);

            Assert.Contains("List not found", result.Exception?.Message);
        }

        [Fact]
        public async Task WhenBodyIsNotJson_FallsBackToReasonPhrase()
        {
            using var response = new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                ReasonPhrase = "Bad Gateway",
                Content = new StringContent("<html>not json</html>", Encoding.UTF8, "text/html"),
            };

            var result = await response.ToClickUpFailureAsync(CancellationToken.None);

            Assert.Contains("Bad Gateway", result.Exception?.Message);
        }

        [Fact]
        public async Task WhenBodyIsEmpty_FallsBackToReasonPhrase()
        {
            using var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = "Internal Server Error",
            };

            var result = await response.ToClickUpFailureAsync(CancellationToken.None);

            Assert.Contains("Internal Server Error", result.Exception?.Message);
        }
    }
}
