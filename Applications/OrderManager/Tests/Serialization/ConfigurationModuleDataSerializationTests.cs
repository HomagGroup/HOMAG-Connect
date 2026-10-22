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
                ModuleShortName = "Tall cabinet",
                ModuleDescription = "Tall cabinet with drop container",
                ModuleImage = "https://images/shape01.png"
            };

            // Act
            var json = JObject.Parse(JsonConvert.SerializeObject(position));
            var names = json.Properties().Select(p => p.Name).ToList();

            // Assert
            Assert.AreEqual("Tall cabinet", (string?)json["ModuleShortName"]);
            Assert.AreEqual("Tall cabinet with drop container", (string?)json["ModuleDescription"]);
            Assert.AreEqual("https://images/shape01.png", (string?)json["ModuleImage"]);
            Assert.IsNull(json["SubArticle"]);
            Assert.IsTrue(names.IndexOf("ModuleId") < names.IndexOf("ModuleShortName"));
            Assert.IsTrue(names.IndexOf("ModuleShortName") < names.IndexOf("ModuleDescription"));
            Assert.IsTrue(names.IndexOf("ModuleDescription") < names.IndexOf("ModuleImage"));
        }

        [TestMethod]
        public void Serialize_ConfigurationWithModuleData_RoundTripsValues()
        {
            // Arrange
            var configuration = new Configuration
            {
                ModuleId = "singleDrawer",
                ModuleShortName = "Drawer",
                ModuleDescription = "Single drawer",
                ModuleImage = "https://images/singleDrawer.png",
                SubArticle = new SubArticle { Id = "subArticle_drawer", Name = "Drawer", Description = "Drawer article", Image = "https://images/drawer.png" }
            };

            // Act
            var result = JsonConvert.DeserializeObject<Configuration>(JsonConvert.SerializeObject(configuration));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Drawer", result.ModuleShortName);
            Assert.AreEqual("Single drawer", result.ModuleDescription);
            Assert.AreEqual("https://images/singleDrawer.png", result.ModuleImage);
            Assert.AreEqual("subArticle_drawer", result.SubArticle?.Id);
            Assert.AreEqual("Drawer", result.SubArticle?.Name);
            Assert.AreEqual("Drawer article", result.SubArticle?.Description);
            Assert.AreEqual("https://images/drawer.png", result.SubArticle?.Image);
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
            Assert.IsNull(result.ModuleShortName);
            Assert.IsNull(result.ModuleDescription);
            Assert.IsNull(result.ModuleImage);
        }

        [TestMethod]
        public void Serialize_ConfigurationWithSubArticle_ContainsSubArticleAfterModuleImage()
        {
            // Arrange
            var configuration = new Configuration
            {
                ModuleId = "singleDrawer",
                ModuleShortName = "Drawer",
                ModuleDescription = "Single drawer",
                ModuleImage = "https://images/singleDrawer.png",
                SubArticle = new SubArticle { Id = "subArticle_drawer" }
            };

            // Act
            var json = JObject.Parse(JsonConvert.SerializeObject(configuration));
            var names = json.Properties().Select(p => p.Name).ToList();

            // Assert
            Assert.IsTrue(names.IndexOf("ModuleId") < names.IndexOf("ModuleShortName"));
            Assert.IsTrue(names.IndexOf("ModuleShortName") < names.IndexOf("ModuleDescription"));
            Assert.IsTrue(names.IndexOf("ModuleDescription") < names.IndexOf("ModuleImage"));
            Assert.IsTrue(names.IndexOf("ModuleImage") < names.IndexOf("SubArticle"));
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
            Assert.IsNull(result.ModuleShortName);
            Assert.IsNull(result.ModuleDescription);
            Assert.IsNull(result.ModuleImage);
            Assert.IsNull(result.SubArticle);
        }
    }
}
