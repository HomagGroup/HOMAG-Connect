namespace HomagConnect.Base.Contracts.Attributes;

/// <summary>
/// Represents an attribute used to associate an event to real time processing 
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class LiveEventAttribute : Attribute { }