using System.Collections;

using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions.Base;

/// <summary>
/// List wrapper.
/// </summary>
public abstract class ListWrapper<T, TW> : IList<TW> where T : ParamBase
{
    /// <summary />
    protected List<T> InnerList { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListWrapper{T, TW}" /> class.
    /// </summary>
    /// <param name="innerList"></param>
    protected ListWrapper(List<T>? innerList)
    {
        if (innerList != null)
        {
            InnerList = innerList;
        }
        else
        {
            InnerList = new List<T>();
        }
    }

    #region IList Members

    /// <inheritdoc />
    public abstract IEnumerator<TW> GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public abstract void Add(TW item);

    /// <inheritdoc />
    public void Clear()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool Contains(TW item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void CopyTo(TW[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool Remove(TW item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int Count
    {
        get
        {
            return InnerList.Count;
        }
    }

    /// <inheritdoc />
    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }

    /// <inheritdoc />
    public int IndexOf(TW item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Insert(int index, TW item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public abstract TW this[int index] { get; set; }

    #endregion
}