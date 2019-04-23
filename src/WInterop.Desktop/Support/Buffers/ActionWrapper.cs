// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers
{
    public struct ActionWrapper<BufferType> : IBufferAction<BufferType>
    {
        public Action<BufferType> Action;

        void IBufferAction<BufferType>.Action(BufferType buffer)
        {
            Action(buffer);
        }
    }
}
