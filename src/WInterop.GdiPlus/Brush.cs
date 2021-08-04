// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus
{
    public class Brush : IDisposable
    {
        private readonly GpBrush _gpBrush;

        public Brush(GpBrush gpBrush)
        {
            if (gpBrush.Handle == 0)
                throw new ArgumentNullException(nameof(gpBrush));

            _gpBrush = gpBrush;
        }

        public unsafe Brush(ARGB color)
        {
            GdiPlus.Init();
            Unsafe.SkipInit(out GpBrush gpBrush);
            GdiPlusImports.GdipCreateSolidFill(color, &gpBrush).ThrowIfFailed();
            _gpBrush = gpBrush;
        }

        public static implicit operator GpBrush(Brush brush) => brush._gpBrush;

        private void Dispose(bool disposing)
        {
            GpStatus status = GdiPlusImports.GdipDeleteBrush(_gpBrush);
            if (disposing)
            {
                status.ThrowIfFailed();
            }
        }

        ~Brush() => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
