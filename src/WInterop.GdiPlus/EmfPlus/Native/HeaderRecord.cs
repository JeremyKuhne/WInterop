// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus.EmfPlus.Native
{
    public struct HeaderRecord
    {
        public int Version;        // Version of the file
        public int EmfPlusFlags;   // flags (display and non-dual)
        public int LogicalDpiX;    // DpiX of referenceHdc
        public int LogicalDpiY;    // DpiY of referenceHdc
    }
}
