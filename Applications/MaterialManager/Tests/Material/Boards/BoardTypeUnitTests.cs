using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.Base.TestBase.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using Newtonsoft.Json;
using Shouldly;
using System.Globalization;

namespace HomagConnect.MaterialManager.Tests.Material.Boards;

/// <summary>
/// Contains unit tests for <see cref="BoardType" /> behavior that does not require server interaction.
/// </summary>
[TestClass]
[UnitTest("MaterialManager.Boards")]
public class BoardTypeUnitTests
{
    public TestContext? TestContext { get; set; }

    /// <summary>
    /// Verifies that the specific density is returned when it is explicitly set.
    /// </summary>
    [TestMethod]
    public void BoardType_Density_ReturnsSpecificWhenSet()
    {
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.Chipboard,
            Density = 485.9
        };

        boardType.DensityOrCategoryTypical.ShouldNotBeNull();
        boardType.DensityOrCategoryTypical.Value.ShouldBe(485.9, 0.0001, "because the specific density is set");
    }

    /// <summary>
    /// Verifies that the typical category density is returned when no specific density is set.
    /// </summary>
    [TestMethod]
    public void BoardType_Density_ReturnsCategoryTypicalWhenSpecificMissing()
    {
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.Chipboard,
            Density = null
        };

        boardType.DensityOrCategoryTypical.ShouldBe(650,
            "because typical density should be returned when specific density is not set");
    }

    /// <summary>
    /// Verifies that no density is returned when the material category has no typical density.
    /// </summary>
    [TestMethod]
    public void BoardType_Density_ReturnsNullWhenNoCategoryTypical()
    {
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.Undefined,
            Density = null
        };

        boardType.DensityOrCategoryTypical.ShouldBeNull(
            "because Undefined category does not have a typical density");
    }

    /// <summary>
    /// Verifies that the typical density is converted when switching the unit system.
    /// </summary>
    [TestMethod]
    public void BoardType_Density_ConvertsOnUnitSwitch()
    {
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.MediumdensityFiberboard_MDF,
            Density = null
        };

        boardType.DensityOrCategoryTypical.ShouldBe(700,
            "because MDF typical density should be used in metric");

        var boardTypeImperial = boardType.SwitchUnitSystem(UnitSystem.Imperial, true);

        boardTypeImperial.DensityOrCategoryTypical.ShouldNotBeNull();
        boardTypeImperial.DensityOrCategoryTypical.Value.ShouldBeGreaterThan(40,
            "because MDF typical density should be converted to imperial units");
        boardTypeImperial.DensityOrCategoryTypical.Value.ShouldBeLessThan(50,
            "because MDF typical density should be converted to imperial units");
    }

    /// <summary>
    /// Verifies that a <see cref="BoardType" /> keeps its values after JSON serialization and deserialization.
    /// </summary>
    [TestMethod]
    public void BoardType_SerializeDeserialize_RoundtripPreservesValues()
    {
        var boardType = CreateBoardType();

        boardType.AdditionalProperties.ShouldBeNullOrEmpty();

        var serializedBoardType = JsonConvert.SerializeObject(boardType, SerializerSettings.Default);
        var deserializedBoardType = JsonConvert.DeserializeObject<BoardType>(serializedBoardType, SerializerSettings.Default);

        Assert.IsNotNull(deserializedBoardType, "because the deserialized board type should not be null");

        deserializedBoardType.AdditionalProperties.ShouldBeNullOrEmpty();
        deserializedBoardType.ShouldBeEquivalentTo(boardType);
    }

    /// <summary>
    /// Verifies that dimensions and unit-dependent values change when switching from metric to imperial units.
    /// </summary>
    [TestMethod]
    public void BoardType_SwitchUnitSystem_LengthWidthThicknessChanged()
    {
        var boardTypeMetric = new BoardType
        {
            Length = 2800,
            Width = 2070,
            Thickness = 19,
            TotalAreaAvailableWarningLimit = 60,
            Density = 420
        };

        boardTypeMetric.Trace();

        var boardTypeImperial = boardTypeMetric.SwitchUnitSystem(UnitSystem.Imperial, true);

        boardTypeImperial.Trace();

        boardTypeImperial.UnitSystem.ShouldBe(UnitSystem.Imperial,
            "because board type was switched to Imperial unit system");

        boardTypeImperial.Length.ShouldNotBe(boardTypeMetric.Length,
            "because length should be converted from metric to imperial units");
        boardTypeImperial.Width.ShouldNotBe(boardTypeMetric.Width,
            "because width should be converted from metric to imperial units");
        boardTypeImperial.Thickness.ShouldNotBe(boardTypeMetric.Thickness,
            "because thickness should be converted from metric to imperial units");
        boardTypeImperial.TotalAreaAvailableWarningLimit.ShouldNotBe(boardTypeMetric.TotalAreaAvailableWarningLimit,
            "because total area warning limit should be converted from metric to imperial units");
        boardTypeImperial.Density.ShouldNotBe(boardTypeMetric.Density,
            "because length should be converted from metric to imperial units");
    }

    /// <summary>
    /// Verifies that localized serialization of generated board types produces valid JSON that can be deserialized.
    /// </summary>
    [TestMethod]
    public void BoardType_SerializeLocalized_ReturnsDeserializableJson()
    {
        var boardTypes = new[]
        {
            CreateBoardType(),
            CreateBoardType("BOARD-456", "MAT-456")
        };

        var cultureInfo = CultureInfo.GetCultureInfo("de-DE");
        var serializedBoardTypesLocalized = boardTypes.SerializeLocalized(cultureInfo);

        serializedBoardTypesLocalized.ShouldNotBeNullOrWhiteSpace(
            "because localized serialization should produce JSON content");

        var deserializedBoardTypes = JsonConvert.DeserializeObject(serializedBoardTypesLocalized);

        deserializedBoardTypes.ShouldNotBeNull(
            "because localized serialization should produce valid JSON that can be deserialized");

        TestContext?.AddResultFile(boardTypes.TraceToFile(nameof(BoardType_SerializeLocalized_ReturnsDeserializableJson) + "_BoardTypes").FullName);
        TestContext?.AddResultFile(deserializedBoardTypes.TraceToFile(nameof(BoardType_SerializeLocalized_ReturnsDeserializableJson) + "_Deserialized").FullName);
    }

    private static BoardType CreateBoardType()
    {
        return CreateBoardType("BOARD-123", "MAT-123");
    }

    private static BoardType CreateBoardType(string boardCode, string materialCode)
    {
        return new BoardType
        {
            BoardCode = boardCode,
            MaterialCode = materialCode,
            Length = 2800,
            Width = 2070,
            Thickness = 19,
            Grain = Grain.Lengthwise,
            MaterialCategory = BoardMaterialCategory.Chipboard,
            CoatingCategory = CoatingCategory.Undefined,
            BoardTypeType = BoardTypeType.Board,
            Costs = 14.25,
            ManufacturerName = "HOMAG",
            ProductName = "Test Board",
            ArticleNumber = "ART-123",
            DecorCode = "DEC-123",
            DecorName = "White",
            Gtin = "1234567890123",
            EmbossingTop = "Top",
            EmbossingBottom = "Bottom",
            ExternalSystemId = "EXT-123",
            TotalQuantityInInventory = 12,
            TotalQuantityAllocated = 3,
            TotalQuantityAvailableWarningLimit = 4,
            TotalAreaAvailableWarningLimit = 25.5,
            OptimizeAgainstInfinite = false,
            LockedForOptimization = true,
            LockedForConfiguration = true,
            InsufficientInventory = false,
            Barcode = "BAR-123",
            MaterialParameterForOptimization = "MaterialParameter",
            BoardParameterForOptimization = "BoardParameter",
            Comments = "Roundtrip serialization",
            Thumbnail = new Uri("https://example.com/boards/board-123.png"),
            LastUsed = new DateTimeOffset(2025, 1, 2, 3, 4, 5, TimeSpan.Zero),
            MaterialLastUsed = new DateTimeOffset(2025, 1, 1, 8, 0, 0, TimeSpan.Zero),
            UnitSystem = UnitSystem.Metric
        };
    }
}
