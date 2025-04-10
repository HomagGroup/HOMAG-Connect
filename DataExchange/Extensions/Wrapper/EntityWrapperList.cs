using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Base;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

public class EntityWrapperList : ListWrapper<Entity, EntityWrapper>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityWrapperList" /> class.
    /// </summary>
    public EntityWrapperList(List<Entity>? entities) : base(entities) { }

    /// <inheritdoc />
    public override IEnumerator<EntityWrapper> GetEnumerator()
    {
        return InnerList.Select(entity => new EntityWrapper(entity)).GetEnumerator();
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