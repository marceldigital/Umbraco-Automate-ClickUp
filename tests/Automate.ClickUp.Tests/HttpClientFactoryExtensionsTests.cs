using Moq;
using Umbraco.Automate.OpenIddict.Credentials;
using Umbraco.Community.Automate.ClickUp.Connection;
using Xunit;

namespace Umbraco.Community.Automate.ClickUp.Tests;

public sealed class HttpClientFactoryExtensionsTests
{
    private static readonly Guid CredentialsId = Guid.NewGuid();

    private static Mock<IOAuthCredentialsService> MockCredentialsService(string? token)
    {
        var mock = new Mock<IOAuthCredentialsService>();
        mock.Setup(s => s.GetValidAccessTokenAsync(CredentialsId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(token);
        return mock;
    }

    private static IHttpClientFactory FakeHttpClientFactory()
    {
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient("UmbracoAutomate")).Returns(() => new HttpClient());
        return factory.Object;
    }

    public sealed class CreateClickUpClientAsync
    {
        [Fact]
        public async Task WhenConnectionSettingsIsNull_Throws()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                FakeHttpClientFactory().CreateClickUpClientAsync(
                    null, MockCredentialsService("token").Object, CancellationToken.None));
        }

        [Fact]
        public async Task WhenOAuthCredentialsIdIsNull_Throws()
        {
            var connectionSettings = new ClickUpConnectionSettings { OAuthCredentialsId = null };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                FakeHttpClientFactory().CreateClickUpClientAsync(
                    connectionSettings, MockCredentialsService("token").Object, CancellationToken.None));
        }

        [Fact]
        public async Task WhenOAuthCredentialsIdIsEmpty_Throws()
        {
            var connectionSettings = new ClickUpConnectionSettings { OAuthCredentialsId = Guid.Empty };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                FakeHttpClientFactory().CreateClickUpClientAsync(
                    connectionSettings, MockCredentialsService("token").Object, CancellationToken.None));
        }

        [Fact]
        public async Task WhenAccessTokenIsUnavailable_Throws()
        {
            var connectionSettings = new ClickUpConnectionSettings { OAuthCredentialsId = CredentialsId };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                FakeHttpClientFactory().CreateClickUpClientAsync(
                    connectionSettings, MockCredentialsService(null).Object, CancellationToken.None));
        }

        [Fact]
        public async Task WhenSuccessful_ReturnsClientWithBaseAddressAndAuthorizationHeader()
        {
            var connectionSettings = new ClickUpConnectionSettings { OAuthCredentialsId = CredentialsId };

            using var client = await FakeHttpClientFactory().CreateClickUpClientAsync(
                connectionSettings, MockCredentialsService("test-token").Object, CancellationToken.None);

            Assert.Equal(new Uri("https://api.clickup.com/api/v2/"), client.BaseAddress);
            Assert.Equal("Bearer", client.DefaultRequestHeaders.Authorization?.Scheme);
            Assert.Equal("test-token", client.DefaultRequestHeaders.Authorization?.Parameter);
        }
    }
}
