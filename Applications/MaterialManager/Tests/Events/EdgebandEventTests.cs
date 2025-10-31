using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Events.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Tests.Events;

/// <inheritdoc />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Material.Events")]
public class EdgebandEventTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_EdgebandTypeCreatedEvent_SerializeDeserialize()
    {
        var edgebandTypeCreatedEvent = new EdgebandTypeCreatedEvent();

        edgebandTypeCreatedEvent.SubscriptionId = Guid.NewGuid();
        edgebandTypeCreatedEvent.EdgebandType = new EdgebandType();

        edgebandTypeCreatedEvent.Trace();

        Assert.IsTrue(edgebandTypeCreatedEvent.IsValid);

        TestContext?.AddResultFile(edgebandTypeCreatedEvent.TraceToFile("edgebandTypeCreatedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_EdgebandTypeCreatedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        // Arrange
        var edgebandType = new EdgebandType
        {
            EdgebandCode = "EDGE-123",
            Thickness = 1.0,
            // Add other properties as needed
        };

        var evt = new EdgebandTypeCreatedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            EdgebandType = edgebandType
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<EdgebandTypeCreatedEvent>(json, SerializerSettings.Default);

        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        Assert.IsNotNull(deserializedTyped);
        Assert.IsNotNull(deserializedTyped.EdgebandType);
        Assert.AreEqual(edgebandType.EdgebandCode, deserializedTyped.EdgebandType.EdgebandCode);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("edgebandType"));

        // deserialized back to EdgebandType
        var edgebandTypeJson = deserializedBase.CustomProperties["edgebandType"].ToString();
        if (edgebandTypeJson != null)
        {
            var edgebandTypeFromCustom = JsonConvert.DeserializeObject<EdgebandType>(edgebandTypeJson, SerializerSettings.Default);
            Assert.IsNotNull(edgebandTypeFromCustom);
            Assert.AreEqual(edgebandType.EdgebandCode, edgebandTypeFromCustom.EdgebandCode);
        }
    }

    /// <summary />
    [TestMethod]
    public void Events_ListMaterialEdgebandEvents()
    {
        var assemblies = new[] { typeof(EdgebandTypeCreatedEvent).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        Assert.IsTrue(derivedTypes.Any(t => t == typeof(EdgebandTypeCreatedEvent)), "EdgebandTypeCreatedEvent should be present in derived event types.");

        derivedTypes.Trace();
    }
}