using Shouldly;
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Update;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Integration;

/// <summary>
/// Integration tests for material contract nullability changes (spec 003).
/// Tests end-to-end scenarios: create + update workflows, validation behavior, and backward compatibility.
/// </summary>
[TestClass]
[TestCategory("DeploymentTests.MaterialAssist.Contracts")]
[TestCategory("DeploymentTests.MaterialAssist.Contracts.Integration")]
public class MaterialContractNullabilityIntegrationTests
{
    /// <summary>
    /// T041: Integration test for board entity create + update workflow.
    /// Verify: Create with all required fields passes HGC validation, update with partial payload (omit Length) succeeds.
    /// </summary>
    [TestMethod]
    public void Integration_BoardEntity_CreateUpdateWorkflow_Success()
    {
        // Arrange - Create board entity with all required fields
        var createRequest = new MaterialAssistRequestBoardEntity
        {
            Id = "board-001",
            Quantity = 10,
            BoardCode = "BOARD-WHITE-18",
            ManagementType = ManagementType.Stack
        };

        // Act 1 - Validate create request
        var createResults = new List<ValidationResult>();
        var createValid = Validator.TryValidateObject(createRequest, new ValidationContext(createRequest), createResults, validateAllProperties: true);

        // Assert 1 - Create should pass (all required fields are provided)
        createValid.ShouldBeTrue("because all required fields are provided");
        createResults.ShouldBeEmpty("because create request should have no validation errors");

        // Act 2 - Update with partial payload (omit Length and Width to simulate PATCH)
        var updateRequest = new MaterialAssistUpdateBoardEntity
        {
            Quantity = 12 // Only update quantity, omit Length/Width
        };

        // Act 2 - Validate update request
        var updateResults = new List<ValidationResult>();
        var updateValid = Validator.TryValidateObject(updateRequest, new ValidationContext(updateRequest), updateResults, validateAllProperties: true);

        // Assert 2 - Update should pass (null Length/Width are valid in PATCH operations)
        updateValid.ShouldBeTrue("because nullable properties can be omitted in PATCH operations");
        updateResults.ShouldBeEmpty("because update with omitted Length/Width is valid");
    }

    /// <summary>
    /// T042: Integration test for edgeband entity create + partial update workflow.
    /// Verify: Create with required Length passes, update omitting Length succeeds (PATCH semantics).
    /// </summary>
    [TestMethod]
    public void Integration_EdgebandEntity_CreatePartialUpdateWorkflow_Success()
    {
        // Arrange - Create edgeband entity with required Length
        var createRequest = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-001",
            Quantity = 1,
            EdgebandCode = "EDGE-WHITE-1",
            ManagementType = ManagementType.Single,
            Length = 50, // Required field - must be provided
            CurrentThickness = 1.2
        };

        // Act 1 - Validate create request
        var createResults = new List<ValidationResult>();
        var createValid = Validator.TryValidateObject(createRequest, new ValidationContext(createRequest), createResults, validateAllProperties: true);

        // Assert 1 - Create should pass (all required fields provided)
        createValid.ShouldBeTrue("because Length is required and provided for creation");
        createResults.ShouldBeEmpty("because create request should have no validation errors");

        // Act 2 - Update with partial payload (omit Length, update only CurrentThickness)
        var updateRequest = new MaterialAssistUpdateEdgebandEntity
        {
            CurrentThickness = 1.3 // Partial update - only thickness
            // Length deliberately omitted (null) - should be valid for PATCH
        };

        // Act 2 - Validate update request
        var updateResults = new List<ValidationResult>();
        var updateValid = Validator.TryValidateObject(updateRequest, new ValidationContext(updateRequest), updateResults, validateAllProperties: true);

        // Assert 2 - Update should pass (omitting Length is valid in PATCH)
        updateValid.ShouldBeTrue("because Length is optional in PATCH operations (nullable in UpdateEdgebandEntity)");
        updateResults.ShouldBeEmpty("because partial update is valid");
    }

    /// <summary>
    /// T043: Integration test for validation behavior on edgeband creation.
    /// Note: Since Length is a `double` (value type), it defaults to 0 when not explicitly set in code.
    /// The [Required] attribute works during JSON deserialization but not on C# objects without binding context.
    /// This test verifies that explicitly setting Length=0 (minimum boundary) is valid.
    /// </summary>
    [TestMethod]
    public void Integration_EdgebandEntity_CreateWithMinimumLength_Success()
    {
        // Arrange - Create edgeband with Length set to minimum boundary
        var createRequest = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-002",
            Quantity = 1,
            EdgebandCode = "EDGE-WHITE-2",
            ManagementType = ManagementType.Single,
            Length = 0 // Minimum boundary value
        };

        // Act - Validate create request
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(createRequest, new ValidationContext(createRequest), results, validateAllProperties: true);

        // Assert - Validation should pass because Length=0 is at the minimum boundary
        isValid.ShouldBeTrue("because Length=0 is at the minimum boundary and is a valid value");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T043: Integration test for validation error on invalid update with out-of-range value.
    /// Verify: Attempting to update edgeband with Length above maximum fails HGC validation.
    /// </summary>
    [TestMethod]
    public void Integration_EdgebandEntity_UpdateWithInvalidLength_Fails()
    {
        // Arrange - Try to update edgeband with Length above maximum (2000)
        var updateRequest = new MaterialAssistUpdateEdgebandEntity
        {
            Length = 2100 // Above maximum (2000) - should fail Range validation
        };

        // Act - Validate update request
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(updateRequest, new ValidationContext(updateRequest), results, validateAllProperties: true);

        // Assert - Validation should fail because Length exceeds Range(0, 2000)
        isValid.ShouldBeFalse("because Length=2100 exceeds the maximum boundary (2000)");
        results.ShouldNotBeEmpty("because Range validation error should be reported");
        results.Any(r => r.MemberNames.Contains("Length")).ShouldBeTrue("because Length should have validation error");
    }

    /// <summary>
    /// T043: Integration test for backward compatibility.
    /// Verify: Clients using MaterialAssist contracts unchanged by these changes (no new required fields).
    /// </summary>
    [TestMethod]
    public void Integration_BackwardCompatibility_NoNewRequiredFields()
    {
        // Arrange - Existing clients may send minimal valid requests
        // For boards: previously could omit Length/Width for updates; still can
        var boardUpdate = new MaterialAssistUpdateBoardEntity
        {
            Quantity = 15 // Minimal valid update - only quantity
        };

        // Act 1 - Validate board update
        var boardResults = new List<ValidationResult>();
        var boardValid = Validator.TryValidateObject(boardUpdate, new ValidationContext(boardUpdate), boardResults, validateAllProperties: true);

        // Assert 1 - Should pass (no new required fields added to Update DTOs)
        boardValid.ShouldBeTrue("because no new required fields were added to UpdateBoardEntity");

        // Arrange - For edgebands: update operations should still accept omitted Length
        var edgebandUpdate = new MaterialAssistUpdateEdgebandEntity
        {
            CurrentThickness = 1.5 // Minimal valid update - omit Length
        };

        // Act 2 - Validate edgeband update
        var edgeResults = new List<ValidationResult>();
        var edgeValid = Validator.TryValidateObject(edgebandUpdate, new ValidationContext(edgebandUpdate), edgeResults, validateAllProperties: true);

        // Assert 2 - Should pass (UpdateEdgebandEntity.Length remains nullable, no breaking change)
        edgeValid.ShouldBeTrue("because UpdateEdgebandEntity.Length remains nullable for backward compatibility");
    }

    /// <summary>
    /// T043: Integration test for constraint enforcement with boundary values.
    /// Verify: All Range constraints work as designed for both min and max boundaries.
    /// </summary>
    [TestMethod]
    public void Integration_AllConstraints_BoundaryValidation()
    {
        // Test 1: Board Length at minimum boundary (0.1)
        var boardMinLength = new MaterialAssistUpdateBoardEntity { Length = 0.1 };
        var boardMinResults = new List<ValidationResult>();
        var boardMinValid = Validator.TryValidateObject(boardMinLength, new ValidationContext(boardMinLength), boardMinResults, validateAllProperties: true);
        boardMinValid.ShouldBeTrue("because Length=0.1 is at the minimum boundary for boards");

        // Test 2: Board Length at maximum boundary (19999.9)
        var boardMaxLength = new MaterialAssistUpdateBoardEntity { Length = 19999.9 };
        var boardMaxResults = new List<ValidationResult>();
        var boardMaxValid = Validator.TryValidateObject(boardMaxLength, new ValidationContext(boardMaxLength), boardMaxResults, validateAllProperties: true);
        boardMaxValid.ShouldBeTrue("because Length=19999.9 is at the maximum boundary for boards");

        // Test 3: Board Width at minimum boundary (0.1)
        var boardMinWidth = new MaterialAssistUpdateBoardEntity { Width = 0.1 };
        var boardMinWidthResults = new List<ValidationResult>();
        var boardMinWidthValid = Validator.TryValidateObject(boardMinWidth, new ValidationContext(boardMinWidth), boardMinWidthResults, validateAllProperties: true);
        boardMinWidthValid.ShouldBeTrue("because Width=0.1 is at the minimum boundary for boards");

        // Test 4: Edgeband Length at minimum boundary (0)
        var edgeMinLength = new MaterialAssistUpdateEdgebandEntity { Length = 0 };
        var edgeMinResults = new List<ValidationResult>();
        var edgeMinValid = Validator.TryValidateObject(edgeMinLength, new ValidationContext(edgeMinLength), edgeMinResults, validateAllProperties: true);
        edgeMinValid.ShouldBeTrue("because Length=0 is at the minimum boundary for edgebands");

        // Test 5: Edgeband Length at maximum boundary (2000)
        var edgeMaxLength = new MaterialAssistUpdateEdgebandEntity { Length = 2000 };
        var edgeMaxResults = new List<ValidationResult>();
        var edgeMaxValid = Validator.TryValidateObject(edgeMaxLength, new ValidationContext(edgeMaxLength), edgeMaxResults, validateAllProperties: true);
        edgeMaxValid.ShouldBeTrue("because Length=2000 is at the maximum boundary for edgebands");
    }
}
