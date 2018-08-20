// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Modules.Native;

namespace WInterop.Direct3d.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        public const uint D3D11_SDK_VERSION = 7;

        // https://docs.microsoft.com/en-us/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice
        [DllImport(Libraries.D3D11, ExactSpelling = true)]
        public unsafe static extern HRESULT D3D11CreateDevice(
            [MarshalAs(UnmanagedType.IUnknown)] // IDXGIAdapter
            object pAdapter,
            DriverType DriverType,
            HMODULE Software,
            CreateDeviceFlags Flags,
            FeatureLevel *pFeatureLevels,
            uint FeatureLevels,
            uint SDKVersion,
            out IntPtr ppDevice,                // ID3D11Device
            FeatureLevel* pFeatureLevel,
            out IntPtr ppImmediateContext);     // ID3D11DeviceContext
    }
}
