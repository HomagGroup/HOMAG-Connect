using HomagConnect.Base.Contracts;
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
    public void Events_EdgebandEntityDeletedEvent_SerializeDeserialize()
    {
        var EdgebandEntityDeletedEvent = new EdgebandEntityDeletedEvent();

        EdgebandEntityDeletedEvent.SubscriptionId = Guid.NewGuid();
        EdgebandEntityDeletedEvent.Id = "abc";

        EdgebandEntityDeletedEvent.Trace();

        Assert.IsTrue(EdgebandEntityDeletedEvent.IsValid, "because EdgebandEntityDeletedEvent should be valid after setting required properties");

        TestContext?.AddResultFile(EdgebandEntityDeletedEvent.TraceToFile("EdgebandEntityDeletedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_EdgebandEntityDeletedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        Guid expected = Guid.NewGuid();
        // Arrange
        var evt = new EdgebandEntityDeletedEvent()
        {
            SubscriptionId = Guid.NewGuid(),
            Id = expected.ToString()
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<EdgebandEntityDeletedEvent>(json, SerializerSettings.Default);

        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        Assert.IsNotNull(deserializedTyped);
        Assert.AreEqual(expected.ToString(), deserializedTyped.Id);

        Assert.AreEqual(expected, deserializedBase.Id);
    }

    /// <summary />
    [TestMethod]
    public void Events_EdgebandTypeDeletedEvent_SerializeDeserialize()
    {
        var edgebandTypeDeletedEvent = new EdgebandTypeDeletedEvent();

        edgebandTypeDeletedEvent.SubscriptionId = Guid.NewGuid();
        edgebandTypeDeletedEvent.EdgebandCode = "EDGE-DEL-123";

        edgebandTypeDeletedEvent.Trace();

        Assert.IsTrue(edgebandTypeDeletedEvent.IsValid);

        TestContext?.AddResultFile(edgebandTypeDeletedEvent.TraceToFile("edgebandTypeDeletedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_EdgebandTypeDeletedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        // Arrange
        var edgebandCode = "EDGE-DEL-123";
        var evt = new EdgebandTypeDeletedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            EdgebandCode = edgebandCode
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<EdgebandTypeDeletedEvent>(json, SerializerSettings.Default);

        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        Assert.IsNotNull(deserializedTyped);
        Assert.AreEqual(edgebandCode, deserializedTyped.EdgebandCode);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("edgebandCode"));
        Assert.AreEqual(edgebandCode, deserializedBase.CustomProperties["edgebandCode"].ToString());
    }

    /// <summary />
    [TestMethod]
    public void Events_EdgebandTypeUpsertedEvent_SerializeDeserialize()
    {
        var edgebandTypeUpsertedEvent = new EdgebandTypeUpsertedEvent();

        edgebandTypeUpsertedEvent.SubscriptionId = Guid.NewGuid();
        edgebandTypeUpsertedEvent.EdgebandType = new EdgebandType();

        edgebandTypeUpsertedEvent.Trace();

        Assert.IsTrue(edgebandTypeUpsertedEvent.IsValid);

        TestContext?.AddResultFile(edgebandTypeUpsertedEvent.TraceToFile("edgebandTypeUpsertedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_EdgebandTypeUpsertedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        // Arrange
        var edgebandType = new EdgebandType
        {
            EdgebandCode = "EDGE-123",
            Thickness = 1.0,
            // Add other properties as needed
        };

        var evt = new EdgebandTypeUpsertedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            EdgebandType = edgebandType
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<EdgebandTypeUpsertedEvent>(json, SerializerSettings.Default);

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
        var assemblies = new[] { typeof(EdgebandTypeUpsertedEvent).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        Assert.IsTrue(derivedTypes.Any(t => t == typeof(EdgebandEntityDeletedEvent)), "EdgebandEntityDeletedEvent should be present in derived event types.");
        Assert.IsTrue(derivedTypes.Any(t => t == typeof(EdgebandTypeUpsertedEvent)), "EdgebandTypeUpsertedEvent should be present in derived event types.");
        Assert.IsTrue(derivedTypes.Any(t => t == typeof(EdgebandTypeDeletedEvent)), "EdgebandTypeDeletedEvent should be present in derived event types.");

        derivedTypes.Trace();
    }
}