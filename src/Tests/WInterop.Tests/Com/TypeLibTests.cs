// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Com;
using WInterop.Globalization;

namespace ComTests;

public class TypeLibTests
{
    // "00020430-0000-0000-C000-000000000046"
    public static readonly Guid IID_StdOle = new(0x00020430, 0x0000, 0x0000, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46);

    internal static TypeLibrary LoadStdOle2() => Com.LoadRegisteredTypeLibrary(IID_StdOle, 2);

    [Fact]
    public unsafe void LoadFromRegistry()
    {
        using TypeLibrary library = LoadStdOle2();
        library.IsNull.Should().BeFalse();

        uint typeInfoCount = library.GetTypeInfoCount();
        typeInfoCount.Should().Be(42);

        var attributes = library.GetLibraryAttributes();

        attributes.MajorVersion.Should().Be(2);
        attributes.MinorVersion.Should().Be(0);
        attributes.LocaleId.Should().Be((LocaleId)0);
        attributes.LibraryFlags.Should().Be(LibraryFlags.HasDiskImage);
        attributes.Guid.Should().Be(IID_StdOle);

        // Strangely this has started returning Win32 on 64 bit...
        // attributes->SystemKind.Should().Be(Environment.Is64BitProcess ? SystemKind.Win64 : SystemKind.Win32);
    }

    [Fact]
    public unsafe void GetDocumentation()
    {
        using TypeLibrary library = LoadStdOle2();
        library.IsNull.Should().BeFalse();

        uint typeInfoCount = library.GetTypeInfoCount();
        typeInfoCount.Should().Be(42);

        library.GetDocumentation(34, out string name, out string documentation); ;
        name.Should().Be("IPicture");
        documentation.Should().Be("Picture Object");
    }

    [Fact]
    public unsafe void IsName()
    {
        using TypeLibrary library = LoadStdOle2();
        library.IsNull.Should().BeFalse();

        library.IsName("ifont", out string foundName).Should().BeTrue();
        foundName.Should().Be("IFont");
    }

    [Fact]
    public unsafe void FindName()
    {
        using TypeLibrary library = LoadStdOle2();
        library.IsNull.Should().BeFalse();

        var results = library.FindName("picture", 1, out string foundName);

        results.Count.Should().Be(1);

        using var info = results[0].Info;
        results[0].Id.Should().Be(MemberId.Nil);

        foundName.Should().Be("Picture");
    }
}
