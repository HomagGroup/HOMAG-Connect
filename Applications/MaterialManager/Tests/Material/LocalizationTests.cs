using System.Globalization;

using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

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

        propertyDisplayNames[nameof(BoardEntity.Length)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Length), culture),
            $"because BoardEntity.Length should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.Width)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Width), culture),
            $"because BoardEntity.Width should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.Quantity)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Quantity), culture),
            $"because BoardEntity.Quantity should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.ManagementType)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_ManagementType), culture),
            $"because BoardEntity.ManagementType should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.Comments)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Comments), culture),
            $"because BoardEntity.Comments should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.CreationDate)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_CreationDate), culture),
            $"because BoardEntity.CreationDate should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.Location)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_Location), culture),
            $"because BoardEntity.Location should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardEntity.BoardType)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardEntityProperties_BoardType), culture),
            $"because BoardEntity.BoardType should be localized correctly in culture '{cultureName}'");
    }

    /// <summary />
    [TestMethod]
    public void MaterialManager_Localization_BoardMaterialCategory()
    {
        var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

        displayNames.Should().NotBeEmpty(
            "because BoardMaterialCategory enum should have localized display names");
        displayNames[BoardMaterialCategory.Chipboard].Should().Be("Spanplatte",
            "because BoardMaterialCategory.Chipboard should be localized as 'Spanplatte' in German");

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

        propertyDisplayNames[nameof(BoardType.ArticleNumber)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ArticleNumber), culture),
            $"because BoardType.ArticleNumber should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.LastUsed)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_LastUsed), culture),
            $"because BoardType.LastUsed should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.MaterialCode)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_MaterialCode), culture),
            $"because BoardType.MaterialCode should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Thickness)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Thickness), culture),
            $"because BoardType.Thickness should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.MaterialCategory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_MaterialCategory), culture),
            $"because BoardType.MaterialCategory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.CoatingCategory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_CoatingCategory), culture),
            $"because BoardType.CoatingCategory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.StandardQuality)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_StandardQuality), culture),
            $"because BoardType.StandardQuality should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.MaterialLastUsed)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_MaterialLastUsed), culture),
            $"because BoardType.MaterialLastUsed should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.BoardCode)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_BoardCode), culture),
            $"because BoardType.BoardCode should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Width)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Width), culture),
            $"because BoardType.Width should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Length)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Length), culture),
            $"because BoardType.Length should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Grain)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Grain), culture),
            $"because BoardType.Grain should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Costs)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Costs), culture),
            $"because BoardType.Costs should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Density)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Density), culture),
            $"because BoardType.Density should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.BoardTypeType)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_BoardTypeType), culture),
            $"because BoardType.BoardTypeType should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.ManufacturerName)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ManufacturerName), culture),
            $"because BoardType.ManufacturerName should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.ProductName)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_ProductName), culture),
            $"because BoardType.ProductName should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.DecorCode)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_DecorCode), culture),
            $"because BoardType.DecorCode should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.DecorName)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_DecorName), culture),
            $"because BoardType.DecorName should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Gtin)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Gtin), culture),
            $"because BoardType.Gtin should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.EmbossingTop)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_EmbossingTop), culture),
            $"because BoardType.EmbossingTop should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.EmbossingBottom)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_EmbossingBottom), culture),
            $"because BoardType.EmbossingBottom should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalQuantityAvailableWarningLimit)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityAvailableWarningLimit), culture),
            $"because BoardType.TotalQuantityAvailableWarningLimit should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalAreaAvailableWarningLimit)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaAvailableWarningLimit), culture),
            $"because BoardType.TotalAreaAvailableWarningLimit should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.OptimizeAgainstInfinite)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_OptimizeAgainstInfinite), culture),
            $"because BoardType.OptimizeAgainstInfinite should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.LockedForOptimization)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_LockedForOptimization), culture),
            $"because BoardType.LockedForOptimization should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalQuantityInInventory)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityInInventory), culture),
            $"because BoardType.TotalQuantityInInventory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalQuantityAllocated)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityAllocated), culture),
            $"because BoardType.TotalQuantityAllocated should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalQuantityAvailable)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalQuantityAvailable), culture),
            $"because BoardType.TotalQuantityAvailable should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalAreaInInventory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaInInventory), culture),
            $"because BoardType.TotalAreaInInventory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalAreaAllocated)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaAllocated), culture),
            $"because BoardType.TotalAreaAllocated should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.TotalAreaAvailable)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_TotalAreaAvailable), culture),
            $"because BoardType.TotalAreaAvailable should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.InsufficientInventory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_InsufficientInventory), culture),
            $"because BoardType.InsufficientInventory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardType.Comments)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeProperties_Comments), culture),
            $"because BoardType.Comments should be localized correctly in culture '{cultureName}'");
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

        propertyDisplayNames[nameof(BoardTypeAllocation.BoardCode)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeAllocationProperties_BoardCode), culture),
            $"because BoardTypeAllocation.BoardCode should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.Quantity)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeAllocationProperties_Quantity), culture),
            $"because BoardTypeAllocation.Quantity should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.AdditionalData)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_AdditionalData), culture),
            $"because BoardTypeAllocation.AdditionalData should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.AdditionalProperties)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_AdditionalProperties), culture),
            $"because BoardTypeAllocation.AdditionalProperties should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.Comments)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_Comments), culture),
            $"because BoardTypeAllocation.Comments should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.CreatedAt)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_CreatedAt), culture),
            $"because BoardTypeAllocation.CreatedAt should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.CreatedBy)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_CreatedBy), culture),
            $"because BoardTypeAllocation.CreatedBy should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.Name)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_Name), culture),
            $"because BoardTypeAllocation.Name should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.Source)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_Source), culture),
            $"because BoardTypeAllocation.Source should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeAllocation.Workstation)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.AllocationProperties_Workstation), culture),
            $"because BoardTypeAllocation.Workstation should be localized correctly in culture '{cultureName}'");
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

        propertyDisplayNames[nameof(BoardTypeDetails.AdditionalData)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeDetailsProperties_AdditionalData), culture),
            $"because BoardTypeDetails.AdditionalData should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeDetails.Allocations)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeDetailsProperties_Allocations), culture),
            $"because BoardTypeDetails.Allocations should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeDetails.Inventory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeDetailsProperties_Inventory), culture),
            $"because BoardTypeDetails.Inventory should be localized correctly in culture '{cultureName}'");
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

        propertyDisplayNames[nameof(BoardTypeInventory.Code)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeInventoryProperties_Code), culture),
            $"because BoardTypeInventory.Code should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeInventory.Location)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeInventoryProperties_Location), culture),
            $"because BoardTypeInventory.Location should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeInventory.Workstation)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeInventoryProperties_Workstation), culture),
            $"because BoardTypeInventory.Workstation should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeInventory.Quantity)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeInventoryProperties_Quantity), culture),
            $"because BoardTypeInventory.Quantity should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeInventory.AdditionalCommentsBoards)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.BoardTypeInventoryProperties_AdditionalCommentsBoards), culture),
            $"because BoardTypeInventory.AdditionalCommentsBoards should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(BoardTypeInventory.CreationDate)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.BoardTypeInventoryProperties_CreationDate), culture),
            $"because BoardTypeInventory.CreationDate should be localized correctly in culture '{cultureName}'");
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
                $"because EdgebandMaterialCategory.{value} should be localized correctly in culture '{cultureName}'");
        }
    }

    /// <summary />
    [TestMethod]
    public void MaterialManager_Localization_Grain()
    {
        var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));

        displayNames.Should().HaveCount(3,
            "because Grain enum should have 3 values");
        displayNames[Grain.None].Should().Be("Keine",
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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

        propertyDisplayNames[nameof(MaterialType.AdditionalData)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_AdditionalData), culture),
            $"because Material.AdditionalData should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.AverageCosts)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_AverageCosts), culture),
            $"because Material.AverageCosts should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.BoardParameterForOptimization)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_BoardParameterForOptimization), culture),
            $"because Material.BoardParameterForOptimization should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.CoatingCategory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_CoatingCategory), culture),
            $"because Material.CoatingCategory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.Code)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_Code), culture),
            $"because Material.Code should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.DecorCode)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_DecorCode), culture),
            $"because Material.DecorCode should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.DecorName)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_DecorName), culture),
            $"because Material.DecorName should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.Density)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_Density), culture),
            $"because Material.Density should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.Manufacturer)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_Manufacturer), culture),
            $"because Material.Manufacturer should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.MasterDataComments)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_MasterDataComments), culture),
            $"because Material.MasterDataComments should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.MaterialCategory)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_MaterialCategory), culture),
            $"because Material.MaterialCategory should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.MaterialParameterForOptimization)].Should().Be(
            Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_MaterialParameterForOptimization), culture),
            $"because Material.MaterialParameterForOptimization should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.ProductName)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_ProductName), culture),
            $"because Material.ProductName should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.StandardQuality)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_StandardQuality), culture),
            $"because Material.StandardQuality should be localized correctly in culture '{cultureName}'");
        propertyDisplayNames[nameof(MaterialType.Thickness)].Should().Be(Resources.ResourceManager.GetString(nameof(Resources.MaterialProperties_Thickness), culture),
            $"because Material.Thickness should be localized correctly in culture '{cultureName}'");
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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
            displayNames[value].Should().Be(CapitalizeFirstLetter(expected),
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