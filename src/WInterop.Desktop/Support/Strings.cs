// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Text;

namespace WInterop.Support
{
    public static class Strings
    {
        /// <summary>
        ///  Single allocation replacement of a single character in a string.
        /// </summary>
        /// <exception cref="ArgumentNullException">value is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">index is not within the bounds of the string.</exception>
        /// <returns>A copy of the given string with the specified character replaced.</returns>
        public static unsafe string ReplaceChar(string value, int index, char newChar)
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

        public static ReadOnlyMemory<char> GetChunk(this StringBuilder builder)
        {
            var enumerator = builder.GetChunks();
            enumerator.MoveNext();
            return enumerator.Current;
        }
    }
}
