// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi
{
    /// <summary>
    ///  [ENUMLOGFONTEX]
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct EnumerateLogicalFontExtended
    {
        public LogicalFont LogicalFont;
        private FixedString.Size64 _elfFullName;
        private FixedString.Size32 _elfStyle;
        private FixedString.Size32 _elfScript;
        public Span<char> FullName => _elfFullName.Buffer;
        public Span<char> Style => _elfStyle.Buffer;
        public Span<char> Script => _elfScript.Buffer;
    }
}
