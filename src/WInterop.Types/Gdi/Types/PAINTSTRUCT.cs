// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd162768.aspx
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public BOOL fErase;
        public RECT rcPaint;
        public BOOL fRestore;
        public BOOL fIncUpdate;
        public unsafe fixed byte rgbReserved[32];
    }
}
