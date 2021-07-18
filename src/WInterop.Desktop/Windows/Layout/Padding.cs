// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public readonly struct Padding
    {
        public readonly int Left;
        public readonly int Top;
        public readonly int Right;
        public readonly int Bottom;

        public Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static implicit operator Padding(int padding) => new(padding, padding, padding, padding);
        public static implicit operator Padding((int Left, int Top, int Right, int Bottom) padding)
            => new(padding.Left, padding.Top, padding.Right, padding.Bottom);
    }
}