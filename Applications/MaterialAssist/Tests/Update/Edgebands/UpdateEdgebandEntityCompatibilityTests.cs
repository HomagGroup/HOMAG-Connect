using Shouldly;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands;

/// <summary>
/// Compatibility tests for MaterialAssistUpdateEdgebandEntity Length validation.
/// Verifies that Length remains nullable in PATCH operations (UpdateEdgebandEntity) to allow omission.
/// </summary>
[TestClass]
[TestCategory("DeploymentTests.MaterialAssist.Contracts")]
[TestCategory("DeploymentTests.MaterialAssist.Contracts.Compatibility")]
public class UpdateEdgebandEntityCompatibilityTests
{
    /// <summary>
    /// T040: Length=null should be valid in PATCH operations (no [Required] marker).
    /// This allows partial updates where Length is omitted.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_Null_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Length = null // Explicitly null
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=null is valid in PATCH operations (omit property to leave unchanged)");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty("because Length is not Required in update");
    }

    /// <summary>
    /// T041: Omitting Length from PATCH request should be valid (allows selective updates).
    /// This simulates deserialization where Length field is not present in JSON.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_Omitted_Valid()
    {
        // Arrange - Create entity without setting Length (simulates missing JSON field)
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Comments = "Updated thickness measurement"
            // Length deliberately not set - simulates omitted field in JSON
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length can be omitted in PATCH operations for partial updates");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T042: Length with provided value should still validate Range(0, 2000) constraint.
    /// When Length is provided in PATCH, it must pass range validation.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_Provided_ValidatedWithRange()
    {
        // Arrange
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Length = 1500 // Within valid range [0, 2000]
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=1500 is within the valid range [0, 2000] and should pass validation");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T043: Length above maximum (2000.1) should fail range validation even in PATCH.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_AboveMax_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Length = 2000.1 // Exceeds maximum
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeFalse("because Length=2000.1 exceeds the maximum boundary (2000) and should fail validation");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldNotBeEmpty("because Range validation should fail");
    }

    /// <summary>
    /// T043: Length below minimum (negative) should fail range validation.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_BelowMin_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Length = -1 // Below minimum
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeFalse("because Length=-1 is below the minimum boundary (0) and should fail validation");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldNotBeEmpty();
    }

    /// <summary>
    /// T043: Length at boundary (0) should be valid.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_MinBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Length = 0
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=0 is at the minimum boundary and should be valid");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T044: Length at boundary (2000) should be valid.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_MaxBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateEdgebandEntity
        {
            Length = 2000
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=2000 is at the maximum boundary and should be valid");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T045: Verify Length does NOT have [Required] attribute in update DTO.
    /// This confirms that PATCH operations can omit Length without validation errors.
    /// </summary>
    [TestMethod]
    public void UpdateEdgebandEntity_Length_NoRequiredAttribute()
    {
        // Act - Inspect the Length property for [Required] attribute
        var property = typeof(MaterialAssistUpdateEdgebandEntity).GetProperty(nameof(MaterialAssistUpdateEdgebandEntity.Length));
        var hasRequired = property?.GetCustomAttributes(typeof(RequiredAttribute), false).Any() ?? false;

        // Assert
        hasRequired.ShouldBeFalse("because Length property should NOT have [Required] attribute for PATCH semantics");
    }
}
