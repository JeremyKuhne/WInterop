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
    public class SafeCertificateStoreHandle : SafeHandleZeroIsInvalid
    {
        public SafeCertificateStoreHandle() : base(ownsHandle: true)
        {
        }

        public SafeCertificateStoreHandle(bool ownsHandle) : base(ownsHandle)
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
