// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Windows;

namespace WInterop.Gdi;

public static unsafe partial class Gdi
{
    public static FontHandle GetStockFont(StockFont font)
        => new((HFONT)TerraFXWindows.GetStockObject((int)font), ownsHandle: false);

    public static Color GetTextColor(this in DeviceContext context) => TerraFXWindows.GetTextColor(context).ToColor();

    public static Color SetTextColor(this in DeviceContext context, Color color)
        => TerraFXWindows.SetTextColor(context, color.ToCOLORREF()).ToColor();

    public static Color SetTextColor(this in DeviceContext context, SystemColor color)
        => TerraFXWindows.SetTextColor(context, TerraFXWindows.GetSysColor((int)color)).ToColor();

    public static TextAlignment GetTextAlignment(this in DeviceContext context)
        => (TextAlignment)TerraFXWindows.GetTextAlign(context);

    public static TextAlignment SetTextAlignment(this in DeviceContext context, TextAlignment alignment)
        => (TextAlignment)TerraFXWindows.SetTextAlign(context, alignment.Value);

    /// <summary>
    ///  Draws text utilizing the current selected font, background color, and text color. Uses the
    ///  current text alignment <see cref="SetTextAlignment(in DeviceContext, TextAlignment)"/>.
    /// </summary>
    public static bool TextOut(this in DeviceContext context, Point position, ReadOnlySpan<char> text)
    {
        fixed (void* t = text)
        {
            return TerraFXWindows.TextOutW(context, position.X, position.Y, (ushort*)t, text.Length);
        }
    }

    public static unsafe int DrawText(
        this in DeviceContext context,
        ReadOnlySpan<char> text,
        Rectangle bounds,
        TextFormat format)
    {
        RECT rect = bounds.ToRECT();

        if ((format & TextFormat.ModifyString) == 0)
        {
            // The string won't be changed, we can just pin
            fixed (char* c = text)
            {
                return TerraFXWindows.DrawTextW(context, (ushort*)c, text.Length, &rect, (uint)format);
            }
        }

        char[] buffer = ArrayPool<char>.Shared.Rent(text.Length);
        text.CopyTo(buffer.AsSpan());
        fixed (char* c = buffer)
        {
            int result = TerraFXWindows.DrawTextW(context, (ushort*)c, text.Length, &rect, (uint)format);
            ArrayPool<char>.Shared.Return(buffer);
            return result;
        }
    }

    public static bool GetTextMetrics(this in DeviceContext context, out TextMetrics metrics)
    {
        fixed (void* m = &metrics)
        {
            return TerraFXWindows.GetTextMetricsW(context, (TEXTMETRICW*)m);
        }
    }

    /// <summary>
    ///  Converts the requested point size to height based on the DPI of the given device context.
    /// </summary>
    public static int FontPointSizeToHeight(this in DeviceContext context, int pointSize)
        => TerraFXWindows.MulDiv(
            pointSize,
            GetDeviceCapability(context, DeviceCapability.LogicalPixelsY),
            72);

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
         FontQuality quality = FontQuality.Default,
         FontPitch pitch = FontPitch.Default,
         FontFamilyType family = FontFamilyType.DoNotCare,
         string? typeface = null)
    {
        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-createfontw
        fixed (void* t = typeface)
        {
            return new FontHandle(TerraFXWindows.CreateFontW(
                height,
                width,
                escapement,
                orientation,
                (int)weight,
                (uint)italic.ToBOOL(),
                (uint)underline.ToBOOL(),
                (uint)strikeout.ToBOOL(),
                (uint)characterSet,
                (uint)outputPrecision,
                (uint)clippingPrecision,
                (uint)quality,
                (uint)((byte)pitch | (byte)family),
                (ushort*)t));
        }
    }

    // https://docs.microsoft.com/previous-versions/dd162618
    [UnmanagedCallersOnly]
    private static int EnumFontFamExProc(
        LOGFONTW* lpelfe,
        TEXTMETRICW* lpntme,
        uint fontType,
        LPARAM lParam)
    {
        var info = (List<FontInformation>?)GCHandle.FromIntPtr(lParam).Target;
        info?.Add(new FontInformation
            {
                FontType = (FontTypes)fontType,
                TextMetrics = *(NewTextMetricsExtended*)lpntme,
                FontAttributes = *(EnumerateLogicalFontExtendedDesignVector*)lpelfe
        });

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
            // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-enumfontfamiliesexw
            int result = TerraFXWindows.EnumFontFamiliesExW(context, (LOGFONTW*)&logFont, &EnumFontFamExProc, GCHandle.ToIntPtr(gch), 0);
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
        _ = TerraFXWindows.GetObjectW(font.HFONT, sizeof(LogicalFont), &logFont);
        return logFont;
    }
}