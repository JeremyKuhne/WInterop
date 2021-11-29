// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Drawing;
using WInterop.Direct2d;
using Xunit;

namespace Direct2dTests
{
    public class FactoryTests
    {
        [Fact]
        public void Initialize()
        {
            using Factory factory = Direct2d.CreateFactory();

            Ellipse ellipse = new(new PointF(1.0f, 2.0f), 3.0f, 4.0f);
            using EllipseGeometry ellipseGeometry = factory.CreateEllipseGeometry(ellipse);
            ellipseGeometry.GetEllipse().Should().Be(ellipse);
        }

        [Fact]
        public void GetDpi()
        {
            using Factory factory = Direct2d.CreateFactory();
            SizeF dpi = factory.GetDesktopDpi();
        }
    }
}
