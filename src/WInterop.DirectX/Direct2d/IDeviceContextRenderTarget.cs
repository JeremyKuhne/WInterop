// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Gdi.Native;

namespace WInterop.Direct2d
{
    public interface IDeviceContextRenderTarget : IRenderTarget
    {
        void BindDC(
            HDC deviceContext,
            Rect subRectangle);
    }

    public static class DeviceContextRenderTargetExtensions
    {
        public static void BindDC(this IDeviceContextRenderTarget renderTarget, DeviceContext deviceContext, Rectangle subRectangle)
        {
            renderTarget.BindDC(deviceContext, subRectangle);
        }
    }
}
