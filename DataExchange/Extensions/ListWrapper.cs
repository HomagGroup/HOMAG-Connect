using System.Collections;

using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions;

public abstract  class ListWrapper<T, W> : IList<W> where T : ParamBase
{
    protected List<T> InnerList { get; }

    public ListWrapper(List<T>? innerList)
    {
        if (innerList != null)
        {
            this.InnerList = innerList;
        }
        else
        {
            InnerList = new List<T>();
        }
    }

    #region IList Members

    /// <inheritdoc />
    public abstract IEnumerator<W> GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public abstract void Add(W item);

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(W item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(W[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(W item)
    {
        throw new NotImplementedException();
    }

    public int Count { get; }

    public bool IsReadOnly { get; }

    #endregion

    public int IndexOf(W item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, W item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public W this[int index]
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }
}