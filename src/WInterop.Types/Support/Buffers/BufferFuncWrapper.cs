// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers
{
    public struct BufferFuncWrapper<TBufferFunc, BufferType, T> : IBufferAction<BufferType> where TBufferFunc : IBufferFunc<BufferType, T>
    {
        public TBufferFunc Func;
        public T Result;

        void IBufferAction<BufferType>.Action(BufferType buffer)
        {
            Result = Func.Func(buffer);
        }
    }
}
