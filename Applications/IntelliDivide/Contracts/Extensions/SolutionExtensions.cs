using System.Globalization;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Extensions;

/// <summary>
/// Extension helpers for <see cref="Solution" /> to retrieve localized metadata
/// (name and description) for its <c>Characteristic</c>.
/// </summary>
public static class SolutionExtensions
{
    /// <summary>
    /// Gets a localized description for the solution's characteristic.
    /// Falls back to the enum name when no localization is found.
    /// </summary>
    /// <param name="solution">The solution whose characteristic description is requested.</param>
    /// <param name="culture">The culture to use for localization.</param>
    /// <returns>The localized characteristic description.</returns>
    public static string GetLocalizedDescription(Solution solution, CultureInfo culture)
    {
        return solution.Characteristic.GetLocalizedDescription(solution.CharacteristicsInAddition, culture);
    }

    /// <summary>
    /// Gets a localized display name for the solution's characteristic.
    /// Falls back to the enum name when no localization is found.
    /// </summary>
    /// <param name="solution">The solution whose characteristic name is requested.</param>
    /// <param name="culture">The culture to use for localization.</param>
    /// <returns>The localized characteristic name.</returns>
    public static string GetLocalizedName(Solution solution, CultureInfo culture)
    {
        return solution.Characteristic.GetLocalizedName(culture);
    }
}