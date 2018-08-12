// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Direct2d.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://docs.microsoft.com/en-us/windows/desktop/api/d2d1/nf-d2d1-d2d1createfactory
        [DllImport(Libraries.D2Dd1, ExactSpelling = true)]
        public static extern HRESULT D2D1CreateFactory(
            FactoryType factoryType,
            ref Guid riid,
            in DebugLevel pFactoryOptions,
            out IFactory ppIFactory);
    }
}
