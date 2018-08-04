// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling;
using WInterop.Security.Types;

namespace WInterop.Authentication
{
    public static partial class AuthenticationMethods
    {
        public static LsaHandle LsaOpenLocalPolicy(PolicyAccessRights access)
        {
            LSA_OBJECT_ATTRIBUTES attributes = new LSA_OBJECT_ATTRIBUTES();
            LsaHandle handle;
            NTSTATUS status;

            unsafe
            {
                status = Imports.LsaOpenPolicy(null, &attributes, access, out handle);
            }

            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);

            return handle;
        }
    }
}
