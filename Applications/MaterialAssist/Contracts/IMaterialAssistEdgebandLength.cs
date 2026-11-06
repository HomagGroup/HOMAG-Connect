using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts;

/// <summary>
/// Defines shared unit and range metadata for an edgeband coil length in update contracts.
/// </summary>
/// <remarks>
/// <para>Creation requests use a required, non-nullable length. Update requests use this nullable property so callers can omit it
/// to leave the existing length unchanged.</para>
/// </remarks>
public interface IMaterialAssistEdgebandLength
{
    /// <summary>
    /// Length of one edgeband coil.
    /// </summary>
    /// <remarks>
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// <para>Creation requires a length; see <see cref="HomagConnect.MaterialAssist.Contracts.Request.MaterialAssistRequestEdgebandEntity.Length" />.
    /// Updates may omit or set this property to null to leave the existing length unchanged; see
    /// <see cref="HomagConnect.MaterialAssist.Contracts.Update.MaterialAssistUpdateEdgebandEntity.Length" />.</para>
    /// </remarks>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.00)]
    double? Length { get; set; }
}
