// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Types;

namespace WInterop.Gdi.Types
{
    public class PaintDeviceContext : DeviceContext
    {
        private WindowHandle _window;
        private PAINTSTRUCT _paintStruct;

        public PaintDeviceContext(WindowHandle window, PAINTSTRUCT paintStruct, IntPtr handle)
            : base(handle, ownsHandle: true)
        {
            _window = window;
            _paintStruct = paintStruct;
        }

        protected override void Dispose(bool disposing)
        {
            GdiMethods.Imports.EndPaint(_window, ref _paintStruct);
        }
    }
}
