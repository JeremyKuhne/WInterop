// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using WInterop.Errors;

namespace WInterop.Com;

public unsafe struct StorageEnumerator : IDisposable
{
    private readonly IEnumSTATSTG* _enumerator;
    public StorageEnumerator(IEnumSTATSTG* enumerator) => _enumerator = enumerator;

    /// <summary>
    ///  Creates a copy of the current enumerator at the current state.
    /// </summary>
    public StorageEnumerator Clone()
    {
        IEnumSTATSTG* copy;
        _enumerator->Clone(&copy).ThrowIfFailed();
        return new(copy);
    }

    /// <summary>
    ///  Try to get information on the next item.
    /// </summary>
    /// <returns>Item information, must be disposed.</returns>
    public bool TryGetNext(out StorageStats next)
    {
        fixed (void* n = &next)
        {
            uint fetched;
            HResult result = _enumerator->Next(1, (STATSTG*)n, &fetched).ToHResult();
            return result == HResult.S_OK;
        }
    }

    /// <summary>
    ///  Get up to the specified <paramref name="count"/> of items.
    /// </summary>
    /// <returns>Collection of items, must be disposed.</returns>
    public StatsCollection Next(uint count)
    {
        StorageStats[] stats = new StorageStats[(int)count];
        fixed (void* s = stats)
        {
            uint fetched;
            _enumerator->Next(count, (STATSTG*)s, &fetched);

            return fetched == 0
                ? StatsCollection.Empty
                : new StatsCollection(new ArraySegment<StorageStats>(stats, 0, (int)fetched));
        }
    }

    /// <summary>
    ///  Resets back to the beginning.
    /// </summary>
    public void Reset() => _enumerator->Reset().ThrowIfFailed();

    /// <summary>
    ///  Skip the specified number of entries.
    /// </summary>
    public void Skip(uint count) => _enumerator->Skip(count).ThrowIfFailed();

    public void Dispose() => _enumerator->Release();

    public sealed class StatsCollection : ReadOnlyCollection<StorageStats>, IDisposable
    {
        public static StatsCollection Empty { get; } = new(Array.Empty<StorageStats>());

        public StatsCollection(IList<StorageStats> list)
            : base(list)
        { }

        public void Dispose()
        {
            foreach (var item in Items)
            {
                item.Dispose();
            }
        }
    }
}