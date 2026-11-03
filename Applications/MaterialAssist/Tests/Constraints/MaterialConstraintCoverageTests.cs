using System.Reflection;

using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

[TestClass]
public class MaterialConstraintCoverageTests
{
    [TestMethod]
    public void AllMutationTypes_HaveAnInventoryEntry()
    {
        var known = ReviewedShapes().Select(row => (Type)row[0]).Append(typeof(MaterialAssistRequestBase)).ToHashSet();
        var discovered = typeof(MaterialAssistRequestBase).Assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsNested && type.Namespace != null &&
                           (type.Namespace.EndsWith(".Request") || type.Namespace.EndsWith(".Update") || type.Namespace.EndsWith(".Storage")));
        foreach (var type in discovered)
            Assert.IsTrue(known.Contains(type), $"Classify {type.FullName} before adding material validation.");
    }

    [TestMethod]
    [DataRow(typeof(MaterialAssistClientBoards),
        "CreateBoardType(MaterialManagerRequestBoardType);CreateBoardEntity(MaterialAssistRequestBoardEntity);CreateOffcutEntity(MaterialAssistRequestOffcutEntity);CreateTemplateEntity(MaterialAssistRequestTemplateEntity);UpdateBoardEntity(String,MaterialAssistUpdateBoardEntity);StoreBoardEntity(MaterialAssistStoreBoardEntity);StoreBoardEntity(String,Int32,Int32,StorageLocation)")]
    [DataRow(typeof(MaterialAssistClientEdgebands),
        "CreateEdgebandType(MaterialManagerRequestEdgebandType);CreateEdgebandEntity(MaterialAssistRequestEdgebandEntity);UpdateEdgebandEntity(String,MaterialAssistUpdateEdgebandEntity);StoreEdgebandEntity(MaterialAssistStoreEdgebandEntity);StoreEdgebandEntity(String,StorageLocation,Double)")]
    public void MutationEntryPoints_RequireMappingBeforeNewOverloadsAreAdded(Type type, string reviewed)
    {
        var actual = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Where(method => method.Name.StartsWith("Create") || method.Name.StartsWith("Update") || method.Name.StartsWith("Store"))
            .Select(method => $"{method.Name}({string.Join(",", method.GetParameters().Select(parameter => parameter.ParameterType.Name))})")
            .ToArray();
        CollectionAssert.AreEquivalent(reviewed.Split(';'), actual);
    }

    [TestMethod]
    [DynamicData(nameof(ReviewedShapes))]
    public void PublicShape_HasReviewedMappingOrExplicitDisposition(Type type, string reviewed)
    {
        CollectionAssert.AreEquivalent(reviewed.Split(' '),
            type.GetProperties().Select(property => property.Name).ToArray(),
            $"Review {type.Name} against its operation-specific material mapping.");
    }

    public static IEnumerable<object[]> ReviewedShapes()
    {
        yield return [typeof(MaterialAssistRequestBoardEntity), "Comments Id Quantity BoardCode ManagementType"];
        yield return [typeof(MaterialAssistRequestOffcutEntity), "Comments Id Quantity BoardCode Length Width"];
        yield return [typeof(MaterialAssistRequestTemplateEntity), "Comments Id Quantity BoardCode Length Width"];
        yield return [typeof(MaterialAssistRequestEdgebandEntity), "Comments Id Quantity CurrentThickness EdgebandCode Length ManagementType"];
        yield return [typeof(MaterialAssistUpdateBoardEntity), "Comments Id Length Quantity Width"];
        yield return [typeof(MaterialAssistUpdateEdgebandEntity), "Comments CurrentThickness Id Length Quantity"];
        yield return [typeof(MaterialAssistStoreBoardEntity), "Id Length StorageLocation Width Workstation"];
        yield return [typeof(MaterialAssistStoreEdgebandEntity), "Id Length StorageLocation Workstation"];
    }
}