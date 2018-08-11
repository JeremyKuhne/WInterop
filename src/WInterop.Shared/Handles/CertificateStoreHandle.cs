// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.Handles
{
    /// <summary>
    /// Wrapper for a native certificate store handle.
    /// </summary>
    public class CertificateStoreHandle : HandleZeroIsInvalid
    {
        public CertificateStoreHandle() : base(ownsHandle: true)
        {
        }

        public CertificateStoreHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            return Support.Internal.Imports.CertCloseStore(handle, dwFlags: 0);
        }
    }
}
