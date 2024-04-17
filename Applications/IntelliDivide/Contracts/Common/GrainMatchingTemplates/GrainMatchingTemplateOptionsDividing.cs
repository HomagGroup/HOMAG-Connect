namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Defines the dividing options for a grain matching template.
/// </summary>
public enum GrainMatchingTemplateOptionsDividing
{
    /// <summary>
    /// The template should be divided in  a separate pattern.
    /// </summary>
    SeparatePattern = 0,

    /// <summary>
    /// The template should be divided within the pattern.
    /// </summary>
    WithinPattern = 16
}