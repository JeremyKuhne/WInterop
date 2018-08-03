// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.Types;

namespace WInterop.Storage
{
    /// <summary>
    /// Handle for enumerating volume mount points.
    /// </summary>
    public class FindVolumeMountPointHandle : HandleZeroOrMinusOneIsInvalid
    {
        public FindVolumeMountPointHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return StorageMethods.Imports.FindVolumeMountPointClose(handle);
        }
    }
}
