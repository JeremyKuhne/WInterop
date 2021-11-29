// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Renders drawing instructions to a window. [ID2D1HwndRenderTarget]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe class WindowRenderTarget : RenderTarget, IWindowRenderTarget
    {
        private readonly ID2D1HwndRenderTarget* _handle;

        internal WindowRenderTarget(ID2D1HwndRenderTarget* handle)
            : base ((ID2D1RenderTarget*)handle)
            => _handle = handle;

        public WindowState CheckWindowState() => (WindowState)_handle->CheckWindowState();

        public void Resize(SizeU pixelSize) => _handle->Resize((D2D_SIZE_U*)&pixelSize).ThrowIfFailed();

        public Windows.Native.HWND GetHwnd() => new(_handle->GetHwnd());
    }
}
