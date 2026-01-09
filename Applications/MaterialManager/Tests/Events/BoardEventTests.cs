using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Events.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using Shouldly;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Tests.Events;

/// <inheritdoc />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Material.Events")]
public class BoardEventTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_BoardEntityUpsertedEvent_SerializeDeserialize()
    {
        var boardEntityUpsertedEvent = new BoardEntityUpsertedEvent();

        boardEntityUpsertedEvent.SubscriptionId = Guid.NewGuid();
        boardEntityUpsertedEvent.BoardEntity = new BoardEntity();

        boardEntityUpsertedEvent.Trace();

        boardEntityUpsertedEvent.IsValid.ShouldBeTrue(
            "because BoardEntityUpsertedEvent should be valid after setting required properties");

        TestContext?.AddResultFile(boardEntityUpsertedEvent.TraceToFile("boardEntityUpsertedEvent").FullName);
    }

    /// <summary />
    [TestMethod]
    public void Events_BoardEntityUpsertedEvent_SerializeDeserialize_AsSelf_And_AsAppEvent()
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

        var evt = new BoardEntityUpsertedEvent
        {
            SubscriptionId = Guid.NewGuid(),
            BoardEntity = boardEntity
        };

        // Act
        var json = JsonConvert.SerializeObject(evt, SerializerSettings.Default);

        var deserializedTyped = JsonConvert.DeserializeObject<BoardEntityUpsertedEvent>(json, SerializerSettings.Default);

        var deserializedBase = JsonConvert.DeserializeObject<AppEvent>(json, SerializerSettings.Default);

        // Assert
        deserializedTyped.ShouldNotBeNull(
            "because BoardEntityUpserted should be successfully deserialized from JSON");
        deserializedTyped!.BoardEntity.ShouldNotBeNull(
            "because BoardEntity property should be included in the serialized event");
        deserializedTyped.BoardEntity.Id.ShouldBe(boardEntity.Id,
            $"because deserialized BoardEntity should have ID '{boardEntity.Id}'");

        deserializedBase.ShouldNotBeNull(
            "because AppEvent should be successfully deserialized from JSON");
        deserializedBase!.CustomProperties.ShouldNotBeNull(
            "because CustomProperties should contain event-specific data");
        deserializedBase.CustomProperties.ContainsKey("boardEntity").ShouldBeTrue(
            "because boardEntity should be stored in CustomProperties when deserialized as AppEvent");

        // deserialized back to BoardEntity
        var boardEntityJson = deserializedBase.CustomProperties["boardEntity"].ToString();

        boardEntityJson.ShouldNotBeNull(
            "because boardEntity value in CustomProperties should not be null after serialization");

        var boardEntityFromCustom = JsonConvert.DeserializeObject<BoardEntity>(boardEntityJson!, SerializerSettings.Default);

        boardEntityFromCustom.ShouldNotBeNull(
            "because boardEntity from CustomProperties should deserialize back to BoardEntity");
        boardEntityFromCustom!.Id.ShouldBe(boardEntity.Id,
            $"because BoardEntity deserialized from CustomProperties should have ID '{boardEntity.Id}'");
    }

    /// <summary />
    [TestMethod]
    public void Events_ListMaterialAssistEvents()
    {
        var assemblies = new[] { typeof(BoardEntityUpsertedEvent).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        derivedTypes.ShouldNotBeNull(
            "because FindDerivedTypes should return a collection of derived types");
        derivedTypes.Length.ShouldBeGreaterThan(0,
            "because there should be at least one type derived from AppEvent in the MaterialManager assembly");

        derivedTypes.Trace();
    }
}