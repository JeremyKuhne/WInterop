// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Windows;
using WInterop.Windows.Unsafe;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Contains the HWND, pixel size, and presentation options for an
    /// ID2D1HwndRenderTarget. [D2D1_HWND_RENDER_TARGET_PROPERTIES]
    /// </summary>
    public readonly struct WindowRenderTargetProperties
    {
        private readonly HWND hwnd;
        public readonly SizeU PixelSize;
        public readonly PresentOptions PresentOptions;

        public WindowRenderTargetProperties(WindowHandle window, Size pixelSize, PresentOptions options = PresentOptions.None)
        {
            hwnd = window;
            PixelSize = pixelSize;
            PresentOptions = options;
        }
    }
}
