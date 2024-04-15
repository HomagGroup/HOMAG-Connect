using System.Globalization;

namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Describes the position within a grain matching template.
/// </summary>
public class GrainMatchTemplatePosition
{
    /// <summary>
    /// Gets or sets the column within the template.
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Gets or sets the rows within the template.
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Converts a <see cref="GrainMatchTemplatePosition" /> to string.
    /// </summary>
    public static implicit operator string(GrainMatchTemplatePosition position)
    {
        return $"{position.Column}.{position.Row}";
    }

    /// <summary>
    /// Converts a string into <see cref="GrainMatchTemplatePosition" />.
    /// </summary>
    /// <param name="position"></param>
    public static implicit operator GrainMatchTemplatePosition(string position)
    {
        var result = new GrainMatchTemplatePosition();

        var parts = position.Split('.');

        if (parts.Length > 0 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var column))
        {
            result.Column = column;
        }

        if (parts.Length > 1 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var row))
        {
            result.Row = row;
        }

        return result;
    }
}