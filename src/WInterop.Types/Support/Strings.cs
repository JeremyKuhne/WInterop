// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support
{
    public static class Strings
    {
        /// <summary>
        /// Copy up to the specified number of characters to the designated buffer with an
        /// additional null terminator if not otherwise specified.
        /// </summary>
        /// <param name="value">The string to copy from.</param>
        /// <param name="destination">The buffer to copy to.</param>
        /// <param name="maxCharacters">Max number of characters to copy.</param>
        /// <param name="nullTerminate">Add a null to the end of the string (not counted in <paramref name="maxCharacters"/>).</param>
        public unsafe static void StringToBuffer(string value, char* destination, int maxCharacters, bool nullTerminate = true)
        {
            int count = maxCharacters;
            if (count == 0 || value == null || value.Length == 0)
            {
                if (nullTerminate)
                    *destination = '\0';
                return;
            }

            if (count < 0)
                count = value.Length;
            else if (value.Length < count)
                count = value.Length;

            fixed (char* start = value)
            {
                Buffer.MemoryCopy(
                    source: start,
                    destination: destination,
                    destinationSizeInBytes: count * sizeof(char),
                    sourceBytesToCopy: count * sizeof(char)
                );
            }

            if (nullTerminate)
            {
                destination += count = '\0';
            }
        }

        /// <summary>
        /// Single allocation replacement of a single character in a string.
        /// </summary>
        /// <exception cref="ArgumentNullException">value is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">index is not within the bounds of the string.</exception>
        /// <returns>A copy of the given string with the specified character replaced.</returns>
        public unsafe static string ReplaceChar(string value, int index, char newChar)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            if (index < 0 || index >= value.Length) throw new ArgumentOutOfRangeException(nameof(index));

            fixed (char* v = value)
            {
                string newString = new string(v, 0, value.Length);
                fixed (char* n = newString)
                    n[index] = newChar;
                return newString;
            }
        }
    }
}
