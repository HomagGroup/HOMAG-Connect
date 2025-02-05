using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Base;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

/// <summary>
/// Image wrapper list.
/// </summary>
public class ImageWrapperList : ListWrapper<Image, ImageWrapper>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageWrapperList" /> class.
    /// </summary>
    public ImageWrapperList(List<Image>? imageList) : base(imageList) { }

    /// <inheritdoc />
    public override IEnumerator<ImageWrapper> GetEnumerator()
    {
        return InnerList.Select(image => new ImageWrapper(image)).GetEnumerator();
    }

    /// <inheritdoc />
    public override void Add(ImageWrapper image)
    {
        InnerList.Add(image.Image);
    }
}