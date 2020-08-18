// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public struct Padding
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static implicit operator Padding(int padding) => new Padding(padding, padding, padding, padding);
        public static implicit operator Padding((int left, int top, int right, int bottom) padding)
            => new Padding(padding.left, padding.top, padding.right, padding.bottom);
    }
}
