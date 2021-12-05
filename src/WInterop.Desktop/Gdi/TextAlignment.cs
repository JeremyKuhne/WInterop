// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi;

/// <summary>
///  Text alignment
/// </summary>
public struct TextAlignment
{
    // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-gettextalign

    // https://docs.microsoft.com/openspecs/windows_protocols/ms-wmf/2cf0d802-5db7-42f6-bb75-50ff195a6c7c
    // https://docs.microsoft.com/openspecs/windows_protocols/ms-wmf/2475d008-f5ff-4c93-b28b-3953818b8827

    public uint Value;

    public TextAlignment(Horizontal horizontal, Vertical vertical)
    {
        Value = 0;
        HorizontalAlignment = horizontal;
        VerticalAlignment = vertical;
    }

    public TextAlignment(HorizontalAlignment horizontal, VerticalAlignment vertical)
    {
        Value = 0;
        HorizontalAlignment = horizontal switch
        {
            Windows.HorizontalAlignment.Left => Horizontal.Left,
            Windows.HorizontalAlignment.Right => Horizontal.Right,
            _ => Horizontal.Center,
        };

        VerticalAlignment = vertical switch
        {
            Windows.VerticalAlignment.Top => Vertical.Top,
            Windows.VerticalAlignment.Bottom => Vertical.Bottom,
            _ => Vertical.Baseline,
        };
    }

    public bool UpdatePosition
    {
        get => (Value & TA.TA_UPDATECP) != 0;
        set => Value = value
            ? (Value | TA.TA_UPDATECP)
            : (uint)(Value & ~TA.TA_UPDATECP);
    }

    public bool Valid => Value != TerraFXWindows.GDI_ERROR;

    public Horizontal HorizontalAlignment
    {
        get => (Horizontal)(Value & (TA.TA_RIGHT | TA.TA_CENTER));
        set => Value = (Value & ~(uint)HorizontalAlignment) | (uint)value;
    }

    public Vertical VerticalAlignment
    {
        get => (Vertical)(Value & (TA.TA_BOTTOM | TA.TA_BASELINE));
        set => Value = (Value & ~(uint)VerticalAlignment) | (uint)value;
    }

    public static explicit operator TextAlignment(uint value) => new() { Value = value };

    public enum Horizontal : uint
    {
        /// <summary>
        ///  Aligned to the left of the bounding box. (Considered "Top" for vertically aligned text.)
        /// </summary>
        Left = TA.TA_LEFT,

        /// <summary>
        ///  Aligned to the right of the bounding box. (Considered "Bottom" for vertically aligned text.)
        /// </summary>
        Right = TA.TA_RIGHT,

        /// <summary>
        ///  Aligned to the center fo the bounding box.
        /// </summary>
        Center = TA.TA_CENTER
    }

    public enum Vertical : uint
    {
        /// <summary>
        ///  Aligned to the top of the bounding box. (Considered "Right" for vertically aligned text.)
        /// </summary>
        Top = TA.TA_TOP,

        /// <summary>
        ///  Aligned to the botton of the bounding box. (Considered "Left" for vertically aligned text.)
        /// </summary>
        Bottom = TA.TA_BOTTOM,

        /// <summary>
        ///  Aligned to the text baseline.
        /// </summary>
        Baseline = TA.TA_BASELINE
    }
}