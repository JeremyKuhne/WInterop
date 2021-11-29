// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.CompilerServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [D3DCOLORVALUE], [DXGI_RGBA], [D2D1_COLOR_F], [D2D_COLOR_F], etc.
    /// </summary>
    public readonly struct ColorF
    {
        public readonly float R;
        public readonly float G;
        public readonly float B;
        public readonly float A;

        public ColorF(float r, float g, float b, float a = 1.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColorF(Color color, float a = 1.0f)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
            A = a;
        }

        public static implicit operator ColorF(in DXGI_RGBA color)
            => Unsafe.As<DXGI_RGBA, ColorF>(ref Unsafe.AsRef(color));

        public static implicit operator DXGI_RGBA(in ColorF color)
            => Unsafe.As<ColorF, DXGI_RGBA>(ref Unsafe.AsRef(color));

        public static implicit operator Color(ColorF color)
            => Color.FromArgb((int)(color.A * 255), (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));

        public static implicit operator ColorF(Color color)
            => new(color, color.A / 255.0f);
    }
}
