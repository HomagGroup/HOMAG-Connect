using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;
using Shouldly;
using MaterialType = HomagConnect.MaterialManager.Contracts.Material.Boards.Material;
namespace HomagConnect.MaterialManager.Tests.Material;
/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
public class LocalizationTests
{
    /// <summary>
    /// </summary>
    /// <param name="cultureName"></param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BoardEntity(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var boardEntity = new BoardEntity();
        var propertyDisplayNames = boardEntity.GetPropertyDisplayNames(culture);
        foreach (var propertyName in propertyDisplayNames.Keys)
        {
            var resourceKey = $"BoardEntityProperties_{propertyName}";
            var expected = CapitalizeFirstLetter(Resources.ResourceManager.GetString(resourceKey, culture));
            if (string.IsNullOrEmpty(expected)) continue; //not all properties have resources like HasGrain
            propertyDisplayNames[propertyName].ShouldBe(expected,
                $"because BoardEntity.{propertyName} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary />
    [TestMethod]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public void MaterialManager_Localization_BoardMaterialCategory()
    {
        var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));
        displayNames.ShouldNotBeEmpty(
            "because BoardMaterialCategory enum should have localized display names");
        displayNames[BoardMaterialCategory.Chipboard].ShouldBe("Spanplatte",
            "because BoardMaterialCategory.Chipboard should be localized as 'Spanplatte' in German");
        displayNames[BoardMaterialCategory.Ceramic].ShouldBe("Keramik",
            "because BoardMaterialCategory.Ceramic should be localized as 'Keramik' in German");
        displayNames[BoardMaterialCategory.FloatGlass].ShouldBe("Floatglas",
            "because BoardMaterialCategory.FloatGlass should be localized as 'Floatglas' in German");
        displayNames.Trace();
    }
    /// <summary>
    /// </summary>
    /// <param name="cultureName"></param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BoardType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var boardType = new BoardType();
        var propertyDisplayNames = boardType.GetPropertyDisplayNames(culture);
        foreach (var propertyName in propertyDisplayNames.Keys)
        {
            var resourceKey = $"BoardTypeProperties_{propertyName}";
            var expected = CapitalizeFirstLetter(Resources.ResourceManager.GetString(resourceKey, culture));
            if (string.IsNullOrEmpty(expected)) continue; //not all properties have resources like HasGrain
            propertyDisplayNames[propertyName].ShouldBe(expected,
                $"because BoardType.{propertyName} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// </summary>
    /// <param name="cultureName"></param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BoardTypeAllocation(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var allocation = new BoardTypeAllocation();
        var propertyDisplayNames = allocation.GetPropertyDisplayNames(culture);
        foreach (var propertyName in propertyDisplayNames.Keys)
        {
            var resourceKey = $"BoardTypeAllocationProperties_{propertyName}";
            var expected = CapitalizeFirstLetter(Resources.ResourceManager.GetString(resourceKey, culture));
            if (string.IsNullOrEmpty(expected)) continue; //not all properties have resources like HasGrain
            propertyDisplayNames[propertyName].ShouldBe(expected,
                $"because BoardTypeAllocation.{propertyName} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// </summary>
    /// <param name="cultureName"></param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BoardTypeDetails(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var details = new BoardTypeDetails();
        var propertyDisplayNames = details.GetPropertyDisplayNames(culture);

        foreach (var propertyName in propertyDisplayNames.Keys)
        {
            var resourceKey = $"BoardTypeDetailsProperties_{propertyName}";
            var expected = CapitalizeFirstLetter(Resources.ResourceManager.GetString(resourceKey, culture));
            if (string.IsNullOrEmpty(expected)) continue; //not all properties have resources like HasGrain
            propertyDisplayNames[propertyName].ShouldBe(expected,
                $"because BoardTypeDetails.{propertyName} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// </summary>
    /// <param name="cultureName"></param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BoardTypeInventory(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var inventory = new BoardTypeInventory();
        var propertyDisplayNames = inventory.GetPropertyDisplayNames(culture);
        foreach (var propertyName in propertyDisplayNames.Keys)
        {
            var resourceKey = $"BoardTypeInventoryProperties_{propertyName}";
            var expected = CapitalizeFirstLetter(Resources.ResourceManager.GetString(resourceKey, culture));
            if (string.IsNullOrEmpty(expected)) continue; //not all properties have resources like HasGrain
            propertyDisplayNames[propertyName].ShouldBe(expected,
                $"because Material.{propertyName} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the BoardTypeType enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BoardTypeType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<BoardTypeType>(culture);
        foreach (BoardTypeType value in Enum.GetValues(typeof(BoardTypeType)))
        {
            var expected = BoardTypeTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because BoardTypeType.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the BookHeightMode enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_BookHeightMode(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<BookHeightMode>(culture);
        foreach (BookHeightMode value in Enum.GetValues(typeof(BookHeightMode)))
        {
            var expected = BookHeightModeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because BookHeightMode.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the CoatingCategory enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_CoatingCategory(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<CoatingCategory>(culture);
        foreach (CoatingCategory value in Enum.GetValues(typeof(CoatingCategory)))
        {
            var expected = CoatingCategoryDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because CoatingCategory.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the EdgebandingProcess enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_EdgebandingProcess(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<EdgebandingProcess>(culture);
        foreach (EdgebandingProcess value in Enum.GetValues(typeof(EdgebandingProcess)))
        {
            var expected = EdgebandingProcessDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because EdgebandingProcess.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the EdgebandMaterialCategory enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_EdgebandMaterialCategory(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<EdgebandMaterialCategory>(culture);
        foreach (EdgebandMaterialCategory value in Enum.GetValues(typeof(EdgebandMaterialCategory)))
        {
            var expected = EdgebandMaterialCategoryDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because EdgebandMaterialCategory.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary />
    [TestMethod]
    public void MaterialManager_Localization_Grain()
    {
        var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));
        displayNames.Count.ShouldBe(3,
            "because Grain enum should have 3 values");
        displayNames[Grain.None].ShouldBe("Keine",
            "because Grain.None should be localized as 'Keine' in German");
        displayNames.Trace();
    }
    /// <summary>
    /// Tests the localization of the ImageSize enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_ImageSize(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<ImageSize>(culture);
        foreach (ImageSize value in Enum.GetValues(typeof(ImageSize)))
        {
            var expected = ImageSizeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because ImageSize.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the ImageType enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_ImageType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<ImageType>(culture);
        foreach (ImageType value in Enum.GetValues(typeof(ImageType)))
        {
            var expected = ImageTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because ImageType.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the ImportMode enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_ImportMode(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<ImportMode>(culture);
        foreach (ImportMode value in Enum.GetValues(typeof(ImportMode)))
        {
            var expected = ImportModeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because ImportMode.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the InventoryType enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_InventoryType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<InventoryType>(culture);
        foreach (InventoryType value in Enum.GetValues(typeof(InventoryType)))
        {
            var expected = InventoryTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because InventoryType.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the ManagementType enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_ManagementType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<ManagementType>(culture);
        foreach (ManagementType value in Enum.GetValues(typeof(ManagementType)))
        {
            var expected = ManagementTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because ManagementType.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// </summary>
    /// <param name="cultureName"></param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_Material(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var material = new MaterialType();
        var propertyDisplayNames = material.GetPropertyDisplayNames(culture);

        foreach (var propertyName in propertyDisplayNames.Keys)
        {
            var resourceKey = $"MaterialProperties_{propertyName}";
            var expected = CapitalizeFirstLetter(Resources.ResourceManager.GetString(resourceKey, culture));
            if (string.IsNullOrEmpty(expected)) continue; //not all properties have resources like HasGrain
            propertyDisplayNames[propertyName].ShouldBe(expected,
                $"because Material.{propertyName} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the StandardQuality enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_StandardQuality(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<StandardQuality>(culture);
        foreach (StandardQuality value in Enum.GetValues(typeof(StandardQuality)))
        {
            var expected = StandardQualityDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because StandardQuality.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the TensionTrimType enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_TensionTrimType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<TensionTrimType>(culture);
        foreach (TensionTrimType value in Enum.GetValues(typeof(TensionTrimType)))
        {
            var expected = TensionTrimTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because TensionTrimType.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    /// <summary>
    /// Tests the localization of the WorkstationType enumeration for a given culture.
    /// </summary>
    /// <param name="cultureName">The name of the culture to test (e.g., "de" or "en").</param>
    [DataRow("de")]
    [DataRow("en")]
    [TestMethod]
    public void MaterialManager_Localization_WorkstationType(string cultureName)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var displayNames = EnumExtensions.GetDisplayNames<WorkstationType>(culture);
        foreach (WorkstationType value in Enum.GetValues(typeof(WorkstationType)))
        {
            var expected = WorkstationTypeDisplayNames.ResourceManager.GetString(value.ToString(), culture);
            displayNames[value].ShouldBe(CapitalizeFirstLetter(expected),
                $"because WorkstationType.{value} should be localized correctly in culture '{cultureName}'");
        }
    }
    private static string CapitalizeFirstLetter(string? input)
    {
        if (string.IsNullOrEmpty(input)) return "";
        if (input.Length == 1) return input.ToUpper();
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}