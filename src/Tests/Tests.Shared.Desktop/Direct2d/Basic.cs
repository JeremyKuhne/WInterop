using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using WInterop.Direct2d;
using WInterop.Direct2d.Native;
using WInterop.Errors;
using System.Drawing;

namespace DesktopTests.Direct2d
{
    public class Basic
    {
        [Fact]
        public void Initialize()
        {
            Guid riid = new Guid(InterfaceIds.IID_ID2D1Factory);
            HRESULT result = Imports.D2D1CreateFactory(FactoryType.SingleThreaded, ref riid, DebugLevel.None, out IFactory factory);

            Ellipse ellipse = new Ellipse(new PointF(1.0f, 2.0f), 3.0f, 4.0f);
            var ellipseGeometry = factory.CreateEllipseGeometry(in ellipse);
            ellipseGeometry.GetEllipse(out Ellipse newEllipse);
        }
    }
}
