// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi;

public static partial class Gdi
{
    public static FontHandle GetStockFont(StockFont font)
        => new((HFONT)GdiImports.GetStockObject((int)font), ownsHandle: false);

    public static Color GetTextColor(this in DeviceContext context) => GdiImports.GetTextColor(context);

    public static Color SetTextColor(this in DeviceContext context, Color color) => GdiImports.SetTextColor(context, color);

    public static Color SetTextColor(this in DeviceContext context, SystemColor color)
        => GdiImports.SetTextColor(context, Windows.Windows.GetSystemColor(color));

    public static TextAlignment GetTextAlignment(this in DeviceContext context)
        => GdiImports.GetTextAlign(context);

    public static TextAlignment SetTextAlignment(this in DeviceContext context, TextAlignment alignment)
        => GdiImports.SetTextAlign(context, alignment);

    /// <summary>
    ///  Draws text utilizing the current selected font, background color, and text color. Uses the
    ///  current text alignment <see cref="SetTextAlignment(in DeviceContext, TextAlignment)"/>.
    /// </summary>
    public static bool TextOut(this in DeviceContext context, Point position, ReadOnlySpan<char> text)
        => GdiImports.TextOutW(context, position.X, position.Y, ref MemoryMarshal.GetReference(text), text.Length);

    public static unsafe int DrawText(
        this in DeviceContext context,
        ReadOnlySpan<char> text,
        Rectangle bounds,
        TextFormat format)
    {
        Rect rect = bounds;

        if ((format & TextFormat.ModifyString) == 0)
        {
            // The string won't be changed, we can just pin
            fixed (char* c = text)
            {
                return GdiImports.DrawTextW(context, c, text.Length, ref rect, format);
            }
        }

        char[] buffer = ArrayPool<char>.Shared.Rent(text.Length);
        text.CopyTo(buffer.AsSpan());
        fixed (char* c = buffer)
        {
            int result = GdiImports.DrawTextW(context, c, text.Length, ref rect, format);
            ArrayPool<char>.Shared.Return(buffer);
            return result;
        }
    }

    public static bool GetTextMetrics(this in DeviceContext context, out TextMetrics metrics)
        => GdiImports.GetTextMetricsW(context, out metrics);

    /// <summary>
    ///  Converts the requested point size to height based on the DPI of the given device context.
    /// </summary>
    public static int FontPointSizeToHeight(this in DeviceContext context, int pointSize)
    {
        return Windows.Native.WindowsImports.MulDiv(
            pointSize,
            GetDeviceCapability(context, DeviceCapability.LogicalPixelsY),
            72);
    }

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
        return new FontHandle(GdiImports.CreateFontW(
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
        LogicalFont logFont = new()
        {
            CharacterSet = characterSet,
        };

        logFont.FaceName.CopyFrom(faceName);

        List<FontInformation> info = new();
        GCHandle gch = GCHandle.Alloc(info, GCHandleType.Normal);
        try
        {
            int result = GdiImports.EnumFontFamiliesExW(context, ref logFont, EnumerateFontCallback, GCHandle.ToIntPtr(gch), 0);
        }
        finally
        {
            gch.Free();
        }

        return info;
    }

    public static unsafe LogicalFont GetLogicalFont(this FontHandle font)
    {
        Unsafe.SkipInit(out LogicalFont logFont);
        GdiImports.GetObjectW(font, sizeof(LogicalFont), &logFont);
        return logFont;
    }
}