// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.Support.Buffers
{
    public interface IBufferAction<BufferType>
    {
        void Action(BufferType buffer);
    }
}
