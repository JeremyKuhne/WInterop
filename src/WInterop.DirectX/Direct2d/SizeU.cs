// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    /// [D2D_SIZE_U]
    /// </summary>
    public readonly struct SizeU
    {
        public readonly uint Width;
        public readonly uint Height;

        public SizeU(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public static implicit operator Size(SizeU size) => new Size((int)size.Width, (int)size.Height);
        public static implicit operator SizeU(Size size) => new SizeU((uint)size.Width, (uint)size.Height);
    }
}
