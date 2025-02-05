using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions;

public class ImageWrapperList : ListWrapper<Image, ImageWrapper>
{
    public ImageWrapperList(List<Image>? orderList) : base(orderList)
    {

    }

    public override IEnumerator<ImageWrapper> GetEnumerator()
    {
        return InnerList.Select(image => new ImageWrapper(image)).GetEnumerator();
    }

    public override void Add(ImageWrapper image)
    {
        InnerList.Add(image.Image);
    }
}