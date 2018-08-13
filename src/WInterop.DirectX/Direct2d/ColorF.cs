// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    /// [D3DCOLORVALUE], [DXGI_RGBA], [D2D1_COLOR_F], [D2D_COLOR_F], etc.
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
            => Color.FromArgb((int)(color.A * 255), (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));

        public static implicit operator ColorF(Color color)
            => new ColorF(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
    }
}
