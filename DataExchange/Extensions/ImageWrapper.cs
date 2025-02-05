using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions;

/// <summary>
/// Order wrapper class to handle the image properties.
/// </summary>
public class ImageWrapper
{
    #region Private Properties

    /// <summary />

    internal Image Image { get; }

    #endregion

    #region Public Properties

    /// <summary />
    public string? Category
    {
        get
        {
            return Image.GetPropertyValue<string>();
        }
        set
        {
            Image.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? Description
    {
        get
        {
            return Image.GetPropertyValue<string>();
        }
        set
        {
            Image.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? OriginalFileName
    {
        get
        {
            return Image.GetPropertyValue<string>();
        }
        set
        {
            Image.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? ImageLinkPicture
    {
        get
        {
            return Image.GetPropertyValue<string>();
        }
        set
        {
            Image.SetPropertyValue(value);
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the ImageWrapper.
    /// </summary>
    public ImageWrapper()
    {
        Image = new Image();
    }

    /// <summary>
    /// Creates a new instance of the ImageWrapper.
    /// </summary>
    public ImageWrapper(Image image)
    {
        Image = image;
    }

    #endregion
}