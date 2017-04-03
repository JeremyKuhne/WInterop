// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.DataTypes
{
    /// <summary>
    /// Wrapper for a native certificate store handle.
    /// </summary>
    public class CertificateStoreHandle : SafeHandleZeroIsInvalid
    {
        public CertificateStoreHandle() : base(ownsHandle: true)
        {
        }

        public CertificateStoreHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            Cryptography.CryptoMethods.CloseStore(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
