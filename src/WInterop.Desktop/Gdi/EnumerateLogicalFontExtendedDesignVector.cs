// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <summary>
///  [ENUMLOGFONTEXDVW]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-enumlogfontexdvw
///  https://docs.microsoft.com/openspecs/windows_protocols/ms-emf/595fcaae-bc37-4148-9afe-859e81ca7f76
/// </docs>
public struct EnumerateLogicalFontExtendedDesignVector
{
    public EnumerateLogicalFontExtended EnumLogicalFontEx;
    public DesignVector DesignVector;
}