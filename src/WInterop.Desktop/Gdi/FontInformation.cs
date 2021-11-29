// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public class FontInformation
{
    public EnumerateLogicalFontExtendedDesignVector FontAttributes;
    public NewTextMetricsExtended TextMetrics;
    public FontTypes FontType;

    public override string ToString()
    {
        string fullName = FontAttributes.EnumLogicalFontEx.FullName.CreateString();
        string style = FontAttributes.EnumLogicalFontEx.Style.CreateString();

        return fullName.EndsWith(style)
            ? $"{fullName} {FontAttributes.EnumLogicalFontEx.Script.CreateString()} ({FontType})"
            : $"{fullName} {style} {FontAttributes.EnumLogicalFontEx.Script.CreateString()} ({FontType})";
    }
}