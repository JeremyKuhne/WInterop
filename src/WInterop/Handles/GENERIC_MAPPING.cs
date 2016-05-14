// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Handles
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446633.aspx
    // ACCESS_MASK
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct GENERIC_MAPPING
    {
        uint GenericRead;
        uint GenericWrite;
        uint GenericExecute;
        uint GenericAll;
    }
}
