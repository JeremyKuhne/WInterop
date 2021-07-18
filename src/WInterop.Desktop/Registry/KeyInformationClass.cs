// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Registry
{
    /// <summary>
    ///  [KEY_INFORMATION_CLASS]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff553373.aspx
    public enum KeyInformationClass
    {
        BasicInformation = 0,
        NodeInformation = 1,
        FullInformation = 2,
        NameInformation = 3,
        CachedInformation = 4,
        FlagsInformation = 5,
        VirtualizationInformation = 6,
        HandleTagsInformation = 7
    }
}