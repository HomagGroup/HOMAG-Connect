namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Additional data CNC program.
/// </summary>
public class AdditionalDataCNCProgram : AdditionalDataEntity
{
    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Image;
}