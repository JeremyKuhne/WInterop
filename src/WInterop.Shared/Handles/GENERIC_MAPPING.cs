// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446633.aspx
    // ACCESS_MASK
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx
    public struct GENERIC_MAPPING
    {
        public uint GenericRead;
        public uint GenericWrite;
        public uint GenericExecute;
        public uint GenericAll;
    }
}
