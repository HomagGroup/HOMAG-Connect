using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

/// <summary>
/// Provides extension methods for working with board material categories.
/// </summary>
public static class BoardMaterialCategoryExtensions
{
    /// <summary>
    /// Gets the typical density for the specified board material category in the given unit system.
    /// </summary>
    /// <remarks>
    /// If the board material category does not have a defined typical density, the method returns
    /// null. The returned value is automatically converted to the appropriate unit system based on the specified
    /// parameter.
    /// </remarks>
    /// <param name="boardMaterialCategory">The board material category for which to retrieve the typical density.</param>
    /// <param name="unitSystem">
    /// The unit system in which to return the density value. Use Metric for kilograms per cubic meter or Imperial for
    /// pounds per cubic foot.
    /// </param>
    /// <returns>
    /// The typical density of the specified board material category in the requested unit system, or null if no typical
    /// density is defined for the category.
    /// </returns>
    public static double? GetTypicalDensity(this BoardMaterialCategory boardMaterialCategory, UnitSystem unitSystem)
    {
        var typicalDensity = typeof(BoardMaterialCategory).GetField(boardMaterialCategory.ToString())?.GetCustomAttribute<TypicalDensityAttribute>();

        if (typicalDensity == null)
        {
            return null;
        }

        var valueDependsOnUnitSystemAttribute = new ValueDependsOnUnitSystemAttribute(BaseUnit.KilogramPerCubicMeter);

        if (unitSystem == UnitSystem.Metric)
        {
            return typicalDensity.Value;
        }

        return typicalDensity.Value * valueDependsOnUnitSystemAttribute.ConversionFactorMetricToImperial;
    }
}