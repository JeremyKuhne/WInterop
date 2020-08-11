// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Native
{
    // https://docs.microsoft.com/windows/desktop/api/winuser/ns-winuser-tagpaintstruct
    public readonly struct PAINTSTRUCT
    {
        public readonly HDC hdc;
        public readonly Boolean32 fErase;
        public readonly Rect rcPaint;
        public readonly Boolean32 fRestore;
        public readonly Boolean32 fIncUpdate;
        private readonly unsafe FixedByte.Size32 _rgbReserved;
        public Span<byte> rgbReserved => _rgbReserved.Buffer;

        /// <summary>
        ///  Used for calling EndPaint without holding onto the entire paintstruct.
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
