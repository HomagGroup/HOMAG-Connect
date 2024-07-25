namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Additional data CNC program.
/// </summary>
#pragma warning disable S101
public class AdditionalDataCNCProgram : AdditionalDataEntity
#pragma warning restore S101

{
    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Image;
}