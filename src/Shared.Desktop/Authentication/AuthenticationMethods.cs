// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Authentication.Types;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.SecurityManagement.Types;

namespace WInterop.Authentication
{
    public static partial class AuthenticationMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378299.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public unsafe static extern NTSTATUS LsaOpenPolicy(
                UNICODE_STRING* SystemName,
                LSA_OBJECT_ATTRIBUTES* ObjectAttributes,
                PolicyAccessRights DesiredAccess,
                out LsaHandle PolicyHandle);
        }

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
