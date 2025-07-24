using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

public class EntityWrapper
{
    #region Private Properties

    /// <summary />

    internal Entity Entity { get; }

    #endregion

    #region Public Properties
    
    /// <summary />
    public string? Barcode
    {
        get
        {
            return Entity.GetPropertyValue<string>();
        }
        set
        {
            Entity.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? Type
    {
        get
        {
            return Entity.GetPropertyValue<string>();
        }
        set
        {
            Entity.SetPropertyValue(value);
        }
    }


    /// <summary />
    public IList<EntityWrapper> Entities
    {
        get
        {
            return new EntityWrapperList(Entity.Entities);
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the ImageWrapper.
    /// </summary>
    public EntityWrapper()
    {
        Entity = new Entity();
    }

    /// <summary>
    /// Creates a new instance of the ImageWrapper.
    /// </summary>
    public EntityWrapper(Entity entity)
    {
        Entity = entity;
    }

    #endregion
}