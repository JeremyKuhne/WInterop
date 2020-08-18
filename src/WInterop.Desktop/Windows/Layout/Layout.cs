// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows
{
    public static class Layout
    {
        public static void SetLayout(this Window window, ILayoutHandler handler)
        {
            new LayoutProvider(window, handler);
        }

        public static ILayoutHandler FixedPercent(
            ILayoutHandler handler,
            SizeF percent,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
            => new FixedPercentLayout(handler, percent, verticalAlignment, horizontalAlignment);

        public static ILayoutHandler FixedPercent(
            ILayoutHandler handler,
            float percent,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
            => new FixedPercentLayout(handler, new SizeF(percent, percent), verticalAlignment, horizontalAlignment);

        public static ILayoutHandler FixedSize(
            ILayoutHandler handler,
            Size size,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
            => new FixedSizeLayout(handler, size, verticalAlignment, horizontalAlignment);

        public static ILayoutHandler Horizontal(
            params (float Percent, ILayoutHandler Handler)[] handlers)
            => new HorizontalLayout(handlers);

        public static ILayoutHandler Vertical(
            params (float Percent, ILayoutHandler Handler)[] handlers)
            => new VerticalLayout(handlers);

        public static ILayoutHandler Margin(
            Padding margin,
            ILayoutHandler handler)
            => new PaddedLayout(margin, handler);

        public static ILayoutHandler Fill(ILayoutHandler handler) => new FillLayout(handler);

        public static ILayoutHandler Empty => EmptyLayout.Instance;
    }
}
