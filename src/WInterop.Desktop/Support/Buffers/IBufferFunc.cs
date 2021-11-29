// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers;

public interface IBufferFunc<TBuffer, T>
{
    T Func(TBuffer buffer);
}