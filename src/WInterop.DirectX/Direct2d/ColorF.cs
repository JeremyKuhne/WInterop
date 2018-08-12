// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    /// [D3DCOLORVALUE], [DXGI_RGBA], etc.
    /// </summary>
    public readonly struct ColorF
    {
        public readonly float R;
        public readonly float G;
        public readonly float B;
        public readonly float A;

        public ColorF(float r, float g, float b, float a = 0.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static implicit operator Color(ColorF color)
            => Color.FromArgb((int)color.A, (int)color.R, (int)color.G, (int)color.B);

        public static implicit operator ColorF(Color color)
            => new ColorF(color.R, color.G, color.B, color.A);
    }
}
