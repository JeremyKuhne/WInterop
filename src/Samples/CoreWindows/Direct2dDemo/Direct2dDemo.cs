// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Direct2d;
using WInterop.Direct3d;
using WInterop.DirectX;
using WInterop.Errors;
using WInterop.GraphicsInfrastructure;
using WInterop.Windows;

namespace Direct2dDemo
{
    // https://docs.microsoft.com/en-us/windows/desktop/Direct2D/direct2d-quickstart
    public class Direct2dDemo : DirectXWindowClass
    {
        private ISolidColorBrush _lightSlateGrayBrush;
        private ISolidColorBrush _cornflowerBlueBrush;

        protected override void CreateResources(WindowHandle window)
        {
            Direct3d.CreateDirect2dCompatibleDevice(out object d3d11Device, out IntPtr deviceContext);
            var dxgiDevice = (WInterop.GraphicsInfrastructure.IDevice1)d3d11Device;
            WInterop.Direct2d.IDevice device = Direct2dFactory.CreateDevice(dxgiDevice);

            SwapChainDescriptor1 descriptor = new SwapChainDescriptor1
            {
                Format = Format.B8G8R8A8_UNORM,                     // Most common swapchain format
                SampleDescriptor = new SampleDescriptor(count: 1),  // No multisampling
                BufferUsage = Usage.RenderTargetOutput,
                BufferCount = 2,                                    // Use double buffering to enable flip
                Scaling = Scaling.None,
                SwapEffect = SwapEffect.FlipSequential              // Required for all apps
            };

            IObject adapter = dxgiDevice.GetAdapter();
            IFactory2 factory = (IFactory2)adapter.GetParent(new Guid(WInterop.GraphicsInfrastructure.InterfaceIds.IID_IDXGIFactory2));
            bool isCurrent = factory.IsCurrent();
            ISwapChain1 swapChain = factory.CreateSwapChainForHwnd(dxgiDevice, window, in descriptor);
            dxgiDevice.SetMaximumFrameLatency(1);

            ISurface backBuffer = (ISurface)swapChain.GetBuffer(0, new Guid(WInterop.GraphicsInfrastructure.InterfaceIds.IID_IDXGISurface));
            BitmapProperties1 properties = new BitmapProperties1(
                new PixelFormat(Format.B8G8R8A8_UNORM, AlphaMode.Ignore),
                96,
                96,
                BitmapOptions.Target | BitmapOptions.CannotDraw);

            IDeviceContext context = device.CreateDeviceContext();
            IBitmap targetBitmap = context.CreateBitmapFromDxgiSurface(backBuffer, properties);
            context.SetTarget(targetBitmap);

            _lightSlateGrayBrush = RenderTarget.CreateSolidColorBrush(Color.LightSlateGray);
            _cornflowerBlueBrush = RenderTarget.CreateSolidColorBrush(Color.CornflowerBlue);
        }

        protected override void OnPaint(WindowHandle window)
        {
            RenderTarget.SetTransform();
            RenderTarget.Clear(Color.White);
            Size size = RenderTarget.GetSize().ToSize();

            _lightSlateGrayBrush.GetColor(out ColorF color);

            for (int x = 0; x < size.Width; x += 10)
            {
                RenderTarget.DrawLine(
                    new Point(x, 0), new Point(x, size.Height),
                    _lightSlateGrayBrush, 0.5f);
            }

            for (int y = 0; y < size.Height; y += 10)
            {
                RenderTarget.DrawLine(
                    new Point(0, y), new Point(size.Width, y),
                    _lightSlateGrayBrush, 0.5f);
            }

            Rectangle rectangle1 = Rectangle.FromLTRB(
                size.Width / 2 - 50,
                size.Height / 2 - 50,
                size.Width / 2 + 50,
                size.Height / 2 + 50);

            Rectangle rectangle2 = Rectangle.FromLTRB(
                size.Width / 2 - 100,
                size.Height / 2 - 100,
                size.Width / 2 + 100,
                size.Height / 2 + 100);

            RenderTarget.FillRectangle(rectangle1, _lightSlateGrayBrush);
            RenderTarget.DrawRectangle(rectangle2, _cornflowerBlueBrush);
        }
    }
}
