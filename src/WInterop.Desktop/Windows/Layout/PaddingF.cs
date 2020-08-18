// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public struct PaddingF
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;

        public PaddingF(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static implicit operator PaddingF(float padding) => new PaddingF(padding, padding, padding, padding);
        public static implicit operator PaddingF((float left, float top, float right, float bottom) padding)
            => new PaddingF(padding.left, padding.top, padding.right, padding.bottom);
    }
}
