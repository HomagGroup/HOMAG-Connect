using HomagConnect.Base;
using HomagConnect.Base;
using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Events.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Tests;

using Newtonsoft.Json;

/// <inheritdoc />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Material.Events")]
public class BoardEventTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_BoardEntityCreatedEvent_SerializeDeserialize()
    {
        var boardEntityCreatedEvent = new BoardEntityCreatedEvent();

        boardEntityCreatedEvent.SubscriptionId = Guid.NewGuid();
        boardEntityCreatedEvent.BoardEntity = new BoardEntity();

        boardEntityCreatedEvent.Trace();

        Assert.IsTrue(boardEntityCreatedEvent.IsValid);

        TestContext?.AddResultFile(boardEntityCreatedEvent.TraceToFile("boardEntityCreatedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_BoardEntityCreatedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        // Arrange
        var boardEntity = new BoardEntity
        {
            Id = "B123",
            Length = 1000,
            Width = 500,
            Quantity = 1,
            Comments = "Test board"
        };

        var evt = new BoardEntityCreatedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            BoardEntity = boardEntity
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<BoardEntityCreatedEvent>(json, SerializerSettings.Default);

        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        Assert.IsNotNull(deserializedTyped);
        Assert.IsNotNull(deserializedTyped.BoardEntity);
        Assert.AreEqual(boardEntity.Id, deserializedTyped.BoardEntity.Id);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("boardEntity"));

        // deserialized back to BoardEntity
        var boardEntityJson = deserializedBase.CustomProperties["boardEntity"].ToString();
        var boardEntityFromCustom = JsonConvert.DeserializeObject<BoardEntity>(boardEntityJson, SerializerSettings.Default);
        Assert.IsNotNull(boardEntityFromCustom);
        Assert.AreEqual(boardEntity.Id, boardEntityFromCustom.Id);
    }

    /// <summary />
    [TestMethod]
    public void Events_ListMaterialEvents()
    {
        var assemblies = new[] { typeof(BoardEntityCreatedEvent).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        Assert.IsTrue(derivedTypes.Any(t => t == typeof(BoardEntityCreatedEvent)), "BoardEntityCreatedEvent should be present in derived event types.");
        Assert.IsTrue(derivedTypes.Any(t => t == typeof(BoardTypeCreatedEvent)), "BoardTypeCreatedEvent should be present in derived event types.");

        derivedTypes.Trace();
    }

    /// <summary />
    [TestMethod]
    public void Events_BoardTypeCreatedEvent_SerializeDeserialize()
    {
        var boardTypeCreatedEvent = new BoardTypeCreatedEvent();

        boardTypeCreatedEvent.SubscriptionId = Guid.NewGuid();
        boardTypeCreatedEvent.BoardType = new BoardType();

        boardTypeCreatedEvent.Trace();

        Assert.IsTrue(boardTypeCreatedEvent.IsValid);

        TestContext?.AddResultFile(boardTypeCreatedEvent.TraceToFile("boardTypeCreatedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_BoardTypeCreatedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
    {
        // Arrange
        var boardType = new BoardType
        {
            BoardCode = "CODE-123",
            Length = 2800,
            Width = 2070,
            Thickness = 19,
            MaterialCode = "MAT-001"
        };

        var evt = new BoardTypeCreatedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            BoardType = boardType
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<BoardTypeCreatedEvent>(json, SerializerSettings.Default);

        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        Assert.IsNotNull(deserializedTyped);
        Assert.IsNotNull(deserializedTyped.BoardType);
        Assert.AreEqual(boardType.BoardCode, deserializedTyped.BoardType.BoardCode);

        Assert.IsNotNull(deserializedBase);
        Assert.IsNotNull(deserializedBase.CustomProperties);
        Assert.IsTrue(deserializedBase.CustomProperties.ContainsKey("boardType"));

        // deserialized back to BoardType
        var boardTypeJson = deserializedBase.CustomProperties["boardType"].ToString();
        var boardTypeFromCustom = JsonConvert.DeserializeObject<BoardType>(boardTypeJson, SerializerSettings.Default);
        Assert.IsNotNull(boardTypeFromCustom);
        Assert.AreEqual(boardType.BoardCode, boardTypeFromCustom.BoardCode);
    }
}