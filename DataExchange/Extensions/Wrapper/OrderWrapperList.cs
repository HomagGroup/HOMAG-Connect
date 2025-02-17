using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Base;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

/// <summary>
/// Order wrapper list.
/// </summary>
public class OrderWrapperList : ListWrapper<Order, OrderWrapper>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderWrapperList" /> class.
    /// </summary>
    public OrderWrapperList(List<Order>? orderList) : base(orderList) { }

    /// <inheritdoc />
    public override IEnumerator<OrderWrapper> GetEnumerator()
    {
        return InnerList.Select(order => new OrderWrapper(order)).GetEnumerator();
    }

    /// <inheritdoc />
    public override void Add(OrderWrapper item)
    {
        InnerList.Add(item.Order);
    }

    /// <inheritdoc />
    public override OrderWrapper this[int index]
    {
        get
        {
            return new OrderWrapper(InnerList[index]);
        }
        set
        {
            throw new NotImplementedException();
        }
    }
}