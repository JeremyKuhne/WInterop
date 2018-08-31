// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.GdiPlus.Unsafe;

namespace WInterop.GdiPlus
{
    public struct GpGraphics : IDisposable
    {
        public IntPtr Handle;

        public void Dispose()
        {
            GdiPlusMethods.ThrowIfFailed(Imports.GdipDeleteGraphics(Handle));
        }
    }
}
