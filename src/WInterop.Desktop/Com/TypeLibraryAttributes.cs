// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Globalization;

namespace WInterop.Com
{
    /// <summary>
    ///  [TLIBATTR]
    /// </summary>
    public struct TypeLibraryAttributes
    {
        public Guid Guid;
        public LocaleId LocaleId;
        public SystemKind SystemKind;
        public ushort MajorVerNum;
        public ushort MinorVerNum;
        public LibraryFlags LibraryFlags;
    }
}
