// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  [ENUMLOGFONTEXDVW]
    /// </summary>
    /// <msdn>https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-tagenumlogfontexdvw</msdn>
    public struct EnumerateLogicalFontExtendedDesignVector
    {
        public EnumerateLogicalFontExtended EnumLogicalFontEx;
        public DesignVector DesignVector;
    }
}
