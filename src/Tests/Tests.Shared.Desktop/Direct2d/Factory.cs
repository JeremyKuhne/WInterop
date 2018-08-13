// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using WInterop.Direct2d;
using WInterop.Direct2d.Native;
using WInterop.Errors;
using System.Drawing;

namespace Direct2dTests
{
    public class Factory
    {
        [Fact]
        public void Initialize()
        {
            HRESULT result = Imports.D2D1CreateFactory(
                FactoryType.SingleThreaded, new Guid(InterfaceIds.IID_ID2D1Factory), DebugLevel.None, out IFactory factory);

            Ellipse ellipse = new Ellipse(new PointF(1.0f, 2.0f), 3.0f, 4.0f);
            var ellipseGeometry = factory.CreateEllipseGeometry(in ellipse);
            ellipseGeometry.GetEllipse(out Ellipse newEllipse);
        }

        [Fact]
        public void GetDpi()
        {
            IFactory factory = Direct2d.CreateFactory();

            factory.GetDesktopDpi(out float x, out float y);
        }
    }
}
