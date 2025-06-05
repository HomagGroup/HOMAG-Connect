using HomagConnect.Base.Contracts.Attributes;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Defines the base unit of a property.
/// </summary>
public enum BaseUnit
{
    /// <summary />
    [RoundingFormat(1, 3)]
    Millimeter,

    /// <summary />
    [RoundingFormat(2, 2)]
    SquareMeter,

    /// <summary />
    [RoundingFormat(2, 2)]
    Meter,

    /// <summary />
    [RoundingFormat(2, 1)]
    Bar,

    /// <summary />
    [RoundingFormat(1, 1)]
    MeterPerSecond
}