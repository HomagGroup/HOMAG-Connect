using Shouldly;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards;

/// <summary>
/// Boundary tests for MaterialAssistUpdateBoardEntity Length and Width Range validation.
/// Verifies [Range(0.1, 19999.9)] constraints are properly enforced on PATCH operations.
/// </summary>
[TestClass]
[TestCategory("DeploymentTests.MaterialAssist.Contracts")]
[TestCategory("DeploymentTests.MaterialAssist.Contracts.Boundaries")]
public class UpdateBoardEntityBoundaryTests
{
    /// <summary>
    /// T027: Length at minimum boundary (0.1) should be valid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Length_MinBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Length = 0.1 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=0.1 is at the minimum boundary and should be valid");
        results.ShouldBeEmpty("because no validation errors should occur");
    }

    /// <summary>
    /// T028: Length below minimum boundary (0.09) should be invalid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Length_BelowMin_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Length = 0.09 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeFalse("because Length=0.09 is below the minimum boundary (0.1) and should fail validation");
        results.ShouldNotBeEmpty("because validation error should be reported");
        results.FirstOrDefault()?.MemberNames.ShouldContain("Length");
    }

    /// <summary>
    /// T029: Length at maximum boundary (19999.9) should be valid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Length_MaxBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Length = 19999.9 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=19999.9 is at the maximum boundary and should be valid");
        results.ShouldBeEmpty("because no validation errors should occur");
    }

    /// <summary>
    /// T030: Length above maximum boundary (20000) should be invalid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Length_AboveMax_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Length = 20000 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeFalse("because Length=20000 exceeds the maximum boundary (19999.9) and should fail validation");
        results.ShouldNotBeEmpty("because validation error should be reported");
        results.FirstOrDefault()?.MemberNames.ShouldContain("Length");
    }

    /// <summary>
    /// T031: Length=null should be valid (PATCH semantics: omit to leave unchanged).
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Length_Null_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Length = null };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Length=null is valid in PATCH operations (omit to leave unchanged)");
        results.ShouldBeEmpty("because nullable properties should not cause validation errors");
    }

    /// <summary>
    /// T032: Width boundary tests (mirror of Length tests).
    /// Width at minimum boundary (0.1) should be valid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Width_MinBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Width = 0.1 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Width=0.1 is at the minimum boundary and should be valid");
        results.ShouldBeEmpty();
    }

    /// <summary>
    /// T032: Width below minimum boundary (0.09) should be invalid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Width_BelowMin_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Width = 0.09 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeFalse("because Width=0.09 is below the minimum boundary (0.1) and should fail validation");
        results.ShouldNotBeEmpty();
        results.FirstOrDefault()?.MemberNames.ShouldContain("Width");
    }

    /// <summary>
    /// T032: Width at maximum boundary (19999.9) should be valid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Width_MaxBoundary_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Width = 19999.9 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Width=19999.9 is at the maximum boundary and should be valid");
        results.ShouldBeEmpty();
    }

    /// <summary>
    /// T032: Width above maximum boundary (20000) should be invalid.
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Width_AboveMax_Invalid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Width = 20000 };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeFalse("because Width=20000 exceeds the maximum boundary (19999.9) and should fail validation");
        results.ShouldNotBeEmpty();
        results.FirstOrDefault()?.MemberNames.ShouldContain("Width");
    }

    /// <summary>
    /// T032: Width=null should be valid (PATCH semantics).
    /// </summary>
    [TestMethod]
    public void UpdateBoardEntity_Width_Null_Valid()
    {
        // Arrange
        var entity = new MaterialAssistUpdateBoardEntity { Width = null };

        // Act
        var results = new List<ValidationResult>();
        var isValid = PropertyValidation.TryValidateObject(entity, new ValidationContext(entity), results);

        // Assert
        isValid.ShouldBeTrue("because Width=null is valid in PATCH operations");
        results.ShouldBeEmpty();
    }
}
