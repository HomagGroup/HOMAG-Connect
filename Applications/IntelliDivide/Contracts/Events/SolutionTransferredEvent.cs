#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Events;
using HomagConnect.IntelliDivide.Contracts.Result;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Events;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when a solution has been transferred.
/// </summary>
[AppEvent("SolutionTransferred")]
public class SolutionTransferredEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the name of the algorithm used for the solution.
    /// </summary>
    [JsonProperty(Order = 10)]
    [Required]
    public string AlgorithmName { get; set; }

    /// <summary>
    /// Gets or sets the settings of the algorithm used for the solution.
    /// </summary>
    [JsonProperty(Order = 11)]
    [Required]
    public string AlgorithmSettings { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who transferred the solution.
    /// </summary>
    [JsonProperty(Order = 12)]
    [Required]
    public string TransferredBy { get; set; }

    /// <summary>
    /// Gets or sets the name of the displayed machine.
    /// </summary>
    [JsonProperty(Order = 13)]
    [Required]
    public string MachineDisplayName { get; set; }

    /// <summary>
    /// Gets or sets the machine number.
    /// </summary>
    [JsonProperty(Order = 14)]
    public string? MachineNumber { get; set; }

    /// <summary>
    /// Gets or sets the type of the machine: Cnc, Cutting, etc.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Required]
    public MachineType MachineType { get; set; }

    /// <summary>
    /// Gets or sets the source URI.
    /// </summary>
    [JsonProperty(Order = 16)]
    [Required]
    public Uri SolutionExportUri { get; set; }

    /// <summary>
    /// Gets or sets the solution details which have been transferred.
    /// </summary>
    [JsonProperty(Order = 17)]
    [Required]
    public SolutionDetails SolutionDetails { get; set; }
}