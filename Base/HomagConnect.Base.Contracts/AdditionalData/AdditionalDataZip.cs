namespace HomagConnect.Base.Contracts.AdditionalData;

/// <summary>
/// Additional data zip.
/// </summary>
#pragma warning disable S101
public class AdditionalDataZip : AdditionalDataEntity
#pragma warning restore S101

{
    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Zip;
}