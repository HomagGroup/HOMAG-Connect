namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Defines the trims for a grain matching template.
/// </summary>
public enum GrainMatchingTemplateOptionsTrims
{
    /// <summary>
    /// The template has trims on the long sides.
    /// </summary>
    AllSides = 0,

    /// <summary>
    /// The template has no trims.
    /// </summary>
    None = 4,

    /// <summary>
    /// The template has trims on the short sides.
    /// </summary>
    ShortSides = 8
}