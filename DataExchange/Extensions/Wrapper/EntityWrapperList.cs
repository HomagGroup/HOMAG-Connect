using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Base;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

/// <inheritdoc />
public class EntityWrapperList : ListWrapper<Entity, EntityWrapper>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityWrapperList" /> class.
    /// </summary>
    public EntityWrapperList(List<Entity>? entities) : base(entities) { }

    /// <inheritdoc />
    public override IEnumerator<EntityWrapper> GetEnumerator()
    {
        return InnerList.Select(entity =>
        {
            var wrapper = new EntityWrapper(entity);

            if (wrapper.Type == "OrderItem")
            {
                return new OrderItemWrapper(entity);
            }

            return wrapper;

        }).GetEnumerator();
    }

    /// <inheritdoc />
    public override void Add(EntityWrapper item)
    {
        InnerList.Add(item.Entity);
    }

    /// <inheritdoc />
    public override EntityWrapper this[int index]
    {
        get
        {
            return new EntityWrapper(InnerList[index]);
        }
        set
        {
            throw new NotImplementedException();
        }
    }
}