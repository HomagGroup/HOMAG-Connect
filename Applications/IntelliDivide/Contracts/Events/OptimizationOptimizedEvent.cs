using HomagConnect.Base.Contracts.Events;

namespace HomagConnect.IntelliDivide.Contracts.Events;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when an optimization process has been successfully completed.
/// </summary>
[AppEvent(_Provider, _Key)]
public class OptimizationOptimizedEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the optimization that has been completed.
    /// </summary>
    public Optimization Optimization { get; set; }

    #region Constants

    private const string _Key = "OptimizationOptimized";
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