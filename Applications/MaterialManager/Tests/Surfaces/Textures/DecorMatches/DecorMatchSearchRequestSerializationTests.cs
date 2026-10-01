using System.Globalization;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures.DecorMatches;

/// <summary>
/// Serialization tests for the polymorphic decor match search request hierarchy, verifying that the
/// <see cref="DecorMatchSearchRequest.MaterialType" /> discriminator resolves to the correct derived type.
/// </summary>
[TestClass]
[UnitTest("MaterialManager.Textures")]
public class DecorMatchSearchRequestSerializationTests
{
    /// <summary>
    /// Tests that a <see cref="BoardTypeDecorMatchSearchRequest" /> round-trips through JSON serialization
    /// and preserves its derived type and property values.
    /// </summary>
    [TestMethod]
    public void BoardTypeDecorMatchSearchRequest_RoundTrip_PreservesType()
    {
        var request = new BoardTypeDecorMatchSearchRequest
        {
            SearchTerm = "Oak",
            Culture = new CultureInfo("de-DE"),
            Skip = 5,
            Take = 25,
            Material = new BoardType { BoardCode = "P2_Gold_Craft_Oak_19.0" }
        };

        var json = JsonConvert.SerializeObject(request);

        json.ShouldContain("\"MaterialType\":\"Board\"");

        var deserialized = JsonConvert.DeserializeObject<DecorMatchSearchRequest>(json);

        deserialized.ShouldBeOfType<BoardTypeDecorMatchSearchRequest>();
        deserialized.MaterialType.ShouldBe(DecorMatchMaterialType.Board);
        deserialized.SearchTerm.ShouldBe("Oak");
        deserialized.Culture.Name.ShouldBe("de-DE");
        deserialized.Skip.ShouldBe(5);
        deserialized.Take.ShouldBe(25);
        ((BoardTypeDecorMatchSearchRequest)deserialized).Material.ShouldNotBeNull();
        ((BoardTypeDecorMatchSearchRequest)deserialized).Material.BoardCode.ShouldBe("P2_Gold_Craft_Oak_19.0");
    }

    /// <summary>
    /// Tests that deserializing JSON resolves to the correct derived request type based on the
    /// <see cref="DecorMatchSearchRequest.MaterialType" /> discriminator.
    /// </summary>
    [TestMethod]
    public void DecorMatchSearchRequest_DeserializeByDiscriminator_ResolvesDerivedType()
    {
        const string boardJson = "{\"MaterialType\":\"Board\",\"SearchTerm\":\"Oak\"}";
        const string edgebandJson = "{\"MaterialType\":\"Edgeband\",\"SearchTerm\":\"Beech\"}";

        JsonConvert.DeserializeObject<DecorMatchSearchRequest>(boardJson)
            .ShouldBeOfType<BoardTypeDecorMatchSearchRequest>();

        JsonConvert.DeserializeObject<DecorMatchSearchRequest>(edgebandJson)
            .ShouldBeOfType<EdgebandTypeDecorMatchSearchRequest>();
    }

    /// <summary>
    /// Tests that an <see cref="EdgebandTypeDecorMatchSearchRequest" /> round-trips through JSON serialization
    /// and preserves its derived type and property values.
    /// </summary>
    [TestMethod]
    public void EdgebandTypeDecorMatchSearchRequest_RoundTrip_PreservesType()
    {
        var request = new EdgebandTypeDecorMatchSearchRequest
        {
            SearchTerm = "Beech",
            Culture = new CultureInfo("en-US"),
            Material = new EdgebandType { EdgebandCode = "ABS_Beech_1.0" }
        };

        var json = JsonConvert.SerializeObject(request);

        json.ShouldContain("\"MaterialType\":\"Edgeband\"");

        var deserialized = JsonConvert.DeserializeObject<DecorMatchSearchRequest>(json);

        deserialized.ShouldBeOfType<EdgebandTypeDecorMatchSearchRequest>();
        deserialized.MaterialType.ShouldBe(DecorMatchMaterialType.Edgeband);
        deserialized.SearchTerm.ShouldBe("Beech");
        ((EdgebandTypeDecorMatchSearchRequest)deserialized).Material.ShouldNotBeNull();
        ((EdgebandTypeDecorMatchSearchRequest)deserialized).Material.EdgebandCode.ShouldBe("ABS_Beech_1.0");
    }

    /// <summary>
    /// Tests that <see cref="DecorMatchSearchRequest.ForBoard" /> creates a request with all provided values set.
    /// </summary>
    [TestMethod]
    public void ForBoard_CreatesBoardRequest_WithProvidedValues()
    {
        var culture = new CultureInfo("de-DE");
        var request = DecorMatchSearchRequest.ForBoard(
            new BoardType { BoardCode = "P2_Gold_Craft_Oak_19.0" }, "Oak", culture, 5, 25);

        request.MaterialType.ShouldBe(DecorMatchMaterialType.Board);
        request.Material.ShouldNotBeNull();
        request.Material.BoardCode.ShouldBe("P2_Gold_Craft_Oak_19.0");
        request.SearchTerm.ShouldBe("Oak");
        request.Culture.Name.ShouldBe("de-DE");
        request.Skip.ShouldBe(5);
        request.Take.ShouldBe(25);
    }

    /// <summary>
    /// Tests that <see cref="DecorMatchSearchRequest.ForEdgeband" /> creates a request with default paging
    /// and no search term when only the required material is provided.
    /// </summary>
    [TestMethod]
    public void ForEdgeband_CreatesEdgebandRequest_WithDefaults()
    {
        var request = DecorMatchSearchRequest.ForEdgeband(
            new EdgebandType { EdgebandCode = "ABS_Beech_1.0" });

        request.MaterialType.ShouldBe(DecorMatchMaterialType.Edgeband);
        request.Material.ShouldNotBeNull();
        request.Material.EdgebandCode.ShouldBe("ABS_Beech_1.0");
        request.SearchTerm.ShouldBeNull();
        request.Skip.ShouldBe(0);
        request.Take.ShouldBe(50);
    }
}