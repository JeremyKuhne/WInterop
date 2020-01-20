// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using System.Runtime.InteropServices;

namespace WInterop.Com
{
    /// <summary>
    ///  Wraps a native BSTR.
    /// </summary>
    /// <remarks>
    ///  <see cref="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/automat/bstr"/>
    /// </remarks>
    public ref struct BasicString
    {
        private IntPtr _bstr;

        public BasicString(string value)
        {
            _bstr = Marshal.StringToBSTR(value);
        }

        /// <summary>
        ///  True if the BSTR is null.
        /// </summary>
        public bool IsNull => _bstr == IntPtr.Zero;

        /// <summary>
        ///  Returns the length of the native BSTR.
        /// </summary>
        public unsafe uint Length
            => _bstr == IntPtr.Zero ? 0 :  *(((uint*)_bstr) - 1) / 2;

        /// <summary>
        ///  Returns a span on the native BSTR data.
        /// </summary>
        public unsafe ReadOnlySpan<char> String
            => _bstr == IntPtr.Zero
                ? ReadOnlySpan<char>.Empty
                : new ReadOnlySpan<char>((void*)_bstr, checked((int)Length));

        /// <summary>
        ///  Frees the native BSTR.
        /// </summary>
        public void Free()
        {
            if (_bstr != IntPtr.Zero)
            {
                Marshal.FreeBSTR(_bstr);
                _bstr = IntPtr.Zero;
            }
        }

        /// <summary>
        ///  Creates a string and frees the underlying native BSTR.
        /// </summary>
        /// <returns>A string or <see langword="null"/> if the underlying string is null.</returns>
        public string? ToStringAndFree()
        {
            if (_bstr == IntPtr.Zero)
                return null;

            return String.ToString();
        }

        public void Dispose()
        {
            Free();
        }
    }
}
