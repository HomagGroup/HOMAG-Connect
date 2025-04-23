namespace HomagConnect.Base.Contracts.AdditionalData;

/// <summary>
/// Additional data pdf.
/// </summary>
#pragma warning disable S101
public class AdditionalDataPdf : AdditionalDataEntity
#pragma warning restore S101

{
    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.Pdf;
}