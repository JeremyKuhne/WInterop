// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Direct2d;
using WInterop.Gdi;
using WInterop.Windows;
using Xunit;
using FluentAssertions;
using System.Numerics;

namespace Direct2dTests
{
    public class RenderTarget : IClassFixture<RenderTarget.RenderTargetFixture>
    {
        private readonly RenderTargetFixture _fixture;
        public RenderTarget(RenderTargetFixture fixture)
        {
            _fixture = fixture;
        }

        public class RenderTargetFixture : IDisposable
        {
            private readonly IFactory _factory;
            // private DeviceContext _dc;
            private readonly WindowHandle _window;
            private readonly WindowClass _windowClass;

            public RenderTargetFixture()
            {
                _factory = Direct2d.CreateFactory(FactoryType.SingleThreaded, DebugLevel.None);

                // Create a memory only copy of the primary monitor DC
                // _dc = Gdi.CreateCompatibleDeviceContext(Gdi.GetDeviceContext());

                _windowClass = new WindowClass(backgroundBrush: BrushHandle.NoBrush);
                _windowClass.Register();
                _window = _windowClass.CreateWindow("RenderTargetTest");
                RenderTarget = _factory.CreateWindowRenderTarget(default,
                    new WindowRenderTargetProperties(_window, _window.GetClientRectangle().Size));
            }

            public void Dispose()
            {
                // Nothing
            }

            public IRenderTarget RenderTarget { get; private set; }
        }

        [Fact]
        public void GetFactory()
        {
            _fixture.RenderTarget.GetFactory(out IFactory factory);
            factory.Should().NotBeNull();
        }

        [Fact]
        public void CreateBitmap()
        {
            IBitmap bitmap = _fixture.RenderTarget.CreateBitmap(new Size(100, 100),
                new BitmapProperties(new PixelFormat(WInterop.Dxgi.Format.DXGI_FORMAT_R32G32B32A32_FLOAT, AlphaMode.Ignore), 72, 72));

            bitmap.Should().NotBeNull();
        }

        [Fact]
        public unsafe void CreateBitmapBrush()
        {
            IBitmap bitmap = _fixture.RenderTarget.CreateBitmap(new Size(10, 10),
                new BitmapProperties(new PixelFormat(WInterop.Dxgi.Format.DXGI_FORMAT_R32G32B32A32_FLOAT, AlphaMode.Ignore), 72, 72));

            IBitmapBrush brush = _fixture.RenderTarget.CreateBitmapBrush(bitmap, null, null);
            brush.Should().NotBeNull();
        }

        [Fact]
        public unsafe void CreateSolidColorBrush()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.Bisque);
            brush.Should().NotBeNull();
            brush.GetColor(out ColorF color);
            ((Color)color).ToArgb().Should().Be(Color.Bisque.ToArgb());
        }

        [Fact]
        public unsafe void DrawLine()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.Maroon);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.DrawLine(default, new Point(10, 10), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void DrawRectangle()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.Gold);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.DrawRectangle(new Rectangle(0, 0, 10, 10), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void FillRectangle()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.Blue);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.FillRectangle(new Rectangle(0, 0, 10, 10), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void DrawRoundedRectangle()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.White);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.DrawRoundedRectangle(new RoundedRectangle(new Rectangle(0, 0, 10, 10), 3, 3), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void FillRoundedRectangle()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.BurlyWood);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.FillRoundedRectangle(new RoundedRectangle(new Rectangle(0, 0, 10, 10), 3, 3), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void DrawEllipse()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.Chocolate);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.DrawEllipse(new Ellipse(new Point(5,5), 6, 7), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void FillEllipse()
        {
            ISolidColorBrush brush = _fixture.RenderTarget.CreateSolidColorBrush(Color.Honeydew);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.FillEllipse(new Ellipse(new Point(5, 5), 6, 7), brush);
            _fixture.RenderTarget.EndDraw(out _, out _);
        }

        [Fact]
        public unsafe void Tags()
        {
            _fixture.RenderTarget.SetTags(2001, 2010);
            _fixture.RenderTarget.GetTags(out ulong tag1, out ulong tag2);
            tag1.Should().Be(2001);
            tag2.Should().Be(2010);
            _fixture.RenderTarget.BeginDraw();
            _fixture.RenderTarget.EndDraw(out tag2, out tag1);

            // These are 0 if there are no errors, a value will tell you the tag state when
            // a drawing operation failed.
            tag2.Should().Be(0);
            tag1.Should().Be(0);
        }

        [Fact]
        public unsafe void Dpi()
        {
            _fixture.RenderTarget.SetDpi(72.0f, 72.0f);
            _fixture.RenderTarget.GetDpi(out float dpiX, out float dpiY);
            dpiX.Should().Be(72.0f);
            dpiY.Should().Be(72.0f);
        }

        [Fact]
        public void GetSize()
        {
            _fixture.RenderTarget.GetSize(out SizeF size);
            size.Width.Should().BeGreaterThan(0);
        }

        [Fact]
        public void GetPixelSize()
        {
            _fixture.RenderTarget.GetPixelSize(out SizeU size);
            size.Width.Should().BeGreaterThan(0);
        }

        [Fact]
        public void GetPixelFormat()
        {
            _fixture.RenderTarget.GetPixelFormat(out PixelFormat pixelFormat);
            // BGRA
            pixelFormat.Format.Should().Be(WInterop.Dxgi.Format.DXGI_FORMAT_B8G8R8A8_UNORM);
        }

        [Fact]
        public void AntialiasModes()
        {
            AntialiasMode existing = _fixture.RenderTarget.GetAntialiasMode();
            _fixture.RenderTarget.SetAntialiasMode(AntialiasMode.Aliased);
            _fixture.RenderTarget.GetAntialiasMode().Should().Be(AntialiasMode.Aliased);
            _fixture.RenderTarget.SetAntialiasMode(existing);
        }

        [Fact]
        public void Transforms()
        {
            _fixture.RenderTarget.GetTransform(out Matrix3x2 transform);
            _fixture.RenderTarget.SetTransform();
            _fixture.RenderTarget.GetTransform(out Matrix3x2 newTransform);
            newTransform.IsIdentity.Should().BeTrue();
            _fixture.RenderTarget.SetTransform(ref transform);
        }
    }
}
