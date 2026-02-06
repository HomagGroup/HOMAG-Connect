#nullable enable

using System;
using System.Globalization;
using System.Linq;
using System.Text;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Extensions;

/// <summary>
/// Extensions to compose localized descriptions for <see cref="SolutionCharacteristic" /> values,
/// including any additional characteristics.
/// </summary>
public static class SolutionCharacteristicExtensions
{
    /// <summary>
    /// Builds a localized description for the given characteristic, optionally appending
    /// a list of additional characteristics, separated by commas and preceded by a localized lead-in.
    /// </summary>
    /// <param name="solutionCharacteristic">Primary characteristic.</param>
    /// <param name="additionalCharacteristics">Optional list of additional characteristics to display.</param>
    /// <param name="culture">Culture used for localization.</param>
    /// <returns>Localized description string without trailing separators.</returns>
    public static string GetLocalizedDescription(this SolutionCharacteristic solutionCharacteristic, SolutionCharacteristic[]? additionalCharacteristics, CultureInfo culture)
    {
        var description = solutionCharacteristic.GetLocalizedDescription(culture);

        if (additionalCharacteristics is { Length: > 0 })
        {
            var sb = new StringBuilder(description);

            sb.Append(Environment.NewLine);
            sb.Append(SolutionCharacteristicDisplayNames.AdditionalCharacteristicsDescription);
            sb.Append(' ');

            // Append names joined by comma and space, localized per characteristic
            var joined = string.Join(", ", additionalCharacteristics.Select(ch => ch.GetLocalizedName(culture)));

            sb.Append(joined);

            description = sb.ToString();
        }

        // Ensure no stray separators or spaces at ends
        return description.Trim(' ', ',');
    }
}