// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [DXGI_USAGE]
    /// </summary>
    [Flags]
    public enum Usage : uint
    {
        ShaderInput            = 0x00000010,
        RenderTargetOutput     = 0x00000020,
        BackBuffer             = 0x00000040,
        Shared                 = 0x00000080,
        ReadOnly               = 0x00000100,
        DiscardOnPresent       = 0x00000200,
        UnorderedAccess        = 0x00000400
    }
}
