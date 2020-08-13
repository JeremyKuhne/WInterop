// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/aa379261.aspx">LUID</a> structure.
    /// </summary>
    public readonly struct LUID
    {
        public readonly uint LowPart;
        public readonly uint HighPart;
    }
}
