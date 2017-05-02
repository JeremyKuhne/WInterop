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
    }
}
