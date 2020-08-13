// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;
using WInterop.Storage.Native;

namespace WInterop.Storage
{
    /// <summary>
    ///  Handle for enumerating volumes.
    /// </summary>
    public class FindVolumeHandle : HandleZeroOrMinusOneIsInvalid
    {
        public FindVolumeHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return Imports.FindVolumeClose(handle);
        }
    }
}
