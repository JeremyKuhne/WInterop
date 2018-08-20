// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [DXGI_SWAP_EFFECT]
    /// </summary>
    public enum SwapEffect : uint
    {
        Discard = 0,
        Sequential = 1,
        FlipSequential = 3,
        FlipDiscard = 4
    }
}
