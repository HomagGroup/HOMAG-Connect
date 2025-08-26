using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Events;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when a solution has been transferred.
/// </summary>
[AppEvent(_Provider, _Key)]
public class SolutionTransferredEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the name of the algorithm used for the solution.
    /// </summary>
    public string AlgorithmName { get; set; }

    /// <summary>
    /// Gets or sets the settings of the algorithm used for the solution.
    /// </summary>
    public string AlgorithmSettings { get; set; }

    /// <summary>
    /// Gets or sets the solution details which have been transferred.
    /// </summary>
    public SolutionDetails SolutionDetails { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who transferred the solution.
    /// </summary>
    public string TransferredBy { get; set; }

    #region Constants

    private const string _Key = "SolutionTransferred";
    private const string _Provider = "intelliDivide";

    #endregion

    #region Overrides

    /// <inheritdoc />
    public override string Provider
    {
        get
        {
            return _Provider;
        }
    }

    /// <inheritdoc />
    public override string Key
    {
        get
        {
            return _Key;
        }
    }

    #endregion
}