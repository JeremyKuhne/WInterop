// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers
{
    public interface ITwoBufferAction<TBuffer>
    {
        void Action(TBuffer buffer1, TBuffer buffer2);
    }
}