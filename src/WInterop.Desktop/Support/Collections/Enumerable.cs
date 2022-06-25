// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;

namespace WInterop.Support.Collections;

/// <summary>
///  Simple enumerable base class.
/// </summary>
public abstract class Enumerable<T> : IEnumerable<T>, IEnumerator<T>
{
    protected T? _current = default;

    public abstract bool MoveNext();
    public virtual void Reset() => throw new NotSupportedException();

    public T Current => _current!;
    object? IEnumerator.Current => Current;

    public IEnumerator<T> GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing) { }
}