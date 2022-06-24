// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers;

public ref struct StringBufferCacheScope
{
    private readonly StringBufferCache _cache;
    private StringBuffer _buffer;

    public StringBufferCacheScope(StringBufferCache cache, StringBuffer buffer)
    {
        _cache = cache;
        _buffer = buffer;
    }

    public StringBuffer Buffer => _buffer;

    public unsafe void Dispose()
    {
        _cache.Release(Buffer);
        _buffer = null!;
    }
}