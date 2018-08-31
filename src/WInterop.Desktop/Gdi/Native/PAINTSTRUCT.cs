// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Unsafe
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagpaintstruct
    public readonly struct PAINTSTRUCT
    {
        public readonly HDC hdc;
        public readonly BOOL fErase;
        public readonly RECT rcPaint;
        public readonly BOOL fRestore;
        public readonly BOOL fIncUpdate;
        private readonly unsafe FixedByte.Size32 _rgbReserved;
        public Span<byte> rgbReserved => _rgbReserved.Buffer;

        /// <summary>
        /// Used for calling EndPaint without holding onto the entire paintstruct.
        /// </summary>
        public PAINTSTRUCT(HDC hdc)
        {
            // Windows only uses the HDC out of the PAINTSTRUCT in EndPaint().
            // We take advantage of this so that we can avoid carrying this large
            // struct in HDC wrapping structs.

            this = default;
            this.hdc = hdc;
        }
    }
}
