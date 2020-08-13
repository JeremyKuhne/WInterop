// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers
{
    public struct TwoBufferFuncWrapper<TBuffer, T> : ITwoBufferAction<TBuffer>
    {
        public Func<TBuffer, TBuffer, T> Func;
        public T Result;

        void ITwoBufferAction<TBuffer>.Action(TBuffer buffer1, TBuffer buffer2)
        {
            Result = Func(buffer1, buffer2);
        }
    }

    public struct TwoBufferFuncWrapper<TBufferFunc, TBuffer, T>
        : ITwoBufferAction<TBuffer> where TBufferFunc : ITwoBufferFunc<TBuffer, T>
    {
        public TBufferFunc Func;
        public T Result;

        void ITwoBufferAction<TBuffer>.Action(TBuffer buffer1, TBuffer buffer2)
        {
            Result = Func.Func(buffer1, buffer2);
        }
    }
}
