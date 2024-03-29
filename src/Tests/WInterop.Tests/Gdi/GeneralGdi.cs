﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using WInterop;
using WInterop.Gdi;
using WInterop.Windows;

namespace GdiTests;

public class GeneralGdi
{
    [Fact]
    public void EnumerateDisplayDevices()
    {
        var devices = Gdi.EnumerateDisplayDevices(null).ToArray();
        devices.Should().Contain(d => (d.StateFlags & (DeviceState.Active | DeviceState.PrimaryDevice)) ==
            (DeviceState.Active | DeviceState.PrimaryDevice));
    }

    [Fact]
    public void EnumerateDisplayDevices_Monitors()
    {
        var device = Gdi.EnumerateDisplayDevices(null).First();
        var monitor = Gdi.EnumerateDisplayDevices(device.DeviceName.Buffer.CreateString()).First();

        // Something like \\.\DISPLAY1 and \\.\DISPLAY1\Monitor0
        monitor.DeviceName.Buffer.CreateString().Should().StartWith(device.DeviceName.Buffer.CreateString());
    }

    [Fact]
    public void EnumerateDisplaySettings_Null()
    {
        var settings = Gdi.EnumerateDisplaySettings(null).ToArray();
        settings.Should().NotBeEmpty();
    }

    [Fact]
    public void EnumerateDisplaySettings_FirstDevice()
    {
        var device = Gdi.EnumerateDisplayDevices(null).First();
        var settings = Gdi.EnumerateDisplaySettings(device.DeviceName.Buffer.CreateString());
        settings.Should().NotBeEmpty();
    }

    [Fact]
    public void EnumerateDisplaySettings_FirstDevice_CurrentMode()
    {
        var device = Gdi.EnumerateDisplayDevices(null).First();
        var settings = Gdi.EnumerateDisplaySettings(
            device.DeviceName.Buffer.CreateString(),
            GdiDefines.ENUM_CURRENT_SETTINGS).ToArray();
        settings.Length.Should().Be(1);
    }

    [Fact]
    public void GetDeviceContext_NullWindow()
    {
        // Null here should be the entire screen
        using DeviceContext context = Gdi.GetDeviceContext(default);
        context.IsInvalid.Should().BeFalse();
        int pixeWidth = context.GetDeviceCapability(DeviceCapability.HorzontalResolution);
        int pixelHeight = context.GetDeviceCapability(DeviceCapability.VerticalResolution);
    }

    [Fact]
    public void GetWindowDeviceContext_NullWindow()
    {
        // Null here should be the entire screen
        using DeviceContext context = Gdi.GetWindowDeviceContext(default);
        context.IsInvalid.Should().BeFalse();
        int pixelWidth = context.GetDeviceCapability(DeviceCapability.HorzontalResolution);
        int pixelHeight = context.GetDeviceCapability(DeviceCapability.VerticalResolution);
    }

    [Fact]
    public void CreateDesktopDeviceContext()
    {
        using DeviceContext context = Gdi.CreateDesktopDeviceContext();

        context.GetMappingMode().Should().Be(MappingMode.Text);
        context.GetBoundsRect(out BoundsState state).Should().Be(Rectangle.Empty);
        state.Should().Be(BoundsState.Reset);

        Size resolution = context.GetDeviceResolution();
        Size desktopResolution = context.GetDesktopResolution();

        int pixelWidth = context.GetDeviceCapability(DeviceCapability.HorzontalResolution);
        int pixelHeight = context.GetDeviceCapability(DeviceCapability.VerticalResolution);
        context.GetViewportOrigin(out bool success).Should().Be(new Point());
        success.Should().BeTrue();
        context.GetWindowOrigin(out success).Should().Be(new Point());
        success.Should().BeTrue();
        context.GetViewportExtents(out Size viewportExtents).Should().BeTrue();
        context.GetWindowExtents(out Size windowExtents).Should().BeTrue();
        Rectangle clip = Windows.GetClipCursor();
    }

    [Fact]
    public unsafe void AXISINFO_Size()
    {
        sizeof(AxisInfo).Should().Be(40);
    }

    [Fact]
    public unsafe void AXISINFO_Blittable()
    {
        GCHandle.Alloc(new AxisInfo(), GCHandleType.Pinned).Free();
    }

    [Fact]
    public unsafe void ENUMLOGFONTEXDV_Size()
    {
        sizeof(EnumerateLogicalFontExtendedDesignVector).Should().Be(420);
    }

    [Fact]
    public unsafe void ENUMLOGFONTEXDV_Blittable()
    {
        GCHandle.Alloc(new EnumerateLogicalFontExtendedDesignVector(), GCHandleType.Pinned).Free();
    }

    [Fact]
    public unsafe void DESIGNVECTOR_Size()
    {
        sizeof(DesignVector).Should().Be(72);
    }

    [Fact]
    public unsafe void ENUMLOGFONTEX_Size()
    {
        sizeof(EnumerateLogicalFontExtended).Should().Be(348);
    }

    [Fact]
    public unsafe void LOGFONT_Size()
    {
        sizeof(LogicalFont).Should().Be(92);
    }

    [Fact]
    public unsafe void NEWTEXTMETRIC_Size()
    {
        sizeof(NewTextMetrics).Should().Be(76);
    }

    [Fact]
    public unsafe void NEWTEXTMETRICEX_Size()
    {
        sizeof(NewTextMetricsExtended).Should().Be(100);
    }

    [Fact]
    public unsafe void FONTSIGNATURE_Size()
    {
        sizeof(FontSignature).Should().Be(24);
    }

    [Fact]
    public unsafe void EnumFont_Arial()
    {
        using var context = Gdi.GetDeviceContext(Windows.GetDesktopWindow());
        var info = Gdi.EnumerateFontFamilies(context, CharacterSet.Ansi, "Arial");
        info.Count().Should().Be(4);
        var regular = info.First();
        regular.FontAttributes.EnumLogicalFontEx.FullName.CreateString().Should().Be("Arial");
        regular.FontAttributes.EnumLogicalFontEx.Style.CreateString().Should().Be("Regular");
        regular.FontAttributes.EnumLogicalFontEx.Script.CreateString().Should().Be("Western");
        regular.TextMetrics.TextMetrics.Flags.Should().Be(TextMetricFlags.Regular | TextMetricFlags.TrueType | TextMetricFlags.DigitalSignature);
        regular.TextMetrics.TextMetrics.PitchAndFamily.PitchTypes.Should().Be(FontPitchTypes.VariablePitch | FontPitchTypes.TrueType | FontPitchTypes.Vector);
        regular.TextMetrics.TextMetrics.PitchAndFamily.Family.Should().Be(FontFamilyType.Swiss);
        regular.TextMetrics.FontSignature.UnicodeSubsetsOne.Should().Be(
            UnicodeSubsetsOne.BasicLatin | UnicodeSubsetsOne.Latin1Supplement | UnicodeSubsetsOne.LatinExtendedA | UnicodeSubsetsOne.LatinExtendedB
            | UnicodeSubsetsOne.IPAPhoneticExtensions | UnicodeSubsetsOne.SpacingToneModifier | UnicodeSubsetsOne.CombiningDiacriticalMarks
            | UnicodeSubsetsOne.GreekAndCoptic | UnicodeSubsetsOne.Cyrillic | UnicodeSubsetsOne.Armenian | UnicodeSubsetsOne.Hebrew
            | UnicodeSubsetsOne.Arabic | UnicodeSubsetsOne.LatinExtendedAdditionalCD | UnicodeSubsetsOne.GreekExtended | UnicodeSubsetsOne.Punctuation);
        regular.TextMetrics.FontSignature.UnicodeSubsetsFour.Should().Be((UnicodeSubsetsFour)0);
        regular.TextMetrics.FontSignature.CodePagesOem.Should().Be(CodePagesOem.ModernGreek | CodePagesOem.Russian | CodePagesOem.Nordic
            | CodePagesOem.Arabic | CodePagesOem.CanadianFrench | CodePagesOem.Hebrew | CodePagesOem.Icelandic | CodePagesOem.Portugese
            | CodePagesOem.Turkish | CodePagesOem.Cyrillic | CodePagesOem.Latin2 | CodePagesOem.Baltic | CodePagesOem.Greek
            | CodePagesOem.ArabicAsmo | CodePagesOem.MuiltilingualLatin | CodePagesOem.US);
    }

    [Fact]
    public void EnumFont_All()
    {
        // Just making sure we don't fall over
        using var context = Gdi.GetDeviceContext(Windows.GetDesktopWindow());
        Gdi.EnumerateFontFamilies(context, CharacterSet.Default, null).Should().NotBeEmpty();
    }

    [Fact]
    public void GetSystemColorBrush()
    {
        // System color brushes are special- they'll always give the same value
        BrushHandle brush = Gdi.GetSystemColorBrush(SystemColor.MenuBar);
        long handle = (long)brush.Handle;
        handle &= 0xFFFF00;

        // This changed in RS4 from C5 to BF for the last byte. Checking the first
        // bytes to make sure we're in the right ballpark.
        handle.Should().Be(0x100000);
    }
}
