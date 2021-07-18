// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;
using WInterop.Storage.Native;

namespace WInterop.Storage
{
    /// <summary>
    ///  Handle for enumerating volume mount points.
    /// </summary>
    public class FindVolumeMountPointHandle : HandleZeroOrMinusOneIsInvalid
    {
        public FindVolumeMountPointHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return StorageImports.FindVolumeMountPointClose(handle);
        }
    }
}