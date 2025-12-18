namespace HomagConnect.Base.Contracts.AdditionalData;

/// <summary>
/// Additional data surface texture.
/// </summary>
public class AdditionalDataSurfaceTexture : AdditionalDataEntity
{
    /// <inheritdoc />
    public override AdditionalDataType Type { get; set; } = AdditionalDataType.SurfaceTexture;
}