namespace HomagConnect.Base.Contracts.AdditionalData;

/// <summary>
/// Additional data decor.
/// </summary>
public class AdditionalDataDecor : AdditionalDataEntity
{
    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Decor;
}