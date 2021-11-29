// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

public readonly struct PaddingF
{
    public readonly float Left;
    public readonly float Top;
    public readonly float Right;
    public readonly float Bottom;

    public PaddingF(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public static implicit operator PaddingF(float padding) => new(padding, padding, padding, padding);
    public static implicit operator PaddingF((float Left, float Top, float Right, float Bottom) padding)
        => new(padding.Left, padding.Top, padding.Right, padding.Bottom);
}