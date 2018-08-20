// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using WInterop.Direct3d.Native;
using WInterop.Errors;

namespace WInterop.Direct3d
{
    public static class Direct3d
    {
        public unsafe static void CreateDirect2dCompatibleDevice(out IntPtr device, out IntPtr deviceContext)
        {
            FeatureLevel level;

            HRESULT result = Imports.D3D11CreateDevice(
                pAdapter: null,                     // default adapter
                DriverType.Hardware,
                Software: default,                  // no module for a software rasterizer
                CreateDeviceFlags.BgraSupport,      // needed for D2D compatibility
                pFeatureLevels: null,               // default feature levels (9.1 - 11.0)
                FeatureLevels: 0,
                SDKVersion: Imports.D3D11_SDK_VERSION,
                out device,
                &level,
                out deviceContext);
        }
    }
}
