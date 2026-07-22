using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.OpenIddict.Credentials;
using Umbraco.Automate.Testing;
using Umbraco.Community.Automate.ClickUp.Actions;
using Umbraco.Community.Automate.ClickUp.Connection;
using Xunit;

namespace Umbraco.Community.Automate.ClickUp.Tests.Actions;

public sealed class CreateTaskActionTests
{
    private const string AccessToken = "test-access-token";

    private static readonly Guid CredentialsId = Guid.NewGuid();

    private static CreateTaskSettings ValidSettings() => new()
    {
        ListId = "900200745963",
        Name = "New Task Name",
    };

    private static ClickUpConnectionSettings ValidConnectionSettings() => new()
    {
        OAuthCredentialsId = CredentialsId,
    };

    private static string SuccessResponseJson() => """
        {
          "id": "9hx",
          "custom_id": null,
          "name": "New Task Name",
          "status": { "status": "to do", "type": "open" },
          "date_created": "1567780450202",
          "creator": { "id": 183, "username": "Alex Johnson" },
          "parent": null,
          "top_level_parent": null,
          "priority": { "priority": "normal" },
          "url": "https://app.clickup.com/t/9hx",
          "list": { "id": "15505202", "name": "Sprint Backlog" },
          "folder": { "id": "6992470" },
          "project": { "id": "6992470" },
          "space": { "id": "7002367" }
        }
        """;

    private static Mock<IOAuthCredentialsService> MockCredentialsService(string? token = AccessToken)
    {
        var mock = new Mock<IOAuthCredentialsService>();
        mock.Setup(s => s.GetValidAccessTokenAsync(CredentialsId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(token);
        return mock;
    }

    private static IHttpClientFactory FakeHttpClientFactory(HttpStatusCode statusCode, string? responseBody)
    {
        var handler = new FakeHttpMessageHandler(statusCode, responseBody);
        var httpClient = new HttpClient(handler);

        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient("UmbracoAutomate")).Returns(httpClient);
        return factory.Object;
    }

    private static ActionTestHarness<CreateTaskAction> BuildHarness(
        CreateTaskSettings? settings = null,
        ClickUpConnectionSettings? connectionSettings = null,
        bool withConnection = true,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        string? responseBody = null,
        string? accessToken = AccessToken)
    {
        var harness = ActionTestHarness.For<CreateTaskAction>()
            .WithSettings(settings ?? ValidSettings())
            .WithService(FakeHttpClientFactory(statusCode, responseBody ?? SuccessResponseJson()))
            .WithService(MockCredentialsService(accessToken).Object);

        if (withConnection)
        {
            harness = harness.WithConnection(ClickUpConnectionType.ConnectionAlias, connectionSettings ?? ValidConnectionSettings());
        }

        return harness;
    }

    private sealed class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string? _responseBody;

        public FakeHttpMessageHandler(HttpStatusCode statusCode, string? responseBody)
        {
            _statusCode = statusCode;
            _responseBody = responseBody;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode);
            if (_responseBody is not null)
            {
                response.Content = new StringContent(_responseBody, Encoding.UTF8, "application/json");
            }

            return Task.FromResult(response);
        }
    }

    public sealed class ExecuteAsync
    {
        [Fact]
        public async Task WhenListIdMissing_ReturnsValidationFailure()
        {
            var settings = ValidSettings();
            settings.ListId = "";

            var result = await BuildHarness(settings).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Validation, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenNameMissing_ReturnsValidationFailure()
        {
            var settings = ValidSettings();
            settings.Name = "";

            var result = await BuildHarness(settings).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Validation, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenDueDateIsMalformed_ReturnsValidationFailure()
        {
            var settings = ValidSettings();
            settings.DueDate = "not-a-date";

            var result = await BuildHarness(settings).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Validation, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenStartDateIsMalformed_ReturnsValidationFailure()
        {
            var settings = ValidSettings();
            settings.StartDate = "not-a-date";

            var result = await BuildHarness(settings).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Validation, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenDueDateHasTimeOfDay_SendsDueDateTimeTrue()
        {
            var settings = ValidSettings();
            settings.DueDate = "2026-08-01T14:30:00Z";
            HttpRequestMessage? capturedRequest = null;
            string? capturedBody = null;

            var handler = new CapturingHandler(HttpStatusCode.OK, SuccessResponseJson(), (req, body) =>
            {
                capturedRequest = req;
                capturedBody = body;
            });
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient("UmbracoAutomate")).Returns(new HttpClient(handler));

            var result = await ActionTestHarness.For<CreateTaskAction>()
                .WithSettings(settings)
                .WithConnection(ClickUpConnectionType.ConnectionAlias, ValidConnectionSettings())
                .WithService(factory.Object)
                .WithService(MockCredentialsService().Object)
                .ExecuteAsync();

            Assert.Equal(ActionResultStatus.Success, result.Status);
            Assert.NotNull(capturedBody);
            using var doc = JsonDocument.Parse(capturedBody!);
            Assert.True(doc.RootElement.GetProperty("due_date_time").GetBoolean());
        }

        [Fact]
        public async Task WhenDueDateIsMidnight_SendsDueDateTimeFalse()
        {
            var settings = ValidSettings();
            settings.DueDate = "2026-08-01";
            string? capturedBody = null;

            var handler = new CapturingHandler(HttpStatusCode.OK, SuccessResponseJson(), (_, body) => capturedBody = body);
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient("UmbracoAutomate")).Returns(new HttpClient(handler));

            var result = await ActionTestHarness.For<CreateTaskAction>()
                .WithSettings(settings)
                .WithConnection(ClickUpConnectionType.ConnectionAlias, ValidConnectionSettings())
                .WithService(factory.Object)
                .WithService(MockCredentialsService().Object)
                .ExecuteAsync();

            Assert.Equal(ActionResultStatus.Success, result.Status);
            Assert.NotNull(capturedBody);
            using var doc = JsonDocument.Parse(capturedBody!);
            Assert.False(doc.RootElement.GetProperty("due_date_time").GetBoolean());
        }

        [Fact]
        public async Task WhenConnectionMissing_ReturnsAuthenticationFailure()
        {
            var result = await BuildHarness(withConnection: false).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Authentication, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenAccessTokenUnavailable_ReturnsAuthenticationFailure()
        {
            var result = await BuildHarness(accessToken: null).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Authentication, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenClickUpReturnsSuccess_MapsResponseToOutput()
        {
            var result = await BuildHarness().ExecuteAsync();

            Assert.Equal(ActionResultStatus.Success, result.Status);
            Assert.NotNull(result.OutputData);

            var output = Assert.IsType<CreateTaskOutput>(result.OutputData);
            Assert.Equal("9hx", output.TaskId);
            Assert.Equal("New Task Name", output.Name);
            Assert.Equal("https://app.clickup.com/t/9hx", output.Url);
            Assert.Equal("to do", output.StatusName);
            Assert.Equal("open", output.StatusType);
            Assert.Equal("normal", output.Priority);
            Assert.Null(output.CustomId);
            Assert.Null(output.ParentTaskId);
            Assert.Null(output.TopLevelParentTaskId);
            Assert.Equal(183, output.CreatorId);
            Assert.Equal("Alex Johnson", output.CreatorUsername);
            Assert.Equal("Sprint Backlog", output.ListName);
            Assert.Equal("7002367", output.SpaceId);
            Assert.Equal("6992470", output.FolderId);
            Assert.Equal("6992470", output.ProjectId);
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1567780450202), output.DateCreated);
        }

        [Fact]
        public async Task WhenClickUpReturnsUnauthorized_ReturnsAuthenticationFailure()
        {
            var result = await BuildHarness(
                statusCode: HttpStatusCode.Unauthorized,
                responseBody: """{ "err": "Token invalid" }""").ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Authentication, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenClickUpReturnsRateLimited_ReturnsRateLimitingFailure()
        {
            var result = await BuildHarness(
                statusCode: (HttpStatusCode)429,
                responseBody: """{ "err": "Rate limit reached" }""").ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.RateLimiting, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenClickUpReturnsBadRequest_ReturnsValidationFailure()
        {
            var result = await BuildHarness(
                statusCode: HttpStatusCode.BadRequest,
                responseBody: """{ "err": "List not found" }""").ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.Validation, result.ErrorCategory);
        }

        [Fact]
        public async Task WhenClickUpReturnsServerError_ReturnsServiceUnavailableFailure()
        {
            var result = await BuildHarness(
                statusCode: HttpStatusCode.InternalServerError,
                responseBody: null).ExecuteAsync();

            Assert.Equal(ActionResultStatus.Failed, result.Status);
            Assert.Equal(StepRunErrorCategory.ServiceUnavailable, result.ErrorCategory);
        }
    }

    private sealed class CapturingHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string? _responseBody;
        private readonly Action<HttpRequestMessage, string?> _onSend;

        public CapturingHandler(HttpStatusCode statusCode, string? responseBody, Action<HttpRequestMessage, string?> onSend)
        {
            _statusCode = statusCode;
            _responseBody = responseBody;
            _onSend = onSend;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
            _onSend(request, body);

            var response = new HttpResponseMessage(_statusCode);
            if (_responseBody is not null)
            {
                response.Content = new StringContent(_responseBody, Encoding.UTF8, "application/json");
            }

            return response;
        }
    }
}
