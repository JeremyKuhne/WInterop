// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd162897.aspx
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public void Set(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public bool PointIsInside(Point point)
        {
            return ((point.X >= left) && (point.X < right) &&
                (point.Y >= top) && (point.Y < bottom));
        }

        public void Offset(int cx, int cy)
        {
            left += cx;
            right += cx;
            bottom += cy;
            top += cy;
        }

        public void Inflate(int cx, int cy)
        {
            left -= cx;
            right += cx;
            bottom -= cy;
            top += cy;
        }

        public bool Intersects(RECT rect)
        {
            return !(Intersect(rect).IsEmpty());
        }

        public RECT Intersect(RECT rect)
        {
            int maxLeft = left > rect.left ? left : rect.left;
            int minRight = right < rect.right ? right : rect.right;

            if (maxLeft < minRight)
            {
                int maxTop = top > rect.top ? top : rect.top;
                int minBottom = bottom < rect.bottom ? bottom : rect.bottom;

                if (maxTop < minBottom)
                    return new RECT(maxLeft, maxTop, minRight, minBottom);
            }

            return new RECT();
        }

        /// <summary>
        /// Empty rect is one that has no area.
        /// </summary>
        public bool IsEmpty()
        {
            return ((left >= right) || (top >= bottom));
        }
    }
}
