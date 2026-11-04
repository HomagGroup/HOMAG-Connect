using System.Reflection;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialConstraintCoverageTests
{
    private const string Shared = "AdditionalData ArticleNumber Comments Costs DecorCode DecorName Gtin ManufacturerName ProductName Thumbnail DecorId";

    private const string Board =
        "BoardCode ExternalSystemId CoatingCategory Density Grain Length LockedForOptimization LockedForConfiguration MaterialCategory MaterialCode OptimizeAgainstInfinite StandardQuality Thickness TotalQuantityAvailableWarningLimit Type Width EmbossingTop EmbossingBottom";

    private const string Edge =
        "Airtec DecorEmbossingCode DefaultLength EdgebandCode ExternalSystemId FunctionLayerThickness Height Lasertec MachineTechnologyMacro MaterialCategory Process ProtectionFilmThickness TechnologyMacro Thickness TotalLengthAvailableWarningLimit TotalQuantityAvailableWarningLimit";

    [TestMethod]
    [DataRow(typeof(MaterialManagerClientMaterialBoards),
        "ImportInventory(ImportInventoryRequest);UpdateServiceAvailabilityCheck(String,Dictionary`2);UpdateBoardType(String,MaterialManagerUpdateBoardType);UpdateBoardType(String,MaterialManagerUpdateBoardType,FileReference[]);UpdateBoardTypeAllocation(String,BoardTypeAllocationUpdate);PatchBoardType(String,PatchBuilder`1);CreateBoardType(MaterialManagerRequestBoardType);CreateBoardType(MaterialManagerRequestBoardType,FileReference[]);CreateBoardTypeAllocation(BoardTypeAllocationRequest)")]
    [DataRow(typeof(MaterialManagerClientMaterialEdgebands),
        "UpdateEdgebandType(String,MaterialManagerUpdateEdgebandType);UpdateEdgebandType(String,MaterialManagerUpdateEdgebandType,FileReference[]);UpdateEdgebandTypeAllocation(EdgebandTypeAllocationUpdate);PatchEdgebandType(String,PatchBuilder`1);CreateEdgebandType(MaterialManagerRequestEdgebandType);CreateEdgebandType(MaterialManagerRequestEdgebandType,FileReference[]);CreateEdgebandTypeAllocation(EdgebandTypeAllocationRequest)")]
    public void MutationEntryPoints_HaveExplicitCoverageOrOperationalDisposition(Type type, string reviewed)
    {
        var actual = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Where(method => method.Name.StartsWith("Create") || method.Name.StartsWith("Update") ||
                             method.Name.StartsWith("Patch") || method.Name == "ImportInventory")
            .Select(method => $"{method.Name}({string.Join(",", method.GetParameters().Select(parameter => parameter.ParameterType.Name))})")
            .ToArray();
        actual.ShouldBe(reviewed.Split(';'), ignoreOrder: true,
            customMessage: "A new overload needs mapping, validation invocation and zero-HTTP regression coverage.");
    }

    [TestMethod]
    [DynamicData(nameof(ReviewedShapes))]
    public void PublicShape_HasReviewedMappingOrExplicitDisposition(Type type, string reviewed)
    {
        type.GetProperties().Select(property => property.Name)
            .ShouldBe(reviewed.Split(' ', StringSplitOptions.RemoveEmptyEntries), ignoreOrder: true,
                customMessage: $"Review every added/removed {type.Name} property in the material mapping, including inherited/nested contexts.");
    }

    [TestMethod]
    [DataRow(typeof(BoardTypeDetails), "AdditionalData Allocations Inventory")]
    [DataRow(typeof(BoardTypeInventory), "AdditionalCommentsBoards Code CreationDate Location OrderNumber Quantity Workstation AdditionalProperties")]
    [DataRow(typeof(Contracts.Material.Boards.Material),
        "AdditionalData AverageCosts BoardParameterForOptimization CoatingCategory Code DecorCode DecorName Density HasGrain Manufacturer MasterDataComments MaterialCategory MaterialParameterForOptimization ProductName StandardQuality Thickness Thumbnail UnitSystem AdditionalProperties")]
    [DataRow(typeof(CatalogBoardType),
        "ArticleNumber CoatingCategory DecorCode DecorName EmbossingBottom EmbossingTop Grain Gtin Length ManufacturerName MaterialCategory ProductName Thickness Thumbnail Width")]
    [DataRow(typeof(CatalogEdgebandType),
        "ArticleNumber Costs DecorCode DecorEmbossingCode DecorName DefaultLength FunctionLayerThickness Gtin Height ManufacturerName MaterialCategory Process ProductName ProtectionFilmThickness Thickness Thumbnail")]
    [DataRow(typeof(EdgebandEntity), "Comments CreationDate CurrentThickness EdgebandType Id Length Location ManagementType Quantity TotalLength")]
    [DataRow(typeof(EdgebandTypeDetails), "AdditionalData Images Inventory")]
    [DataRow(typeof(EdgebandTypeInventory), "Code Location Workstation Length TotalLength CurrentThickness CreationDate Quantity AdditionalCommentsEdgeband")]
    [DataRow(typeof(EdgebandTypeAllocation), "AllocatedLength Customer EdgebandCode Order Project UsedLength UnitSystem")]
    [DataRow(typeof(StorageLocation), "Barcode LocationId Name LocationType")]
    [DataRow(typeof(Workstation), "Id Name Type Group")]
    [DataRow(typeof(AdditionalDataEntity), "AdditionalProperties Category DownloadFileName DownloadUri Id Name Previews Type Source")]
    [DataRow(typeof(AdditionalDataTexture), "Grain Length Type Width UnitSystem")]
    [DataRow(typeof(AdditionalDataPreview), "Size Uri")]
    public void ReadOnlyAndNestedShapes_HaveExplicitDispositions(Type type, string reviewed)
    {
        type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Select(property => property.Name).ShouldBe(reviewed.Split(' '), ignoreOrder: true,
                customMessage: $"Review nested/read-only {type.Name} before extending material rules.");
    }

    [TestMethod]
    public void RequestAndUpdateTypes_AreNotSilentlyAddedOutsideTheInventory()
    {
        var known = ReviewedShapes().Select(row => (Type)row[0])
            .Concat([typeof(MaterialManagerRequestMaterialType), typeof(MaterialManagerUpdateMaterialType), typeof(StorageImportFilter)])
            .ToHashSet();
        var discovered = typeof(MaterialManagerRequestBoardType).Assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsNested &&
                           (type.Namespace == typeof(MaterialManagerRequestBoardType).Namespace ||
                            type.Namespace == typeof(MaterialManagerUpdateBoardType).Namespace));
        foreach (var type in discovered)
            known.ShouldContain(type, $"Classify the new operation input {type.FullName}.");
    }

    public static IEnumerable<object[]> ReviewedShapes()
    {
        yield return [typeof(MaterialManagerRequestBoardType), Shared + " " + Board];
        yield return [typeof(MaterialManagerUpdateBoardType), Shared + " " + Board];
        yield return [typeof(MaterialManagerRequestEdgebandType), Shared + " " + Edge];
        yield return [typeof(MaterialManagerUpdateEdgebandType), Shared + " " + Edge];
        yield return [typeof(BoardTypeAllocationRequest), "BoardTypeCode Comments CreatedBy Name Quantity Source Workstation"];
        yield return [typeof(BoardTypeAllocationUpdate), "BoardTypeCode Comments Name Quantity Source Workstation"];
        yield return [typeof(EdgebandTypeAllocationRequest), "Customer EdgebandCode Order Project AllocatedLength Comments CreatedBy Source UsedLength Workstation"];
        yield return [typeof(EdgebandTypeAllocationUpdate), "Customer EdgebandCode Order Project AllocatedLength Comments UsedLength Workstation"];
        yield return [typeof(ImportInventoryRequest), "Allocations ImportMode Instances InventoryType Materials StorageSystemName UnitSystem"];
        yield return
        [
            typeof(BoardType),
            "AdditionalProperties LastUsed UnitSystem MaterialCode Thickness MaterialCategory CoatingCategory StandardQuality MaterialLastUsed BoardCode Width Length Grain Costs Density DensityOrCategoryTypical BoardTypeType ManufacturerName ProductName ArticleNumber DecorCode DecorName Gtin EmbossingTop EmbossingBottom DecorId ExternalId ExternalSystemId TotalQuantityAvailableWarningLimit TotalAreaAvailableWarningLimit OptimizeAgainstInfinite LockedForOptimization LockedForConfiguration OrderDemand TotalQuantityInInventory TotalQuantityAllocated TotalQuantityAvailable TotalValueInInventory TotalAreaInInventory TotalAreaAllocated TotalAreaAvailable InsufficientInventory Barcode MaterialParameterForOptimization BoardParameterForOptimization Comments Thumbnail"
        ];
        yield return
        [
            typeof(EdgebandType),
            "LastUsed UnitSystem ExtensionData EdgebandCode ExternalSystemId Height Thickness DefaultLength Costs MaterialCategory Process Lasertec Airtec ProtectionFilmThickness FunctionLayerThickness TechnologyMacro MachineTechnologyMacro ManufacturerName DecorName ProductName DecorCode DecorId DecorEmbossingCode ArticleNumber Gtin ExternalId TotalQuantityAvailableWarningLimit TotalLengthAvailableWarningLimit TotalQuantityAvailable TotalLengthAvailable InsufficientInventory Comments Thumbnail"
        ];
        yield return [typeof(BoardEntity), "BoardType Comments CreationDate Id Length Location ManagementType Quantity Width"];
        yield return [typeof(BoardTypeAllocation), "BoardCode Quantity AdditionalData AdditionalProperties Comments CreatedAt CreatedBy Name Source Workstation"];
    }
}