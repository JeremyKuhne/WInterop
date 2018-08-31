// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Registry
{
    /// <summary>
    /// [KEY_INFORMATION_CLASS]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff553373.aspx
    public enum KeyInformationClass
    {
        KeyBasicInformation = 0,
        KeyNodeInformation = 1,
        KeyFullInformation = 2,
        KeyNameInformation = 3,
        KeyCachedInformation = 4,
        KeyFlagsInformation = 5,
        KeyVirtualizationInformation = 6,
        KeyHandleTagsInformation = 7,
        MaxKeyInfoClass = 8
    }
}
