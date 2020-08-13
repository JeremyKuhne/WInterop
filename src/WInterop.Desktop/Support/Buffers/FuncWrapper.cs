// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers
{
    public struct FuncWrapper<TBuffer, T> : IBufferFunc<TBuffer, T>
    {
        public Func<TBuffer, T> Func;

        T IBufferFunc<TBuffer, T>.Func(TBuffer buffer)
        {
            return Func(buffer);
        }
    }
}
