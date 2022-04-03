using System;
using System.Collections;
using System.Collections.Generic;

namespace Toolbox;

public class RingBuffer<T> : ICollection, IReadOnlyCollection<T>
    where T: class
{
    private readonly Queue<T> _buffer;

    public RingBuffer (int capacity)
    {
        Capacity = capacity;
        _buffer = new Queue<T>(capacity);
    }

    public int Capacity { get; }

    // ReSharper disable once UnusedMember.Global
    public bool Read (out T? value)
    {
        if (_buffer.Count == 0)
        {
            value = default;

            return false;
        }

        value = _buffer.Dequeue();

        return true;
    }

    // ReSharper disable once UnusedMethodReturnValue.Global
    public bool Write(T value)
    {
        if (_buffer.Count == Capacity)
        {
            _buffer.Dequeue();

            _buffer.Enqueue(value);

            return false;
        }

        _buffer.Enqueue(value);

        return true;
    }

    #region Implementation of IEnumerable

    public IEnumerator<T> GetEnumerator () { return _buffer.GetEnumerator(); }

    IEnumerator IEnumerable.GetEnumerator () { return ((IEnumerable)_buffer).GetEnumerator(); }

    #endregion

    #region Implementation of ICollection

    void ICollection.CopyTo
    (
        Array array
        , int   index
    )
    {
        ((ICollection)_buffer).CopyTo(array, index);
    }

    public int Count => _buffer.Count;

    public bool IsSynchronized => ((ICollection)_buffer).IsSynchronized;

    public object SyncRoot => ((ICollection)_buffer).SyncRoot;

    #endregion

    #region Implementation of IReadOnlyCollection<out T>

    int IReadOnlyCollection<T>.Count => _buffer.Count;

    #endregion

    public void Clear () => _buffer.Clear();
}