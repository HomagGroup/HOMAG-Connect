#nullable enable
using HomagConnect;
using System;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;

/// <summary>
/// The attribute indicates that higher values are better for evaluation purposes.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class HigherIsBetterAttribute : Attribute { }