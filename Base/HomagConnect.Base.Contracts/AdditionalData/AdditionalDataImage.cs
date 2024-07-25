namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Additional data image.
/// </summary>
public class AdditionalDataImage : AdditionalDataEntity
{
    /// <summary>
    /// Gets or sets the additional data category
    /// </summary>
    public string? Category { get; set; }

    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Image;
}