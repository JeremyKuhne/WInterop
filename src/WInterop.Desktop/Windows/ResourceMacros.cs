// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    public static class ResourceMacros
    {
        // How can I tell that somebody used the MAKEINTRESOURCE macro to smuggle an integer inside a pointer?
        // https://blogs.msdn.microsoft.com/oldnewthing/20130925-00/?p=3123/

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648029.aspx
        public static IntPtr MAKEINTRESOURCE(ushort wInteger)
        {
            return (IntPtr)wInteger;
        }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648029.aspx
        public static bool IS_INTRESOURCE(IntPtr p)
        {
            return ((ulong)p) >> 16 == 0;
        }
    }
}
