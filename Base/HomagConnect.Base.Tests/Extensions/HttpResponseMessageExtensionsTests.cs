using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;

using HomagConnect.Base.Client;
using HomagConnect.Base.Contracts.Exceptions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shouldly;

namespace HomagConnect.Base.Tests.Extensions;

[TestClass]
public class HttpResponseMessageExtensionsTests
{
    [TestMethod]
    [DataRow("\"type\":\"https://example.test/problems/custom\",", "application/problem+json")]
    [DataRow("", "application/problem+json")]
    [DataRow("\"type\":\"CustomFailure\",", "application/json")]
    [DataRow("", "application/json")]
    public async Task UnknownOrMissingType_RetainsStructuredProblem(string type, string mediaType)
    {
        var body = "{" + type + "\"title\":\"Invalid input\",\"status\":422,\"detail\":\"Fix input\"," +
                   "\"errors\":{\"Field\":[\"Required\",\"Invalid\"]},\"traceId\":\"trace-123\"}";
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://example.test/items");
        using var response = CreateResponse(HttpStatusCode.BadRequest, body, mediaType);

        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => response.EnsureSuccessStatusCodeWithDetailsAsync(request, null, null));

        exception.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        exception.Message.ShouldContain(body);
        exception.Message.ShouldContain("https://example.test/items");
        var problem = exception.InnerException.ShouldBeOfType<ProblemDetailsException>();
        problem.Type.ShouldBe(type.Length == 0 ? null : mediaType == "application/json"
            ? "CustomFailure" : "https://example.test/problems/custom");
        problem.Status.ShouldBe(422);
        problem.Title.ShouldBe("Invalid input");
        problem.Detail.ShouldBe("Fix input");
        problem.TraceId.ShouldBe("trace-123");
        problem.Errors["Field"].ShouldBe(["Required", "Invalid"]);
    }

    [TestMethod]
    [DataRow("ProblemDetailsException", typeof(ProblemDetailsException))]
    [DataRow("ArgumentOutOfRangeException", typeof(ArgumentOutOfRangeException))]
    [DataRow("NotSupportedException", typeof(NotSupportedException))]
    [DataRow("NotImplementedException", typeof(NotImplementedException))]
    [DataRow("AuthenticationException", typeof(AuthenticationException))]
    [DataRow("ValidationException", typeof(ValidationException))]
    public async Task RecognizedType_PreservesTopLevelException(string type, Type expectedType)
    {
        var body = "{\"type\":\"" + type + "\",\"detail\":\"Failure\",\"status\":400}";
        using var response = CreateResponse(HttpStatusCode.BadRequest, body, "application/json");

        var exception = await Should.ThrowAsync<Exception>(
            () => response.EnsureSuccessStatusCodeWithDetailsAsync(null, null, null));

        exception.GetType().ShouldBe(expectedType);
        var problem = type == nameof(ProblemDetailsException)
            ? (ProblemDetailsException)exception
            : exception.InnerException.ShouldBeOfType<ProblemDetailsException>();
        problem.Type.ShouldBe(type);
        problem.Detail.ShouldBe("Failure");
        problem.Status.ShouldBe(400);
    }

    [TestMethod]
    public async Task LegacyResponseWithoutMediaType_IsRecognized()
    {
        using var response = CreateResponse(HttpStatusCode.BadRequest,
            "{\"title\":\"Failure\",\"status\":400,\"detail\":\"Details\"}", "application/json");
        response.Content.Headers.ContentType = null;

        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => response.EnsureSuccessStatusCodeWithDetailsAsync(null, null, null));
        exception.InnerException.ShouldBeOfType<ProblemDetailsException>().Detail.ShouldBe("Details");
    }

    [TestMethod]
    public async Task LegacyRecognizedTypeWithoutOtherFields_IsStillSpecialized()
    {
        using var response = CreateResponse(HttpStatusCode.BadRequest,
            "{\"type\":\"NotSupportedException\"}", "application/json");

        var exception = await Should.ThrowAsync<NotSupportedException>(
            () => response.EnsureSuccessStatusCodeWithDetailsAsync(null, null, null));
        exception.InnerException.ShouldBeOfType<ProblemDetailsException>();
    }

    [TestMethod]
    [DataRow("{}", "application/problem+json")]
    [DataRow("{}", "application/json")]
    [DataRow("{\"name\":\"Widget\",\"id\":42}", "application/json")]
    [DataRow("{\"type\":\"Widget\"}", "application/json")]
    [DataRow("<html>error</html>", "text/html")]
    [DataRow("", "application/problem+json")]
    [DataRow("{\"type\":", "application/problem+json")]
    [DataRow("{\"title\":\"Failure\",\"status\":\"not a number\"}", "application/problem+json")]
    [DataRow("[]", "application/problem+json")]
    public async Task NonProblemBodies_RemainGeneric(string body, string mediaType)
    {
        using var response = CreateResponse(HttpStatusCode.InternalServerError, body, mediaType);

        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => response.EnsureSuccessStatusCodeWithDetailsAsync(null, null, null));

        exception.InnerException.ShouldBeNull();
        exception.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        exception.Message.ShouldContain(body);
    }

    [TestMethod]
    public async Task FailureCallback_ReceivesStructuredExceptionAndCanSuppressIt()
    {
        const string body = "{\"title\":\"Failure\",\"status\":400,\"detail\":\"Details\"}";
        using var response = CreateResponse(HttpStatusCode.BadRequest, body, "application/problem+json");
        var logger = new RecordingLogger();
        var called = false;

        await response.EnsureSuccessStatusCodeWithDetailsAsync(null, logger, (status, raw, exception) =>
        {
            called = true;
            status.ShouldBe(HttpStatusCode.BadRequest);
            raw.ShouldBe(body);
            var httpException = exception.ShouldBeOfType<HttpRequestException>();
            httpException.InnerException.ShouldBeOfType<ProblemDetailsException>().Detail.ShouldBe("Details");
            return Task.FromResult(true);
        });

        called.ShouldBeTrue();
        logger.Exception.ShouldBeNull();
    }

    [TestMethod]
    public async Task FailureCallback_CanDeclineToSuppressException()
    {
        using var response = CreateResponse(HttpStatusCode.BadRequest, "{}", "application/json");
        var logger = new RecordingLogger();
        Exception? observed = null;
        var exception = await Should.ThrowAsync<HttpRequestException>(() =>
            response.EnsureSuccessStatusCodeWithDetailsAsync(null, logger, (_, _, error) =>
            {
                observed = error;
                return Task.FromResult(false);
            }));

        exception.ShouldBeSameAs(observed);
        logger.Exception.ShouldBeSameAs(exception);
        logger.Level.ShouldBe(LogLevel.Error);
    }

    [TestMethod]
    public async Task Success_DoesNotInvokeCallback()
    {
        using var response = CreateResponse(HttpStatusCode.OK, "{}", "application/json");
        var called = false;
        await response.EnsureSuccessStatusCodeWithDetailsAsync(null, null, (_, _, _) =>
        {
            called = true;
            return Task.FromResult(false);
        });
        called.ShouldBeFalse();
    }

    private static HttpResponseMessage CreateResponse(HttpStatusCode status, string body, string mediaType) =>
        new(status) { Content = new StringContent(body, System.Text.Encoding.UTF8, mediaType) };

    private sealed class RecordingLogger : ILogger
    {
        public Exception? Exception { get; private set; }
        public LogLevel Level { get; private set; }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            Level = logLevel;
            Exception = exception;
        }
    }
}
