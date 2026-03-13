using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

/// <summary />
public class GroupWrapper : EntityWrapper
{
    /// <summary />
    public GroupWrapper(Entity entityEntity) : base(entityEntity) { }

    #region Public properties


    /// <summary />
    public string? OrderItemGroup
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
    public string? ArticleGroup
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
    public string? ArticleNumber
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
    public string? ArticleDescription
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
    public string? Description
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
    public int? Quantity
    {
        get
        {
            return Entity.GetPropertyValue<int>();
        }
        set
        {
            Entity.SetPropertyValue(value);
        }
    } 

    #endregion
}