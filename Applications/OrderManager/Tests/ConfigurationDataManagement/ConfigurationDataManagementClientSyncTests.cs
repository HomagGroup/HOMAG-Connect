using System.Net;
using System.Text;

using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.OrderManager.Client;
using HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomagConnect.OrderManager.Tests.ConfigurationDataManagement
{
    [TestClass]
    [UnitTest("OrderManager.ConfigurationDataManagement")]
    public sealed class ConfigurationDataManagementClientSyncTests
    {
        public TestContext TestContext { get; set; } = null!;

        [TestMethod]
        public async Task StartCustomTableSync_SendsPostWithEscapedValues_ReturnsResult()
        {
            // Arrange
            var handler = new StubHandler(HttpStatusCode.OK, "{\"State\":\"Queued\",\"CorrelationId\":\"c1\"}");
            var client = CreateClient(handler);

            // Act
            var result = await client.StartCustomTableSync("my lib", "table/1");

            // Assert
            Assert.IsNotNull(handler.Request);
            Assert.AreEqual(HttpMethod.Post, handler.Request.Method);
            Assert.AreEqual("https://connect.example.com/api/orderManager/configurationDataManagement/libraries/my%20lib/customTables/table%2F1/sync/start", handler.Request.RequestUri!.AbsoluteUri);
            Assert.AreEqual(CustomTableSyncState.Queued, result.State);
            Assert.AreEqual("c1", result.CorrelationId);
        }

        [TestMethod]
        public async Task StartCustomTableSync_StateError_DoesNotThrow()
        {
            // Arrange
            var handler = new StubHandler(HttpStatusCode.OK, "{\"State\":\"Error\",\"ErrorTexts\":[\"Not linked\"]}");
            var client = CreateClient(handler);

            // Act
            var result = await client.StartCustomTableSync("lib", "table");

            // Assert
            Assert.AreEqual(CustomTableSyncState.Error, result.State);
            Assert.AreEqual(string.Empty, result.CorrelationId);
            Assert.HasCount(1, result.ErrorTexts);
            Assert.AreEqual("Not linked", result.ErrorTexts[0]);
        }

        [TestMethod]
        public async Task StartCustomTableSync_ProblemDetails_Throws()
        {
            // Arrange
            var handler = new StubHandler(HttpStatusCode.NotFound, "{\"type\":\"ProblemDetailsException\",\"title\":\"Library not found\",\"status\":404}", "application/problem+json");
            var client = CreateClient(handler);

            // Act
            var exception = await Assert.ThrowsExactlyAsync<ProblemDetailsException>(() => client.StartCustomTableSync("unknown", "table"));

            // Assert
            Assert.AreEqual("Library not found", exception.Title);
        }

        [TestMethod]
        public async Task GetCustomTableSyncState_SendsGetWithEscapedValue_ReturnsStateData()
        {
            // Arrange
            var handler = new StubHandler(HttpStatusCode.OK, "{\"State\":\"Succeeded\",\"CorrelationId\":\"c/1\",\"LibraryId\":\"lib\",\"CustomTableId\":\"ct\",\"DataSource\":\"Edgebands\",\"Progress\":100,\"ProgressInfo\":\"Done\",\"ErrorTexts\":[],\"WarningTexts\":[\"w\"]}");
            var client = CreateClient(handler);

            // Act
            var result = await client.GetCustomTableSyncState("c/1");

            // Assert
            Assert.IsNotNull(handler.Request);
            Assert.AreEqual(HttpMethod.Get, handler.Request.Method);
            Assert.AreEqual("https://connect.example.com/api/orderManager/configurationDataManagement/sync/customTable/state?correlationId=c%2F1", handler.Request.RequestUri!.AbsoluteUri);
            Assert.AreEqual(CustomTableSyncState.Succeeded, result.State);
            Assert.AreEqual(CustomTableSyncDataSource.Edgebands, result.DataSource);
            Assert.AreEqual(100d, result.Progress);
            Assert.HasCount(1, result.WarningTexts);
        }

        [TestMethod]
        public async Task GetCustomTableSyncState_NotFound_Throws()
        {
            // Arrange
            var handler = new StubHandler(HttpStatusCode.NotFound, "{\"type\":\"ProblemDetailsException\",\"title\":\"Not found\",\"status\":404}", "application/problem+json");
            var client = CreateClient(handler);

            // Act
            var exception = await Assert.ThrowsExactlyAsync<ProblemDetailsException>(() => client.GetCustomTableSyncState("unknown"));

            // Assert
            Assert.AreEqual("Not found", exception.Title);
        }

        [TestMethod]
        public void CustomTableSyncState_ContainsExpectedValues()
        {
            // Arrange
            string[] expected = ["Unknown", "Queued", "Started", "Succeeded", "Error", "Aborted"];

            // Act
            var names = Enum.GetNames<CustomTableSyncState>();

            // Assert
            CollectionAssert.AreEqual(expected, names);
        }

        [TestMethod]
        public void CustomTableSyncDataSource_ContainsExpectedValues()
        {
            // Arrange
            string[] expected = ["Default", "Textures", "Boards", "Edgebands", "Hardware"];

            // Act
            var names = Enum.GetNames<CustomTableSyncDataSource>();

            // Assert
            CollectionAssert.AreEqual(expected, names);
        }

        [TestMethod]
        public void Deserialize_CustomTableSyncStateData_ReadsEnumByName()
        {
            // Arrange
            const string json = "{\"State\":\"Error\",\"CorrelationId\":\"\",\"ErrorTexts\":[\"Not linked\"]}";

            // Act
            var result = JsonConvert.DeserializeObject<CustomTableSyncStateData>(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(CustomTableSyncState.Error, result.State);
            Assert.AreEqual(string.Empty, result.CorrelationId);
            Assert.HasCount(1, result.ErrorTexts);
            Assert.AreEqual("Not linked", result.ErrorTexts[0]);
        }

        [TestMethod]
        public void Deserialize_CustomTableSyncStateData_UnknownState_MapsToUnknown()
        {
            // Arrange
            const string json = "{\"State\":\"SomethingNew\"}";

            // Act
            var result = JsonConvert.DeserializeObject<CustomTableSyncStateData>(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(CustomTableSyncState.Unknown, result.State);
        }

        [TestMethod]
        public void Serialize_CustomTableSyncStateData_ContainsOnlyPublicFields()
        {
            // Arrange
            var data = new CustomTableSyncStateData
            {
                Created = new DateTimeOffset(2026, 11, 5, 10, 0, 0, TimeSpan.Zero),
                State = CustomTableSyncState.Started,
                CorrelationId = "c1",
                LibraryId = "lib",
                CustomTableId = "ct",
                DataSource = CustomTableSyncDataSource.Boards,
                Progress = 50,
                ProgressInfo = "Half way",
                ErrorTexts = ["e"],
                WarningTexts = ["w"]
            };
            string[] expected = ["Created", "State", "CorrelationId", "LibraryId", "CustomTableId", "DataSource", "Progress", "ProgressInfo", "ErrorTexts", "WarningTexts"];

            // Act
            var json = JObject.Parse(JsonConvert.SerializeObject(data));
            var names = json.Properties().Select(p => p.Name).ToArray();

            // Assert
            CollectionAssert.AreEqual(expected, names);
            Assert.AreEqual("Started", (string?)json["State"]);
            Assert.AreEqual("Boards", (string?)json["DataSource"]);
        }

        [TestMethod]
        public void Deserialize_CustomTableSyncStateData_WithoutOptionalValues_UsesDefaults()
        {
            // Arrange
            const string json = "{\"State\":\"Queued\",\"CorrelationId\":\"c1\"}";

            // Act
            var result = JsonConvert.DeserializeObject<CustomTableSyncStateData>(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(CustomTableSyncState.Queued, result.State);
            Assert.IsNull(result.LibraryId);
            Assert.IsNull(result.CustomTableId);
            Assert.AreEqual(CustomTableSyncDataSource.Default, result.DataSource);
            Assert.IsEmpty(result.ErrorTexts);
            Assert.IsEmpty(result.WarningTexts);
        }

        private static ConfigurationDataManagementClient CreateClient(HttpMessageHandler handler)
        {
            return new ConfigurationDataManagementClient(new HttpClient(handler) { BaseAddress = new Uri("https://connect.example.com") });
        }

        private sealed class StubHandler(HttpStatusCode statusCode, string content, string mediaType = "application/json") : HttpMessageHandler
        {
            public HttpRequestMessage? Request { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Request = request;

                return Task.FromResult(new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(content, Encoding.UTF8, mediaType)
                });
            }
        }
    }
}
