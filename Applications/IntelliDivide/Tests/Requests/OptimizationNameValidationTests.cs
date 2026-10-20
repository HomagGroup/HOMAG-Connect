using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts.Request;

namespace HomagConnect.IntelliDivide.Tests.Requests;

[TestClass]
[UnitTest("IntelliDivide.Contracts")]
public class OptimizationNameValidationTests
{
    [TestMethod]
    public void OptimizationName_WithInvalidCharacters_ReturnsValidationError()
    {
        var request = new OptimizationRequest { Name = "Order/2025" };
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(request) { MemberName = nameof(request.Name) };

        var isValid = Validator.TryValidateProperty(request.Name, validationContext, results);

        Assert.IsFalse(isValid);
        Assert.AreEqual(1, results.Count);
        StringAssert.Contains(results[0].ErrorMessage, "'/'");
    }

    [TestMethod]
    public void OptimizationName_WithAllowedCharacters_PassesValidation()
    {
        var request = new OptimizationRequest { Name = "Order_2025-10 (Batch 1)" };
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(request) { MemberName = nameof(request.Name) };

        var isValid = Validator.TryValidateProperty(request.Name, validationContext, results);

        Assert.IsTrue(isValid);
        Assert.AreEqual(0, results.Count);
    }
}
