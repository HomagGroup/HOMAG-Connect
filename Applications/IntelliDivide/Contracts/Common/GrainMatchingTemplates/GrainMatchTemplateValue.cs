using System.Globalization;
using System.Linq;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Describes a reference to a grain matching template.
/// </summary>
[JsonConverter(typeof(GrainMatchTemplateReferenceConverter))]
public class GrainMatchTemplateReference
{
    /// <summary>
    /// Gets or sets the instance of the template.
    /// </summary>
    public int Instance { get; set; } = 1;

    /// <summary>
    /// Gets or sets the options for the template.
    /// </summary>
    public GrainMatchingTemplateOptions Options { get; set; } = GrainMatchingTemplateOptions.None;

    /// <summary>
    /// Gets or sets the positions within the template.
    /// </summary>
    public GrainMatchTemplatePosition[] Positions { get; set; }

    /// <summary>
    /// Gets or sets the template reference as state in the template library.
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// Converts a string into <see cref="GrainMatchTemplateReference" />.
    /// </summary>
    public static GrainMatchTemplateReference FromString(string grainMatchingTemplateReference)
    {
        var result = new GrainMatchTemplateReference();

        const int templateIndex = 0;
        const int positionIndex = 1;
        const int instanceIndex = 2;
        const int optionsIndex = 4;

        var parts = grainMatchingTemplateReference.Split(':');

        if (parts.Length - 1 >= templateIndex)
        {
            result.Template = parts[templateIndex].Trim();
        }

        if (parts.Length - 1 >= positionIndex)
        {
            var strings = parts[positionIndex].Split(' ');

            result.Positions = strings.Select(s => (GrainMatchTemplatePosition)s).ToArray();
        }

        if (parts.Length - 1 >= instanceIndex && int.TryParse(parts[instanceIndex], NumberStyles.Integer, CultureInfo.InvariantCulture, out var instance))
        {
            result.Instance = instance;
        }

        if (parts.Length - 1 >= optionsIndex && int.TryParse(parts[optionsIndex], NumberStyles.Integer, CultureInfo.InvariantCulture, out var options))
        {
            result.Options = (GrainMatchingTemplateOptions)options;
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

        return $"{Template}:{positions}:{Instance}:{(int)Options}";
    }
}