#nullable enable

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Events;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when an optimization process has been successfully completed.
/// </summary>
[AppEvent(nameof(IntelliDivide), nameof(OptimizationOptimizedEvent))]
public class OptimizationOptimizedEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the optimization that has been completed.
    /// </summary>
    [JsonProperty(Order = 10)]
    public Optimization Optimization { get; set; }
}