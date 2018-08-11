// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379560.aspx
    public struct SECURITY_ATTRIBUTES
    {
        public uint nLength;
        public unsafe SECURITY_DESCRIPTOR* lpSecurityDescriptor;
        public BOOL bInheritHandle;
    }
}
