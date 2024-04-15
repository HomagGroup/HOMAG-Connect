using System;

namespace HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;

/// <summary>
/// Defines the options for a grain matching template.
/// </summary>
[Flags]
public enum GrainMatchingTemplateOptions
{
    /// <summary>
    /// The template has special options.
    /// </summary>
    None = 0,

    /// <summary>
    /// The template has the grain lengthwise.
    /// </summary>
    GrainLengthwise = 1,

    /// <summary>
    /// The template has the grain crosswise.
    /// </summary>
    GrainCrosswise = 2,

    /// <summary>
    /// The template has trims on the long sides.
    /// </summary>
    TrimsAllSides = 4,
    
    /// <summary>
    /// The template has trims on the short sides.
    /// </summary>
    TrimsShortSides = 8,

    /// <summary>
    /// The template has no trims.
    /// </summary>
    TrimsNone = 16,
    
    /// <summary>
    /// The template should be divided within the pattern.
    /// </summary>
    DivideWithinPattern = 32,

    /// <summary>
    /// The template    should be divided in  a separate pattern.
    /// </summary>
    DivideInSeparatePattern = 64
}

