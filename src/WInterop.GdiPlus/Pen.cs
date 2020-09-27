// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus
{
    public sealed class Pen : IDisposable
    {
        private readonly GpPen _gpPen;

        public Pen(GpPen gpPen)
        {
            if (gpPen.Handle == IntPtr.Zero)
                throw new ArgumentNullException(nameof(gpPen));

            _gpPen = gpPen;
        }

        public unsafe Pen(ARGB color, float width = 1.0f, GpUnit unit = GpUnit.UnitWorld)
        {
            GdiPlus.Init();
            Unsafe.SkipInit(out GpPen gpPen);
            GdiPlusImports.GdipCreatePen1(color, width, unit, &gpPen).ThrowIfFailed();
            _gpPen = gpPen;
        }

        public unsafe ARGB Color
        {
            get
            {
                Unsafe.SkipInit(out ARGB color);
                GdiPlusImports.GdipGetPenColor(_gpPen, &color).ThrowIfFailed();
                return color;
            }
            set => GdiPlusImports.GdipSetPenColor(_gpPen, value).ThrowIfFailed();
        }

        public static implicit operator GpPen(Pen pen) => pen._gpPen;

        private void Dispose(bool disposing)
        {
            GpStatus status = GdiPlusImports.GdipDeletePen(_gpPen);
            if (disposing)
            {
                status.ThrowIfFailed();
            }
        }

        ~Pen() => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
