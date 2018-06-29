// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.File.Types
{
    /// <summary>
    /// Gets all of the process IDs using the given file.
    /// </summary>
    public struct FILE_PROCESS_IDS_USING_FILE_INFORMATION
    {
        // Note that this is only documented in the DDK (in wdm.h)

        public uint NumberOfProcessIdsInList;

        private TrailingNativeInt _ProcessIdList;

        /// <summary>
        /// While internal process ids are always the size of an native int they're always returned as
        /// a uint in Win32 APIs.
        /// </summary>
        public ReadOnlySpan<UIntPtr> ProcessIdList => _ProcessIdList.GetBuffer((uint)(NumberOfProcessIdsInList * UIntPtr.Size));
    }
}
