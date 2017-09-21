// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    public class FontInformation
    {
        public ENUMLOGFONTEXDV FontAttributes;
        public NEWTEXTMETRICEX TextMetrics;
        public FontTypes FontType;

        public override string ToString()
        {
            string fullName = FontAttributes.elfEnumLogfontEx.elfFullName.ToNullTerminatedString();
            string style = FontAttributes.elfEnumLogfontEx.elfStyle.ToNullTerminatedString();

            return fullName.EndsWith(style)
                ? $"{fullName} {FontAttributes.elfEnumLogfontEx.elfScript.ToNullTerminatedString()} ({FontType})"
                : $"{fullName} {style} {FontAttributes.elfEnumLogfontEx.elfScript.ToNullTerminatedString()} ({FontType})";
        }
    }
}
