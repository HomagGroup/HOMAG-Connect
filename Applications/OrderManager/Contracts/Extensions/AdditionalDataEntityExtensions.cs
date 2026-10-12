using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.OrderManager.Contracts.Extensions;

/// <summary>
/// Extensions for <see cref="AdditionalDataEntity" />.
/// </summary>
public static class AdditionalDataEntityExtensions
{
    /// <summary>
    /// Gets a preview from the additional data
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Uri? GetPreview(this AdditionalDataEntity a)
    {
        if (a is null)
        {
            return null;
        }

        var thumb = a.Previews?.FirstOrDefault(t => t.Size == AdditionalDataPreviewSize.Small);
        if (!string.IsNullOrEmpty(thumb?.Uri?.AbsoluteUri))
        {
            return thumb.Uri;
        }
        thumb = a.Previews?.FirstOrDefault(t => t.Size == AdditionalDataPreviewSize.Medium);
        if (!string.IsNullOrEmpty(thumb?.Uri?.AbsoluteUri))
        {
            return thumb.Uri;
        }
        thumb = a.Previews?.FirstOrDefault(t => t.Size == AdditionalDataPreviewSize.Large);
        if (!string.IsNullOrEmpty(thumb?.Uri?.AbsoluteUri))
        {
            return thumb.Uri;
        }
        if (!string.IsNullOrEmpty(a.DownloadUri?.AbsoluteUri))
        {
            return a.DownloadUri;
        }
        return null;
    }
}
