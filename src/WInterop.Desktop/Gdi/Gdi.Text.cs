// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public static partial class Gdi
    {
        public static FontHandle GetStockFont(StockFont font) => new FontHandle((HFONT)Imports.GetStockObject((int)font), ownsHandle: false);

        public static Color GetTextColor(this in DeviceContext context) => Imports.GetTextColor(context);

        public static Color SetTextColor(this in DeviceContext context, Color color) => Imports.SetTextColor(context, color);

        public static Color SetTextColor(this in DeviceContext context, SystemColor color)
            => Imports.SetTextColor(context, Windows.Windows.GetSystemColor(color));

        public static TextAlignment SetTextAlignment(this in DeviceContext context, TextAlignment alignment) => Imports.SetTextAlign(context, alignment);

        public static bool TextOut(this in DeviceContext context, Point position, ReadOnlySpan<char> text)
            => Imports.TextOutW(context, position.X, position.Y, ref MemoryMarshal.GetReference(text), text.Length);

        public static unsafe int DrawText(this in DeviceContext context, ReadOnlySpan<char> text, Rectangle bounds, TextFormat format)
        {
            Rect rect = bounds;

            if ((format & TextFormat.ModifyString) == 0)
            {
                // The string won't be changed, we can just pin
                return Imports.DrawTextW(context, ref MemoryMarshal.GetReference(text), text.Length, ref rect, format);
            }

            char[] buffer = ArrayPool<char>.Shared.Rent(text.Length + 5);
            try
            {
                Span<char> span = buffer.AsSpan();
                text.CopyTo(span);
                return Imports.DrawTextW(context, ref MemoryMarshal.GetReference(span), buffer.Length, ref rect, format);
            }
            finally
            {
                ArrayPool<char>.Shared.Return(buffer);
            }
        }

        public static bool GetTextMetrics(this in DeviceContext context, out TextMetrics metrics)
            => Imports.GetTextMetricsW(context, out metrics);

        /// <summary>
        ///  Creates a logical font with the specified characteristics that can be selected into a <see cref="DeviceContext"/>.
        /// </summary>
        /// <param name="height">"em" height of the font in logical pixels.</param>
        /// <param name="width">Average character width in logical pixels.</param>
        /// <param name="escapement">Angle in tenths of degrees.</param>
        /// <param name="orientation">Angle in tenths of degrees.</param>
        public static FontHandle CreateFont(
             int height = 0,
             int width = 0,
             int escapement = 0,
             int orientation = 0,
             FontWeight weight = FontWeight.DoNotCare,
             bool italic = false,
             bool underline = false,
             bool strikeout = false,
             CharacterSet characterSet = CharacterSet.Default,
             OutputPrecision outputPrecision = OutputPrecision.Default,
             ClippingPrecision clippingPrecision = ClippingPrecision.Default,
             Quality quality = Quality.Default,
             FontPitch pitch = FontPitch.Default,
             FontFamilyType family = FontFamilyType.DoNotCare,
             string? typeface = null)
        {
            return new FontHandle(Imports.CreateFontW(
                height,
                width,
                escapement,
                orientation,
                weight,
                italic,
                underline,
                strikeout,
                (uint)characterSet,
                (uint)outputPrecision,
                (uint)clippingPrecision,
                (uint)quality,
                (uint)((byte)pitch | (byte)family),
                typeface));
        }

        private static int EnumerateFontCallback(
            ref EnumerateLogicalFontExtendedDesignVector fontAttributes,
            ref NewTextMetricsExtended textMetrics,
            FontTypes fontType,
            LParam lParam)
        {
            var info = (List<FontInformation>?)GCHandle.FromIntPtr(lParam).Target;
            info?.Add(new FontInformation { FontType = fontType, TextMetrics = textMetrics, FontAttributes = fontAttributes });

            // Continue enumeration
            return 1;
        }

        public static IEnumerable<FontInformation> EnumerateFontFamilies(in DeviceContext context, CharacterSet characterSet, string faceName)
        {
            LogicalFont logFont = new LogicalFont
            {
                CharacterSet = characterSet,
            };

            logFont.FaceName.CopyFrom(faceName);

            List<FontInformation> info = new List<FontInformation>();
            GCHandle gch = GCHandle.Alloc(info, GCHandleType.Normal);
            try
            {
                int result = Imports.EnumFontFamiliesExW(context, ref logFont, EnumerateFontCallback, GCHandle.ToIntPtr(gch), 0);
            }
            finally
            {
                gch.Free();
            }

            return info;
        }
    }
}
