// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using WInterop.Com;
using WInterop.Com.Native;
using FluentAssertions;
using WInterop.Errors;
using WInterop.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WInterop.Tests.Com
{
    public class TypeLibTests
    {
        // "00020430-0000-0000-C000-000000000046"
        public static Guid IID_StdOle = new Guid(0x00020430, 0x0000, 0x0000, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46);


        internal static HResult LoadStdOle2(out ITypeLib typeLib)
            => Imports.LoadRegTypeLib(ref IID_StdOle, 2, 0, 0, out typeLib);

        [Fact]
        public unsafe void LoadFromRegistry()
        {
            HResult result = LoadStdOle2(out ITypeLib typeLib);

            result.Should().Be(HResult.S_OK);
            typeLib.Should().NotBeNull();

            uint typeInfoCount = typeLib.GetTypeInfoCount();
            typeInfoCount.Should().Be(42);

            TypeLibraryAttributes* attributes;
            result = typeLib.GetLibAttr(&attributes);
            result.Should().Be(HResult.S_OK);

            attributes->MajorVerNum.Should().Be(2);
            attributes->MinorVerNum.Should().Be(0);
            attributes->LocaleId.Should().Be((LocaleId)0);
            attributes->LibraryFlags.Should().Be(LibraryFlags.HasDiskImage);
            attributes->Guid.Should().Be(IID_StdOle);
            attributes->SystemKind.Should().Be(Environment.Is64BitProcess ? SystemKind.Win64 : SystemKind.Win32);

            typeLib.ReleaseTLibAttr(attributes);
        }

        [Fact]
        public unsafe void GetDocumentation()
        {
            HResult result = LoadStdOle2(out ITypeLib typeLib);

            result.Should().Be(HResult.S_OK);
            typeLib.Should().NotBeNull();

            uint typeInfoCount = typeLib.GetTypeInfoCount();
            typeInfoCount.Should().Be(42);

            var docs = new List<(string name, string doc, string helpFile)>();

            for (int i = 0; i < typeInfoCount; i++)
            {
                BasicString name;
                BasicString doc;
                BasicString helpFile;
                result = typeLib.GetDocumentation(
                    index: i,
                    &name,
                    &doc,
                    null,
                    &helpFile);

                result.Should().Be(HResult.S_OK);
                docs.Add((name.ToStringAndFree(), doc.ToStringAndFree(), helpFile.ToStringAndFree()));
            }

            docs[34].Should().Be(("IPicture", "Picture Object", null));
        }

        [Fact]
        public unsafe void IsName()
        {
            HResult result = LoadStdOle2(out ITypeLib typeLib);

            result.Should().Be(HResult.S_OK);
            typeLib.Should().NotBeNull();

            string name = "ifont";
            char* nameBuffer = stackalloc char[name.Length + 1];
            Span<char> nameSpan = new Span<char>(nameBuffer, name.Length);
            name.AsSpan().CopyTo(nameSpan);
            nameBuffer[name.Length] = '\0';

            result = typeLib.IsName(
                nameBuffer,
                lHashVal: 0,
                out IntBoolean foundName);

            result.Should().Be(HResult.S_OK);
            foundName.Should().Be((IntBoolean)true);

            // Find gives back the right casing
            nameSpan.ToString().Should().Be("IFont");
        }

        [Fact]
        public unsafe void FindName()
        {
            HResult result = LoadStdOle2(out ITypeLib typeLib);

            result.Should().Be(HResult.S_OK);
            typeLib.Should().NotBeNull();

            string name = "picture";
            char* nameBuffer = stackalloc char[name.Length + 1];
            Span<char> nameSpan = new Span<char>(nameBuffer, name.Length);
            name.AsSpan().CopyTo(nameSpan);
            nameBuffer[name.Length] = '\0';

            IntPtr* typeInfos = stackalloc IntPtr[1];
            MemberId* memberIds = stackalloc MemberId[1];
            ushort found = 1;
            result = typeLib.FindName(
                nameBuffer,
                lHashVal: 0,
                typeInfos,
                memberIds,
                &found);

            result.Should().Be(HResult.S_OK);
            found.Should().Be(1);
            memberIds[0].Should().Be(new MemberId { Value = -1 });
            typeInfos[0].Should().NotBe(IntPtr.Zero);
            Marshal.Release(typeInfos[0]);

            // Find gives back the right casing
            nameSpan.ToString().Should().Be("Picture");
        }
    }
}
