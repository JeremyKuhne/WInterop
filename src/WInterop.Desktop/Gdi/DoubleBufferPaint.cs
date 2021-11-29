// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Windows;

namespace WInterop.Gdi;

public ref struct DoubleBufferPaint
{
    private readonly DeviceContext _originalDC;
    private readonly BitmapHandle _bitmap;
    private Rectangle _client;

    public DeviceContext DeviceContext { get; private set; }

    public DoubleBufferPaint(WindowHandle window)
    {
        _client = window.GetClientRectangle();
        _originalDC = window.BeginPaint();
        DeviceContext = _originalDC.CreateCompatibleDeviceContext();
        _bitmap = DeviceContext.CreateCompatibleBitmap(_client.Size);
        DeviceContext.SelectObject(_bitmap);
    }

    public void Dispose()
    {
        try
        {
            DeviceContext.BitBlit(_originalDC, default, new Rectangle(default, _client.Size), new RasterOperation(RasterOperation.Common.SourceCopy));
        }
        finally
        {
            DeviceContext.Dispose();
            _bitmap.Dispose();
            _originalDC.Dispose();
        }
    }
}