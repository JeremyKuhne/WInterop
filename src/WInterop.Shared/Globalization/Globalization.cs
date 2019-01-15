// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Windows;
using WInterop.Globalization.Unsafe;

namespace WInterop.Globalization
{
    public static partial class Globalization
    {
        public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

        /// <summary>
        /// The sort handle for the invariant culture.
        /// </summary>
        public static LParam InvariantSortHandle = GetSortHandle("");

        /// <summary>
        /// Compare the given strings. Note that String.CompareOrdinal can be faster than this.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CompareStringOrdinal(string first, string second, bool ignoreCase = false)
        {
            if (first == second)
                return 0;
            else if (first == null)
                return -1;
            else if (second == null)
                return 1;

            return CompareStringOrdinal(first.AsSpan(), second.AsSpan(), ignoreCase);
        }

        /// <summary>
        /// Compare the given strings.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe int CompareStringOrdinal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, bool ignoreCase = false)
        {
            int result = Imports.CompareStringOrdinal(
                ref MemoryMarshal.GetReference(first),
                first.Length,
                ref MemoryMarshal.GetReference(second),
                second.Length,
                ignoreCase);

            if (result == 0)
                throw Error.GetExceptionForLastError();

            // CSTR_LESS_THAN            1           // string 1 less than string 2
            // CSTR_EQUAL                2           // string 1 equal to string 2
            // CSTR_GREATER_THAN         3           // string 1 greater than string 2

            return result - 2;
        }

        /// <summary>
        /// Convert the given string to to upper case.
        /// </summary>
        public static unsafe void ToUpperInvariant(char* input, int inputLength, char* output, int outputLength)
        {
            if (Imports.LCMapStringEx(null, LocaleMapFlags.UpperCase, input, inputLength, output, outputLength, null, null, InvariantSortHandle) == 0)
                throw Error.GetExceptionForLastError();
        }

        /// <summary>
        /// Convert the given string to lower case.
        /// </summary>
        public static unsafe void ToLowerInvariant(char* input, int inputLength, char* output, int outputLength)
        {
            if (Imports.LCMapStringEx(null, LocaleMapFlags.LowerCase, input, inputLength, output, outputLength, null, null, InvariantSortHandle) == 0)
                throw Error.GetExceptionForLastError();
        }

        /// <summary>
        /// Convert the given character to upper case.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe char ToUpperInvariant(char value)
        {
            if (Imports.LCMapStringEx(null, LocaleMapFlags.UpperCase, &value, 1, &value, 1, null, null, 0) == 0)
                throw Error.GetExceptionForLastError();
            return value;
        }

        /// <summary>
        /// Convert the given character to lower case.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe char ToLowerInvariant(char value)
        {
            if (Imports.LCMapStringEx(null, LocaleMapFlags.LowerCase, &value, 1, &value, 1, null, null, 0) == 0)
                throw Error.GetExceptionForLastError();
            return value;
        }

        /// <summary>
        /// Converts the content of the given string to upper case IN PLACE.
        /// Use with great care as strings are normally considered immutable.
        /// </summary>
        public static unsafe void ToUpperInvariantUnsafe(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            fixed (char* v = value)
                ToUpperInvariant(v, value.Length, v, value.Length);
        }

        /// <summary>
        /// Converts the content of the given string to lower case IN PLACE.
        /// Use with great care as strings are normally considered immutable.
        /// </summary>
        public static unsafe void ToLowerInvariantUnsafe(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            fixed (char* v = value)
                ToLowerInvariant(v, value.Length, v, value.Length);
        }

        /// <summary>
        /// Gets the sort handle for the given locale.
        /// </summary>
        public static unsafe LParam GetSortHandle(string locale)
        {
            LParam sortHandle;
            fixed (char* l = locale)
                if (Imports.LCMapStringEx(l, LocaleMapFlags.SortHandle, null, 0, (char*)&sortHandle, sizeof(LParam), null, null, 0) == 0)
                    throw Error.GetExceptionForLastError();
            return sortHandle;
        }

        /// <summary>
        /// Gets the current Windows ANSI code page.
        /// </summary>
        public static uint GetAnsiCodePage() => Imports.GetACP();
    }
}
