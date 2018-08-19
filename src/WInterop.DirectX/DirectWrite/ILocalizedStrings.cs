// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    /// Represents a collection of strings indexed by locale name. [IDWriteLocalizedStrings]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_IDWriteLocalizedStrings),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ILocalizedStrings
    {
        /// <summary>
        /// Gets the number of language/string pairs.
        /// </summary>
        [PreserveSig]
        uint GetCount();

        /// <summary>
        /// Gets the index of the item with the specified locale name.
        /// </summary>
        /// <param name="localeName">Locale name to look for.</param>
        /// <param name="index">Receives the zero-based index of the locale name/string pair.</param>
        /// <returns>TRUE if the locale name exists or FALSE if not.</returns>
        BOOL FindLocaleName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string localeName,
            out uint index);

        /// <summary>
        /// Gets the length in characters (not including the null terminator) of the locale name with the specified index.
        /// </summary>
        /// <param name="index">Zero-based index of the locale name.</param>
        /// <returns>Receives the length in characters, not including the null terminator.</returns>
        uint GetLocaleNameLength(uint index);

        /// <summary>
        /// Copies the locale name with the specified index to the specified array.
        /// </summary>
        /// <param name="index">Zero-based index of the locale name.</param>
        /// <param name="localeName">Character array that receives the locale name.</param>
        /// <param name="size">Size of the array in characters. The size must include space for the terminating
        /// null character.</param>
        unsafe void GetLocaleName(
            uint index,
            char* localeName,
            uint size);

        /// <summary>
        /// Gets the length in characters (not including the null terminator) of the string with the specified index.
        /// </summary>
        /// <param name="index">Zero-based index of the string.</param>
        /// <returns>
        /// The length in characters, not including the null terminator.
        /// </returns>
        uint GetStringLength(uint index);

        /// <summary>
        /// Copies the string with the specified index to the specified array.
        /// </summary>
        /// <param name="index">Zero-based index of the string.</param>
        /// <param name="stringBuffer">Character array that receives the string.</param>
        /// <param name="size">Size of the array in characters. The size must include space for the terminating
        /// null character.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        unsafe void GetString(
            uint index,
            char* stringBuffer,
            uint size);
    }
}
