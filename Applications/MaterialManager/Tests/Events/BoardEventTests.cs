using FluentAssertions;

using HomagConnect.Base;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Events.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

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
    public void Events_BoardEntityCreatedEvent_SerializeDeserialize()
    {
        var boardEntityCreatedEvent = new BoardEntityCreatedEvent();

        boardEntityCreatedEvent.SubscriptionId = Guid.NewGuid();
        boardEntityCreatedEvent.BoardEntity = new BoardEntity();

        boardEntityCreatedEvent.Trace();

        boardEntityCreatedEvent.IsValid.Should().BeTrue(
            "because BoardEntityCreatedEvent should be valid after setting required properties");

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
        deserializedTyped.Should().NotBeNull(
            "because BoardEntityCreatedEvent should be successfully deserialized from JSON");
        deserializedTyped!.BoardEntity.Should().NotBeNull(
            "because BoardEntity property should be included in the serialized event");
        deserializedTyped.BoardEntity.Id.Should().Be(boardEntity.Id,
            $"because deserialized BoardEntity should have ID '{boardEntity.Id}'");

        deserializedBase.Should().NotBeNull(
            "because AppEvent should be successfully deserialized from JSON");
        deserializedBase!.CustomProperties.Should().NotBeNull(
            "because CustomProperties should contain event-specific data");
        deserializedBase.CustomProperties.Should().ContainKey("boardEntity",
            "because boardEntity should be stored in CustomProperties when deserialized as AppEvent");

        // deserialized back to BoardEntity
        var boardEntityJson = deserializedBase.CustomProperties["boardEntity"].ToString();

        boardEntityJson.Should().NotBeNull(
            "because boardEntity value in CustomProperties should not be null after serialization");

        var boardEntityFromCustom = JsonConvert.DeserializeObject<BoardEntity>(boardEntityJson!, SerializerSettings.Default);

        boardEntityFromCustom.Should().NotBeNull(
            "because boardEntity from CustomProperties should deserialize back to BoardEntity");
        boardEntityFromCustom!.Id.Should().Be(boardEntity.Id,
            $"because BoardEntity deserialized from CustomProperties should have ID '{boardEntity.Id}'");
    }

    /// <summary />
    [TestMethod]
    public void Events_ListMaterialAssistEvents()
    {
        var assemblies = new[] { typeof(BoardEntityCreatedEvent).Assembly };
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        derivedTypes.Should().NotBeNull(
            "because FindDerivedTypes should return a collection of derived types");
        derivedTypes.Should().NotBeEmpty(
            "because there should be at least one type derived from AppEvent in the MaterialManager assembly");

        derivedTypes.Trace();
    }
}