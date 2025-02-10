using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions.Wrapper;

/// <summary>
/// Order wrapper class to handle the order properties.
/// </summary>
public class OrderWrapper
{
    #region Private Properties

    internal Order Order { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of the OrderWrapper.
    /// </summary>
    public OrderWrapper()
    {
        Order = new Order();
    }

    /// <summary>
    /// Creates a new instance of the OrderWrapper.
    /// </summary>
    public OrderWrapper(Order order)
    {
        Order = order;
    }

    #endregion

    #region Public properties

    /// <summary />
    public string? OrderName
    {
        get
        {
            return Order.GetPropertyValue<string>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? OrderNumber
    {
        get
        {
            return Order.GetPropertyValue<string>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public DateTime? DeliveryDatePlanned
    {
        get
        {
            return Order.GetPropertyValue<DateTime>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public DateTime? OrderDate
    {
        get
        {
            return Order.GetPropertyValue<DateTime>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? Company
    {
        get
        {
            return Order.GetPropertyValue<string>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? CustomerName
    {
        get
        {
            return Order.GetPropertyValue<string>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? CustomerNumber
    {
        get
        {
            return Order.GetPropertyValue<string>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary />
    public string? OrderDescription
    {
        get
        {
            return Order.GetPropertyValue<string>();
        }
        set
        {
            Order.SetPropertyValue(value);
        }
    }

    /// <summary>
    /// Gets the properties of the order.
    /// </summary>
    public List<Param> Properties
    {
        get
        {
            return Order.Properties;
        }
      }

    /// <summary />
    public IList<ImageWrapper> Images
    {
        get
        {
            return new ImageWrapperList(Order.Images);
        }
    }

    #endregion

    #region Converters

    /// <summary>
    /// Converts the OrderWrapper to Order.
    /// </summary>
    public static implicit operator Order(OrderWrapper orderWrapper)
    {
        return orderWrapper.Order;
    }

    /// <summary>
    /// Converts the Order into a OrderWrapper.
    /// </summary>
    public static implicit operator OrderWrapper(Order order)
    {
        return new OrderWrapper(order);
    }

    #endregion
}