// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Globalization;

namespace WInterop.Com;

/// <summary>
///  [TLIBATTR]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/oaidl/ns-oaidl-tlibattr
/// </docs>
public struct TypeLibraryAttributes
{
    public Guid Guid;
    public LocaleId LocaleId;
    public SystemKind SystemKind;
    public ushort MajorVersion;
    public ushort MinorVersion;
    public LibraryFlags LibraryFlags;
}