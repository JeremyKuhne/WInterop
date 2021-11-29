// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    public interface IWindowRenderTarget : IRenderTarget
    {
        WindowState CheckWindowState();

        /// <summary>
        ///  Resize the buffer underlying the render target. This operation might fail if
        ///  there is insufficient video memory or system memory, or if the render target is
        ///  resized beyond the maximum bitmap size. If the method fails, the render target
        ///  will be placed in a zombie state and D2DERR_RECREATE_TARGET will be returned
        ///  from it when EndDraw is called. In addition an appropriate failure result will
        ///  be returned from Resize.
        /// </summary>
        void Resize(SizeU pixelSize);

        Windows.Native.HWND GetHwnd();
    }
}
