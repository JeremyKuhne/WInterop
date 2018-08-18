// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows;

namespace WInterop.Direct2d
{
    public class Direct2dWindowClass : WindowClass
    {
        private bool _resourcesValid;
        private IWindowRenderTarget _renderTarget;

        public unsafe Direct2dWindowClass(
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

        protected static IFactory Factory { get; } = Direct2d.CreateFactory();

        private void CreateResourcesInternal(WindowHandle window)
        {
            if (!_resourcesValid)
            {
                _renderTarget = Factory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, window.GetClientRectangle().Size));
                CreateResources();
                _resourcesValid = true;
            }
        }

        private void CreateResourcesInternal(WindowHandle window, in Message.Size size)
        {
            if (!_resourcesValid)
            {
                _renderTarget = Factory.CreateWindowRenderTarget(
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

        protected sealed override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    CreateResourcesInternal(window, new Message.Size(wParam, lParam));
                    break;
                case MessageType.DisplayChange:
                    window.Invalidate(erase: false);
                    break;
                case MessageType.Paint:
                    CreateResourcesInternal(window);
                    _renderTarget.BeginDraw();
                    OnPaint(window);
                    HRESULT result = _renderTarget.EndDraw();
                    window.Validate();

                    if (result == HRESULT.D2DERR_RECREATE_TARGET)
                    {
                        _resourcesValid = false;
                    }
                    else if (result != HRESULT.S_OK)
                    {
                        throw Error.GetIoExceptionForHResult(result);
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
