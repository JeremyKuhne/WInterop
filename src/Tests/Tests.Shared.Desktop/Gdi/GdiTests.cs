// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Windows;
using WInterop.Windows.Types;
using Xunit;

namespace DesktopTests.Gdi
{
    public class GdiTests
    {
        [Fact]
        public void EnumerateDisplayDevices()
        {
            var devices = GdiMethods.EnumerateDisplayDevices(null).ToArray();
            devices.Should().Contain(d => (d.StateFlags & (DeviceState.DISPLAY_DEVICE_ACTIVE | DeviceState.DISPLAY_DEVICE_PRIMARY_DEVICE)) ==
                (DeviceState.DISPLAY_DEVICE_ACTIVE | DeviceState.DISPLAY_DEVICE_PRIMARY_DEVICE));
        }

        [Fact]
        public void EnumerateDisplayDevices_Monitors()
        {
            var device = GdiMethods.EnumerateDisplayDevices(null).First();
            var monitor = GdiMethods.EnumerateDisplayDevices(device.DeviceName).First();

            // Something like \\.\DISPLAY1 and \\.\DISPLAY1\Monitor0
            monitor.DeviceName.Should().StartWith(device.DeviceName);
        }

        [Fact]
        public void EnumerateDisplaySettings_Null()
        {
            var settings = GdiMethods.EnumerateDisplaySettings(null).ToArray();
            settings.Should().NotBeEmpty();
        }

        [Fact]
        public void EnumerateDisplaySettings_FirstDevice()
        {
            var device = GdiMethods.EnumerateDisplayDevices(null).First();
            var settings = GdiMethods.EnumerateDisplaySettings(device.DeviceName);
            settings.Should().NotBeEmpty();
        }

        [Fact]
        public void EnumerateDisplaySettings_FirstDevice_CurrentMode()
        {
            var device = GdiMethods.EnumerateDisplayDevices(null).First();
            var settings = GdiMethods.EnumerateDisplaySettings(device.DeviceName, GdiDefines.ENUM_CURRENT_SETTINGS).ToArray();
            settings.Length.Should().Be(1);
        }

        [Fact]
        public void GetDeviceContext_NullWindow()
        {
            // Null here should be the entire screen
            DeviceContext context = GdiMethods.GetDeviceContext(WindowHandle.Null);
            context.IsInvalid.Should().BeFalse();
            int pixelWidth = GdiMethods.GetDeviceCapability(context, DeviceCapability.HORZRES);
            int pixelHeight = GdiMethods.GetDeviceCapability(context, DeviceCapability.VERTRES);
        }

        [Fact]
        public void GetWindowDeviceContext_NullWindow()
        {
            // Null here should be the entire screen
            DeviceContext context = GdiMethods.GetWindowDeviceContext(WindowHandle.Null);
            context.IsInvalid.Should().BeFalse();
            int pixelWidth = GdiMethods.GetDeviceCapability(context, DeviceCapability.HORZRES);
            int pixelHeight = GdiMethods.GetDeviceCapability(context, DeviceCapability.VERTRES);
        }

        [Fact]
        public unsafe void AXISINFO_Size()
        {
            sizeof(AXISINFO).Should().Be(40);
        }

        [Fact]
        public unsafe void AXISINFO_Blittable()
        {
            GCHandle.Alloc(new AXISINFO(), GCHandleType.Pinned).Free();
        }

        [Fact]
        public unsafe void ENUMLOGFONTEXDV_Size()
        {
            sizeof(ENUMLOGFONTEXDV).Should().Be(420);
        }

        [Fact]
        public unsafe void ENUMLOGFONTEXDV_Blittable()
        {
            GCHandle.Alloc(new ENUMLOGFONTEXDV(), GCHandleType.Pinned).Free();
        }

        [Fact]
        public unsafe void DESIGNVECTOR_Size()
        {
            sizeof(DESIGNVECTOR).Should().Be(72);
        }

        [Fact]
        public unsafe void ENUMLOGFONTEX_Size()
        {
            sizeof(ENUMLOGFONTEX).Should().Be(348);
        }

        [Fact]
        public unsafe void LOGFONT_Size()
        {
            sizeof(LOGFONT).Should().Be(92);
        }

        [Fact]
        public unsafe void NEWTEXTMETRIC_Size()
        {
            sizeof(NEWTEXTMETRIC).Should().Be(76);
        }

        [Fact]
        public unsafe void NEWTEXTMETRICEX_Size()
        {
            sizeof(NEWTEXTMETRICEX).Should().Be(100);
        }

        [Fact]
        public unsafe void FONTSIGNATURE_Size()
        {
            sizeof(FONTSIGNATURE).Should().Be(24);
        }

        [Fact]
        public unsafe void EnumFont_Arial()
        {
            using (var context = GdiMethods.GetDeviceContext(WindowMethods.GetDesktopWindow()))
            {
                var info = GdiMethods.EnumerateFontFamilies(context, CharacterSet.ANSI_CHARSET, "Arial");
                info.Count().Should().Be(4);
                var regular = info.First();
                regular.FontAttributes.elfEnumLogfontEx.elfFullName.CreateString().Should().Be("Arial");
                regular.FontAttributes.elfEnumLogfontEx.elfStyle.CreateString().Should().Be("Regular");
                regular.FontAttributes.elfEnumLogfontEx.elfScript.CreateString().Should().Be("Western");
                regular.TextMetrics.ntmTm.ntmFlags.Should().Be(TextMetricFlags.NTM_REGULAR | TextMetricFlags.NTM_TT_OPENTYPE | TextMetricFlags.NTM_DSIG);
                regular.TextMetrics.ntmTm.tmPitchAndFamily.PitchTypes.Should().Be(FontPitchTypes.VariablePitch | FontPitchTypes.TrueType | FontPitchTypes.Vector);
                regular.TextMetrics.ntmTm.tmPitchAndFamily.Family.Should().Be(FontFamily.Swiss);
                regular.TextMetrics.ntmFontSig.UnicodeSubsetsOne.Should().Be(
                    UnicodeSubsetsOne.BasicLatin | UnicodeSubsetsOne.Latin1Supplement | UnicodeSubsetsOne.LatinExtendedA | UnicodeSubsetsOne.LatinExtendedB
                    | UnicodeSubsetsOne.IPAPhoneticExtensions | UnicodeSubsetsOne.SpacingToneModifier | UnicodeSubsetsOne.CombiningDiacriticalMarks
                    | UnicodeSubsetsOne.GreekAndCoptic | UnicodeSubsetsOne.Cyrillic | UnicodeSubsetsOne.Armenian | UnicodeSubsetsOne.Hebrew
                    | UnicodeSubsetsOne.Arabic | UnicodeSubsetsOne.LatinExtendedAdditionalCD | UnicodeSubsetsOne.GreekExtended | UnicodeSubsetsOne.Punctuation);
                regular.TextMetrics.ntmFontSig.UnicodeSubsetsFour.Should().Be((UnicodeSubsetsFour)0);
                regular.TextMetrics.ntmFontSig.CodePagesOem.Should().Be(CodePagesOem.ModernGreek | CodePagesOem.Russian | CodePagesOem.Nordic
                    | CodePagesOem.Arabic | CodePagesOem.CanadianFrench | CodePagesOem.Hebrew | CodePagesOem.Icelandic | CodePagesOem.Portugese
                    | CodePagesOem.Turkish | CodePagesOem.Cyrillic | CodePagesOem.Latin2 | CodePagesOem.Baltic | CodePagesOem.Greek
                    | CodePagesOem.ArabicAsmo | CodePagesOem.MuiltilingualLatin | CodePagesOem.US);
            }
        }

        [Fact]
        public void EnumFont_All()
        {
            // Just making sure we don't fall over
            using (var context = GdiMethods.GetDeviceContext(WindowMethods.GetDesktopWindow()))
            {
                GdiMethods.EnumerateFontFamilies(context, CharacterSet.DEFAULT_CHARSET, null).Should().NotBeEmpty();
            }
        }

        [Fact]
        public void GetSystemColorBrush()
        {
            // System color brushes are special- they'll always give the same value
            BrushHandle brush = GdiMethods.GetSystemColorBrush(SystemColor.MenuBar);
            long handle = (long)brush.Handle.Handle;
            handle = handle & 0xFFFF00;

            // This changed in RS4 from C5 to BF for the last byte. Checking the first
            // bytes to make sure we're in the right ballpark.
            handle.Should().Be(0x100000);
        }
    }
}
