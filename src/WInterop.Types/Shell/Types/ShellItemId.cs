// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.Shell.Types
{
    /// <summary>
    /// Simple holder for SHITEMID.
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb759800.aspx
    public struct ShellItemId
    {
        public byte[] Id;
    }
}
