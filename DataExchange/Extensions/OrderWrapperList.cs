using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions;


public class OrderWrapperList : ListWrapper<Order, OrderWrapper>
{
    public OrderWrapperList(List<Order>? orderList) : base(orderList)
    {

    }

    public override IEnumerator<OrderWrapper> GetEnumerator()
    {
        return InnerList.Select(order => new OrderWrapper(order)).GetEnumerator();
    }

    public override void Add(OrderWrapper item)
    {
        InnerList.Add(item.Order);
    }
}