// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct3d
{
    /// <summary>
    /// [D3D11_CREATE_DEVICE_FLAG]
    /// </summary>
    [Flags]
    public enum CreateDeviceFlags : uint
    {
        SingleThreaded = 0x1,
        Debug = 0x2,
        SwitchToRef = 0x4,
        PreventInternalThreadingOptimizations = 0x8,
        BgraSupport = 0x20,
        Debuggable = 0x40,
        PreventAlteringLayerSettingsFromRegistry = 0x80,
        DisableGpuTimeout = 0x100,
        VideoSupport = 0x800
    }
}
