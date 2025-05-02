using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

/// <summary />
public class OrderItemWrapper : EntityWrapper
{
    /// <summary />
    public OrderItemWrapper(Entity entityEntity) : base(entityEntity) { }

    #region Public properties

    /// <summary />
    public DateTime? StartDatePlanned
    {
        get
        {
            return Entity.GetPropertyValue<DateTime>();
        }
        set
        {
            Entity.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? OrderItemNumber
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
    public double? Thickness
    {
        get
        {
            return Entity.GetPropertyValue<double>();
        }
        set
        {
            Entity.SetPropertyValue(value);
        }
    }

    /// <summary />
    public double? Width
    {
        get
        {
            return Entity.GetPropertyValue<double>();
        }
        set
        {
            Entity.SetPropertyValue(value);
        }
    }

    /// <summary />
    public double? Length
    {
        get
        {
            return Entity.GetPropertyValue<double>();
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

    /// <summary />
    public string? Catalog
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
    public string? ProcurementType
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

    #endregion
}