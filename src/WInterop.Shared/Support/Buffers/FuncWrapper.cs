// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers
{
    public struct FuncWrapper<BufferType, T> : IBufferFunc<BufferType, T>
    {
        public Func<BufferType, T> Func;

        T IBufferFunc<BufferType, T>.Func(BufferType buffer)
        {
            return Func(buffer);
        }
    }
}
