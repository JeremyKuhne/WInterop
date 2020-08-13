// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using WInterop.Gdi;
using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus
{
    public class Graphics : IDisposable
    {
        private readonly GpGraphics _gpGraphics;

        public unsafe Graphics(DeviceContext deviceContext)
        {
            GdiPlus.Init();
            Unsafe.SkipInit(out GpGraphics gpGraphics);
            Imports.GdipCreateFromHDC(deviceContext, &gpGraphics).ThrowIfFailed();
            _gpGraphics = gpGraphics;
        }

        public static implicit operator GpGraphics(Graphics graphics) => graphics._gpGraphics;

        private void Dispose(bool disposing)
        {
            Imports.GdipDeleteGraphics(_gpGraphics).ThrowIfFailed();
        }

        ~Graphics() => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
