// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.Types;

namespace WInterop.Compression.Types
{
    public class FdiHandle : HandleZeroOrMinusOneIsInvalid
    {
        public FdiHandle() : base(ownsHandle: true) { }

        protected override bool ReleaseHandle()
        {
            return CompressionMethods.Imports.FDIDestroy(handle);
        }
    }
}
