namespace HomagConnect.Base.TestBase.Attributes;

/// <summary>
/// Defines priority levels for categorizing tests.
/// </summary>
public enum TestPriority
{
    /// <summary>
    /// No explicit priority is assigned.
    /// </summary>
    Undefined,

    /// <summary>
    /// Indicates a critical test.
    /// </summary>
    Critical,

    /// <summary>
    /// Indicates a high-priority test.
    /// </summary>
    High,

    /// <summary>
    /// Indicates a medium-priority test.
    /// </summary>
    Medium,

    /// <summary>
    /// Indicates a low-priority test.
    /// </summary>
    Low
}