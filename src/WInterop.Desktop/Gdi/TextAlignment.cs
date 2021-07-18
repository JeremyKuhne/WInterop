// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi
{
    /// <summary>
    ///  Text alignment
    /// </summary>
    public struct TextAlignment
    {
        // https://msdn.microsoft.com/en-us/library/dd144932.aspx

        // https://msdn.microsoft.com/en-us/library/cc669453.aspx
        // https://msdn.microsoft.com/en-us/library/cc669454.aspx

        public uint Value;

        // private const uint TA_NOUPDATECP  = 0;
        private const uint TA_UPDATECP      = 1;
        private const uint TA_LEFT          = 0;
        private const uint TA_RIGHT         = 0b0000_0000_0010;
        private const uint TA_CENTER        = 0b0000_0000_0110;
        private const uint TA_TOP           = 0;
        private const uint TA_BOTTOM        = 0b0000_0000_1000;
        private const uint TA_BASELINE      = 0b0000_0001_1000;
        private const uint TA_RTLREADING    = 0b0001_0000_0000;
        // private const uint VTA_BASELINE     = TA_BASELINE;
        // private const uint VTA_LEFT         = TA_BOTTOM;
        // private const uint VTA_RIGHT        = TA_TOP;
        // private const uint VTA_CENTER       = TA_CENTER;
        // private const uint VTA_BOTTOM       = TA_RIGHT;
        // private const uint VTA_TOP          = TA_LEFT;
        private const uint GDI_ERROR        = 0xFFFFFFFF;

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
            get => (Value & TA_UPDATECP) != 0;
            set => Value = value ? Value | TA_UPDATECP : Value & ~TA_UPDATECP;
        }

        public bool Valid => Value != GDI_ERROR;

        public Horizontal HorizontalAlignment
        {
            get => (Horizontal)(Value & (TA_RIGHT | TA_CENTER));
            set => Value = (Value & ~(uint)HorizontalAlignment) | (uint)value;
        }

        public Vertical VerticalAlignment
        {
            get => (Vertical)(Value & (TA_BOTTOM | TA_BASELINE));
            set => Value = (Value & ~(uint)VerticalAlignment) | (uint)value;
        }

        public enum Horizontal : uint
        {
            /// <summary>
            ///  Aligned to the left of the bounding box. (Considered "Top" for vertically aligned text.)
            /// </summary>
            Left = TA_LEFT,

            /// <summary>
            ///  Aligned to the right of the bounding box. (Considered "Bottom" for vertically aligned text.)
            /// </summary>
            Right = TA_RIGHT,

            /// <summary>
            ///  Aligned to the center fo the bounding box.
            /// </summary>
            Center = TA_CENTER
        }

        public enum Vertical : uint
        {
            /// <summary>
            ///  Aligned to the top of the bounding box. (Considered "Right" for vertically aligned text.)
            /// </summary>
            Top  = TA_TOP,

            /// <summary>
            ///  Aligned to the botton of the bounding box. (Considered "Left" for vertically aligned text.)
            /// </summary>
            Bottom = TA_BOTTOM,

            /// <summary>
            ///  Aligned to the text baseline.
            /// </summary>
            Baseline = TA_BASELINE
        }
    }
}