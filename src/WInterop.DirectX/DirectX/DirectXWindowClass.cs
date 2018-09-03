// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Direct2d;
using WInterop.DirectWrite;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows;

namespace WInterop.DirectX
{
    public class DirectXWindowClass : WindowClass
    {
        private bool _resourcesValid;
        private IWindowRenderTarget _renderTarget;

        public unsafe DirectXWindowClass(
            string className = default,
            ModuleInstance moduleInstance = default,
            ClassStyle classStyle = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
            IconHandle icon = default,
            IconHandle smallIcon = default,
            CursorHandle cursor = default,
            string menuName = null,
            int menuId = 0,
            int classExtraBytes = 0,
            int windowExtraBytes = 0)
            : base(className, moduleInstance, classStyle, BrushHandle.NoBrush, icon, smallIcon, cursor, menuName, menuId, classExtraBytes, windowExtraBytes)
        {
        }

        protected IRenderTarget RenderTarget => _renderTarget;

        protected static Direct2d.IFactory Direct2dFactory { get; } = Direct2d.Direct2d.CreateFactory();
        protected static DirectWrite.IFactory DirectWriteFactory { get; } = DirectWrite.DirectWrite.CreateFactory();

        private void CreateResourcesInternal(WindowHandle window)
        {
            if (!_resourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, window.GetClientRectangle().Size));
                CreateResources();
                _resourcesValid = true;
            }
        }

        private void CreateResourcesInternal(WindowHandle window, in Message.Size size)
        {
            if (!_resourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, size.NewSize));
                CreateResources();
                _resourcesValid = true;
            }
            else
            {
                _renderTarget.Resize(size.NewSize);
            }
        }

        protected virtual void CreateResources()
        {
        }

        protected virtual void OnPaint(WindowHandle window)
        {
        }

        protected virtual void OnSize(WindowHandle window, in Message.Size sizeMessage)
        {
        }

        protected sealed override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    var sizeMessage = new Message.Size(wParam, lParam);
                    CreateResourcesInternal(window, in sizeMessage);
                    OnSize(window, in sizeMessage);
                    break;
                case MessageType.DisplayChange:
                    window.Invalidate(erase: false);
                    break;
                case MessageType.Paint:
                    CreateResourcesInternal(window);
                    _renderTarget.BeginDraw();
                    OnPaint(window);
                    HResult result = _renderTarget.EndDraw();
                    window.Validate();

                    if (result == HResult.D2DERR_RECREATE_TARGET)
                    {
                        _resourcesValid = false;
                    }
                    else
                    {
                        result.ThrowIfFailed();
                    }
                    break;
            }

            return Direct2dWindowProcedure(window, message, wParam, lParam);
        }

        protected virtual LResult Direct2dWindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            // We handled these above. We still call through to here to allow derived classes to do additional handling of
            // these messages if they care to.

            switch (message)
            {
                case MessageType.Size:
                    return 0;
                case MessageType.DisplayChange:
                    return 0;
                case MessageType.Paint:
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
