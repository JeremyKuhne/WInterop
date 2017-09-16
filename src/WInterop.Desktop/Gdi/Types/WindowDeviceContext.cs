// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Types;

namespace WInterop.Gdi.Types
{
    public class WindowDeviceContext : DeviceContext
    {
        private WindowHandle _window;

        public WindowDeviceContext(WindowHandle window, IntPtr handle)
            : base(handle, ownsHandle: true)
        {
            _window = window;
        }

        protected override void Dispose(bool disposing)
        {
            GdiMethods.Imports.ReleaseDC(_window, handle);
        }
    }
}
