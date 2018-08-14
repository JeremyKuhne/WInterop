// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Gdi
{
    public class FontInformation
    {
        public ENUMLOGFONTEXDV FontAttributes;
        public NewTextMetricsExtended TextMetrics;
        public FontTypes FontType;

        public override string ToString()
        {
            string fullName = FontAttributes.elfEnumLogfontEx.elfFullName.CreateString();
            string style = FontAttributes.elfEnumLogfontEx.elfStyle.CreateString();

            return fullName.EndsWith(style)
                ? $"{fullName} {FontAttributes.elfEnumLogfontEx.elfScript.CreateString()} ({FontType})"
                : $"{fullName} {style} {FontAttributes.elfEnumLogfontEx.elfScript.CreateString()} ({FontType})";
        }
    }
}
