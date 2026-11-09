using Shouldly;
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands;

/// <summary>
/// Boundary tests for MaterialAssistRequestEdgebandEntity Length [Required] and [Range] validation.
/// Verifies that Length is mandatory (non-nullable) and validates Range(0, 2000) constraints.
/// </summary>
[TestClass]
[TestCategory("DeploymentTests.MaterialAssist.Contracts")]
[TestCategory("DeploymentTests.MaterialAssist.Contracts.Boundaries")]
public class RequestEdgebandEntityBoundaryTests
{
    /// <summary>
    /// T033: Length with valid value (100) should be valid.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_Required_Valid()
    {
        // Arrange
        var entity = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-001",
            Quantity = 1,
            EdgebandCode = "EDGE-TEST-001",
            ManagementType = ManagementType.Single,
            Length = 100
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, validateAllProperties: true);

        // Assert
        isValid.ShouldBeTrue("because Length=100 is a valid value and all required fields are set");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T034: Length at minimum boundary (0) should be valid.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_MinBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-001",
            Quantity = 1,
            EdgebandCode = "EDGE-TEST-001",
            ManagementType = ManagementType.Single,
            Length = 0
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, validateAllProperties: true);

        // Assert
        isValid.ShouldBeTrue("because Length=0 is at the minimum boundary [Range(0, 2000)] and should be valid");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T035: Length below minimum boundary (-0.1) should be invalid.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_BelowMin_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-001",
            Quantity = 1,
            EdgebandCode = "EDGE-TEST-001",
            ManagementType = ManagementType.Single,
            Length = -0.1
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, validateAllProperties: true);

        // Assert
        isValid.ShouldBeFalse("because Length=-0.1 is below the minimum boundary (0) and should fail validation");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldNotBeEmpty();
    }

    /// <summary>
    /// T036: Length at maximum boundary (2000) should be valid.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_MaxBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-001",
            Quantity = 1,
            EdgebandCode = "EDGE-TEST-001",
            ManagementType = ManagementType.Single,
            Length = 2000
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, validateAllProperties: true);

        // Assert
        isValid.ShouldBeTrue("because Length=2000 is at the maximum boundary and should be valid");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldBeEmpty();
    }

    /// <summary>
    /// T037: Length above maximum boundary (2000.1) should be invalid.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_AboveMax_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistRequestEdgebandEntity
        {
            Id = "edge-001",
            Quantity = 1,
            EdgebandCode = "EDGE-TEST-001",
            ManagementType = ManagementType.Single,
            Length = 2000.1
        };

        // Act
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(entity, new ValidationContext(entity), results, validateAllProperties: true);

        // Assert
        isValid.ShouldBeFalse("because Length=2000.1 exceeds the maximum boundary (2000) and should fail validation");
        results.Where(r => r.MemberNames.Contains("Length")).ShouldNotBeEmpty();
    }

    /// <summary>
    /// T038: Verify Length is required by checking the property attributes.
    /// This confirms that [Required] attribute is present on Length property.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_HasRequiredAttribute()
    {
        // Act - Inspect the Length property for [Required] attribute
        var property = typeof(MaterialAssistRequestEdgebandEntity).GetProperty(nameof(MaterialAssistRequestEdgebandEntity.Length));
        var hasRequired = property?.GetCustomAttributes(typeof(RequiredAttribute), false).Any() ?? false;

        // Assert
        hasRequired.ShouldBeTrue("because Length property should have [Required] attribute to enforce mandatory constraint");
    }

    /// <summary>
    /// T039: Verify Length has Range constraint by checking property attributes.
    /// This confirms that [Range(0, 2000)] attribute is present on Length property.
    /// </summary>
    [TestMethod]
    public void RequestEdgebandEntity_Length_HasRangeAttribute()
    {
        // Act - Inspect the Length property for [Range] attribute
        var property = typeof(MaterialAssistRequestEdgebandEntity).GetProperty(nameof(MaterialAssistRequestEdgebandEntity.Length));
        var rangeAttr = property?.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;

        // Assert
        rangeAttr.ShouldNotBeNull("because Length property should have [Range] attribute");
        rangeAttr.Minimum.ShouldBe(0);
        rangeAttr.Maximum.ShouldBe(2000.00);
    }
}
