// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd162618.aspx
    public delegate int EnumerateFontFamilyExtendedProcedure(
        ref EnumerateLogicalFontExtendedDesignVector lpelfe,
        ref NewTextMetricsExtended lpntme,
        FontTypes fontType,
        LParam lParam);
}