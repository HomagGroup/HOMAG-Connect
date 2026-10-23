using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts.OrderItems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomagConnect.OrderManager.Tests.Serialization
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Contracts")]
    public sealed class ConfigurationModuleDataSerializationTests
    {
        public TestContext TestContext { get; set; } = null!;

        [TestMethod]
        public void Serialize_ConfigurationPositionWithModuleData_ContainsModuleDataAfterModuleId()
        {
            // Arrange
            var position = new ConfigurationPosition
            {
                ModuleId = "shape01",
                ModuleName = "Tall cabinet",
                ModuleDescription = "Tall cabinet with drop container",
                SubArticle = new SubArticle
                {
                    Id = "subArticle_tallCabinet",
                    Name = "Tall cabinet",
                    Description = "Tall cabinet article",
                    AdditionalData = [
                        new AdditionalDataEntity
                        {
                            Type = AdditionalDataType.Image,
                            Category = "ArticleImage",
                            DownloadUri = new Uri("https://images/subArticle_tallCabinet.png")
                        }
                    ]
                },
                AdditionalData = [
                    new AdditionalDataEntity
                    {
                        Type = AdditionalDataType.Image,
                        Category = "ModuleImage",
                        DownloadUri = new Uri("https://images/shape01.png")
                    }
                ]
            };

            // Act
            var json = JObject.Parse(JsonConvert.SerializeObject(position));
            var names = json.Properties().Select(p => p.Name).ToList();
            var moduleImage = json["AdditionalData"]?.FirstOrDefault(t => (string?)t["Category"] == "ModuleImage");
            var subArticle = json["SubArticle"];
            var articleImage = subArticle?["AdditionalData"]?.FirstOrDefault(t => (string?)t["Category"] == "ArticleImage");

            // Assert
            Assert.AreEqual("Tall cabinet", (string?)json["ModuleName"]);
            Assert.AreEqual("Tall cabinet with drop container", (string?)json["ModuleDescription"]);
            Assert.IsNotNull(subArticle);
            Assert.AreEqual(JTokenType.Object, subArticle.Type);
            Assert.AreEqual("subArticle_tallCabinet", (string?)subArticle["Id"]);
            Assert.AreEqual("Tall cabinet", (string?)subArticle["Name"]);
            Assert.AreEqual("Tall cabinet article", (string?)subArticle["Description"]);
            Assert.IsNotNull(articleImage);
            Assert.AreEqual("https://images/subArticle_tallCabinet.png", (string?)articleImage["DownloadUri"]);
            Assert.IsNotNull(moduleImage);
            Assert.AreEqual("https://images/shape01.png", (string?)moduleImage["DownloadUri"]);
            Assert.IsTrue(names.IndexOf("ModuleId") < names.IndexOf("ModuleName"));
            Assert.IsTrue(names.IndexOf("ModuleName") < names.IndexOf("ModuleDescription"));
            Assert.IsTrue(names.IndexOf("ModuleDescription") < names.IndexOf("SubArticle"));
        }

        [TestMethod]
        public void Serialize_ConfigurationWithModuleData_RoundTripsValues()
        {
            // Arrange
            var configuration = new Configuration
            {
                ModuleId = "singleDrawer",
                ModuleName = "Drawer",
                ModuleDescription = "Single drawer",
                SubArticle = new SubArticle { Id = "subArticle_drawer", Name = "Drawer", Description = "Drawer article" }
            };

            // Act
            var result = JsonConvert.DeserializeObject<Configuration>(JsonConvert.SerializeObject(configuration));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Drawer", result.ModuleName);
            Assert.AreEqual("Single drawer", result.ModuleDescription);
            Assert.AreEqual("subArticle_drawer", result.SubArticle?.Id);
            Assert.AreEqual("Drawer", result.SubArticle?.Name);
            Assert.AreEqual("Drawer article", result.SubArticle?.Description);
        }

        [TestMethod]
        public void Deserialize_LegacyConfigurationPositionWithoutModuleData_Succeeds()
        {
            // Arrange
            const string legacyJson = "{\"Id\":\"a1\",\"ModuleId\":\"shape01\",\"Description\":\"Position text\",\"Attributes\":[]}";

            // Act
            var result = JsonConvert.DeserializeObject<ConfigurationPosition>(legacyJson);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("shape01", result.ModuleId);
            Assert.AreEqual("Position text", result.Description);
            Assert.IsNull(result.ModuleName);
            Assert.IsNull(result.ModuleDescription);
        }

        [TestMethod]
        public void Serialize_ConfigurationWithSubArticle_ContainsSubArticleAfterModuleDescription()
        {
            // Arrange
            var configuration = new Configuration
            {
                ModuleId = "singleDrawer",
                ModuleName = "Drawer",
                ModuleDescription = "Single drawer",
                SubArticle = new SubArticle { Id = "subArticle_drawer" }
            };

            // Act
            var json = JObject.Parse(JsonConvert.SerializeObject(configuration));
            var names = json.Properties().Select(p => p.Name).ToList();

            // Assert
            Assert.IsTrue(names.IndexOf("ModuleId") < names.IndexOf("ModuleName"));
            Assert.IsTrue(names.IndexOf("ModuleName") < names.IndexOf("ModuleDescription"));
            Assert.IsTrue(names.IndexOf("ModuleDescription") < names.IndexOf("SubArticle"));
            Assert.IsTrue(names.IndexOf("SubArticle") < names.IndexOf("Attributes"));
        }

        [TestMethod]
        public void Deserialize_LegacyConfigurationWithoutModuleData_Succeeds()
        {
            // Arrange
            const string legacyJson = "{\"Id\":\"m7\",\"ModuleId\":\"singleDrawer\",\"Attributes\":[]}";

            // Act
            var result = JsonConvert.DeserializeObject<Configuration>(legacyJson);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("singleDrawer", result.ModuleId);
            Assert.IsNull(result.ModuleName);
            Assert.IsNull(result.ModuleDescription);
            Assert.IsNull(result.SubArticle);
        }
    }
}
