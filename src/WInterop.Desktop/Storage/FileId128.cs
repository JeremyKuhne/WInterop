// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    ///  128 bit file identifier. [FILE_ID_128]
    /// </summary>
    public struct FileId128
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh965605.aspx

        public FixedByte.Size16 Identifier;
    }
}
