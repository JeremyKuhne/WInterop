// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers
{
    public struct BufferFuncWrapper<TBufferFunc, TBuffer, T>
        : IBufferAction<TBuffer> where TBufferFunc : IBufferFunc<TBuffer, T>
    {
        public TBufferFunc Func;
        public T Result;

        void IBufferAction<TBuffer>.Action(TBuffer buffer)
        {
            Result = Func.Func(buffer);
        }
    }
}
