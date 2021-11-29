// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers;

public struct ActionWrapper<TBuffer> : IBufferAction<TBuffer>
{
    public Action<TBuffer> Action;

    void IBufferAction<TBuffer>.Action(TBuffer buffer)
    {
        Action(buffer);
    }
}