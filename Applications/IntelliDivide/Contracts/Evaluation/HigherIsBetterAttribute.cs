#nullable enable
using HomagConnect;
using System;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// The attribute indicates that higher values are better for evaluation purposes.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class HigherIsBetterAttribute : Attribute { }