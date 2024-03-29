﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using WInterop.Direct2d;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows;

namespace WInterop.DirectX;

public class DirectXWindowClass : WindowClass
{
    private bool _resourcesValid;

    public unsafe DirectXWindowClass(
        string? className = default,
        ModuleInstance? moduleInstance = default,
        ClassStyle classStyle = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
        IconHandle icon = default,
        IconHandle smallIcon = default,
        CursorHandle cursor = default,
        string? menuName = null,
        int menuId = 0,
        int classExtraBytes = 0,
        int windowExtraBytes = 0)
        : base(
              className,
              moduleInstance,
              classStyle,
              BrushHandle.NoBrush,
              icon,
              smallIcon,
              cursor,
              menuName,
              menuId,
              classExtraBytes,
              windowExtraBytes)
    {
    }

    [AllowNull]
    protected IWindowRenderTarget RenderTarget { get; private set; }

    protected static Factory Direct2dFactory { get; } = Direct2d.Direct2d.CreateFactory();
    protected static DirectWrite.WriteFactory DirectWriteFactory { get; } = DirectWrite.DirectWrite.CreateFactory();

    [MemberNotNull(nameof(RenderTarget))]
    private void CreateResourcesInternal(WindowHandle window)
    {
        if (!_resourcesValid || RenderTarget is null)
        {
            RenderTarget = Direct2dFactory.CreateWindowRenderTarget(
                default, new WindowRenderTargetProperties(window, window.GetClientRectangle().Size));
            CreateResources();
            _resourcesValid = true;
        }
    }

    [MemberNotNull(nameof(RenderTarget))]
    private void CreateResourcesInternal(WindowHandle window, in Message.Size size)
    {
        if (!_resourcesValid || RenderTarget is null)
        {
            RenderTarget = Direct2dFactory.CreateWindowRenderTarget(
                default, new WindowRenderTargetProperties(window, size.NewSize));
            CreateResources();
            _resourcesValid = true;
        }
        else
        {
            RenderTarget.Resize(size.NewSize);
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
                RenderTarget.BeginDraw();
                OnPaint(window);
                RenderTarget.EndDraw(out bool recreateTarget);
                window.Validate();

                if (recreateTarget)
                {
                    _resourcesValid = false;
                }

                break;
        }

        // Ensure that the RenderTarget is always valid.
        CreateResourcesInternal(window);
        return Direct2dWindowProcedure(window, message, wParam, lParam);
    }

    protected virtual LResult Direct2dWindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        // We handled these above. We still call through to here to allow derived classes to do additional handling of
        // these messages if they care to.

        switch (message)
        {
            case MessageType.Size:
            case MessageType.DisplayChange:
            case MessageType.Paint:
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }

    protected override void Dispose(bool disposing)
    {
        RenderTarget.Dispose();
        _resourcesValid = false;
        base.Dispose(disposing);
    }
}
