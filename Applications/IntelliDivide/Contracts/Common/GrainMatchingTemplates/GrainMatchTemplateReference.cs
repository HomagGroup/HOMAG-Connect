using System.Globalization;
using System.Linq;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Describes a reference to a grain matching template.
/// </summary>
[JsonConverter(typeof(GrainMatchTemplateReferenceConverter))]
public class GrainMatchTemplateReference
{
    /// <summary>
    /// Gets or sets whether the template should be divided within the pattern or in separate pattern.
    /// </summary>
    public GrainMatchingTemplateOptionsDividing Dividing { get; set; } = GrainMatchingTemplateOptionsDividing.SeparatePattern;

    /// <summary>
    /// Gets or sets the grain of the template.
    /// </summary>
    public Grain Grain { get; set; } = Grain.None;

    /// <summary>
    /// Gets or sets the instance of the template.
    /// </summary>
    public int Instance { get; set; } = 1;

    /// <summary>
    /// Gets or sets the positions within the template.
    /// </summary>
    public GrainMatchTemplatePosition[] Positions { get; set; }

    /// <summary>
    /// Gets or sets the template reference as state in the template library.
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// Gets or sets the trims of the template.
    /// </summary>
    public GrainMatchingTemplateOptionsTrims Trims { get; set; } = GrainMatchingTemplateOptionsTrims.AllSides;

    /// <summary>
    /// Converts a string into <see cref="GrainMatchTemplateReference" />.
    /// </summary>
    public static GrainMatchTemplateReference FromString(string grainMatchingTemplateReference)
    {
        var result = new GrainMatchTemplateReference();

        const int templateIndex = 0;
        const int positionIndex = 1;
        const int instanceIndex = 2;
        const int optionsIndex = 3;

        var parts = grainMatchingTemplateReference.Split(':');

        result.Template = parts[templateIndex].Trim();

        if (parts.Length >= positionIndex)
        {
            var strings = parts[positionIndex].Trim().Split(' ');

            result.Positions = strings.Select(s => (GrainMatchTemplatePosition)s).ToArray();
        }

        if (parts.Length >= instanceIndex && int.TryParse(parts[instanceIndex].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var instance))
        {
            result.Instance = instance;
        }

        if (parts.Length >= optionsIndex && int.TryParse(parts[optionsIndex].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var options))
        {
            // Trims / AllSides is the default

            if ((options & (int)GrainMatchingTemplateOptionsTrims.None) == (int)GrainMatchingTemplateOptionsTrims.None)
            {
                result.Trims = GrainMatchingTemplateOptionsTrims.None;
            }

            if ((options & (int)GrainMatchingTemplateOptionsTrims.ShortSides) == (int)GrainMatchingTemplateOptionsTrims.ShortSides)
            {
                result.Trims = GrainMatchingTemplateOptionsTrims.ShortSides;
            }

            // Dividing / SeparatePattern is the default

            if ((options & (int)GrainMatchingTemplateOptionsDividing.WithinPattern) == (int)GrainMatchingTemplateOptionsDividing.WithinPattern)
            {
                result.Dividing = GrainMatchingTemplateOptionsDividing.WithinPattern;
            }

            // Grain / None is the default

            if ((options & (int)Grain.Lengthwise) == (int)Grain.Lengthwise)
            {
                result.Grain = Grain.Lengthwise;
            }

            if ((options & (int)Grain.Crosswise) == (int)Grain.Crosswise)
            {
                result.Grain = Grain.Crosswise;
            }
        }

        return result;
    }

    /// <summary>
    /// Converts a <see cref="GrainMatchTemplateReference" /> to string.
    /// </summary>
    public static implicit operator string(GrainMatchTemplateReference grainMatchingTemplate)
    {
        return grainMatchingTemplate.ToString();
    }

    /// <summary>
    /// Converts a string to <see cref="GrainMatchTemplateReference" />.
    /// </summary>
    /// <param name="grainMatchingTemplate"></param>
    public static implicit operator GrainMatchTemplateReference(string grainMatchingTemplate)
    {
        return FromString(grainMatchingTemplate);
    }

    /// <summary>
    /// Converts a <see cref="GrainMatchTemplateReference" /> to string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var positions = Positions.Aggregate("", (current, position) => current + position + " ").Trim();

        var options = (int)Trims;
        options |= (int)Grain;
        options |= (int)Dividing;

        return $"{Template}:{positions}:{Instance}:{options}";
    }
}