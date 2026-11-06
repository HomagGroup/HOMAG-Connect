using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts;

/// <summary>
/// Defines edgeband length validation rules for both creation and update operations.
/// </summary>
/// <remarks>
/// <para>This interface consolidates length validation across different operation contexts:</para>
/// <para><strong>Creation Operations</strong>: Implementations define Length as <c>double</c> (non-nullable)
/// with <c>[Required]</c> attribute. Must be between 0 and 2000 metres. Edgebands are defined by their coil length,
/// making this mandatory at creation. Example: <c>MaterialAssistRequestEdgebandEntity</c>.</para>
/// <para><strong>Update Operations (PATCH)</strong>: Implementations define Length as <c>double?</c> (nullable)
/// to support partial updates. When omitted, current value is unchanged; when provided, must be between 0 and 2000 metres.
/// Example: <c>MaterialAssistUpdateEdgebandEntity</c>.</para>
/// <para><strong>Unit</strong>: Length is expressed in metres (m), representing the coil length.</para>
/// <para><strong>Constraint Validation</strong>: <c>[Range(0, 2000)]</c> attribute enforces the valid range
/// in both creation and update contexts.</para>
/// </remarks>
public interface IMaterialAssistEdgebandLength
{
    /// <summary>
    /// Coil length of the edgeband in metres (m).
    /// <para>Valid Range: 0 - 2000 m.</para>
    /// </summary>
    /// <remarks>
    /// <para><strong>Creation</strong>: Required property (marked with <c>[Required]</c>). Edgebands are identified and tracked by coil length,
    /// making this mandatory. Must be explicitly provided.</para>
    /// <para><strong>Update</strong>: Optional property. Omit in JSON to leave unchanged; provide to validate and update.</para>
    /// <para>See implementing class documentation for operation-specific details.</para>
    /// </remarks>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.00)]
    double? Length { get; set; }
}
