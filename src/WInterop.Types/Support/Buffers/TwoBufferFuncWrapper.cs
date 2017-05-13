// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers
{
    public struct TwoBufferFuncWrapper<TBufferFunc, BufferType, T> : ITwoBufferAction<BufferType> where TBufferFunc : ITwoBufferFunc<BufferType, T>
    {
        public TBufferFunc Func;
        public T Result;

        void ITwoBufferAction<BufferType>.Action(BufferType buffer1, BufferType buffer2)
        {
            Result = Func.Func(buffer1, buffer2);
        }
    }
}
