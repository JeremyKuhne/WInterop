// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Buffers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Avoid using this directly. Create wrappers that can ensure proper length checks.
    /// </summary>
    public static class BufferHelper
    {
        /// <summary>
        /// Splits a null terminated unicode string list. The final string is followed by a second null.
        /// This is a common pattern Windows uses. Usually you should use StringBuffer and split on null. There
        /// are some cases (such as GetEnvironmentStrings) where Windows returns a handle to a buffer it allocated
        /// with no length.
        /// </summary>
        public static unsafe IEnumerable<string> SplitNullTerminatedStringList(IntPtr handle)
        {
            if (handle == IntPtr.Zero) throw new ArgumentNullException(nameof(handle));

            var strings = new List<string>();

            char* start = (char*)handle;
            char* current = start;

            for (uint i = 0; ; i++)
            {
                if ('\0' == *current)
                {
                    // Split
                    strings.Add(new string(value: start, startIndex: 0, length: checked((int)(current - start))));
                    start = current + 1;

                    if ('\0' == *start) break;
                }

                current++;
            }

            return strings;
        }
    }
}
