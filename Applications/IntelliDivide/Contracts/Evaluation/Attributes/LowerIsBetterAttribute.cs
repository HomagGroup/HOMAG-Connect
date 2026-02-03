#nullable enable
using HomagConnect;
using System;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;

/// <summary>
/// The attribute indicates that lower values are better for evaluation purposes.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal class LowerIsBetterAttribute : Attribute { }