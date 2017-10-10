// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// 128 bit file identifier.
    /// </summary>
    public struct FILE_ID_128
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh965605.aspx

        private FixedByte.Size16 Identifier;
    }
}
