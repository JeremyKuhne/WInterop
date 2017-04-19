// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Authentication.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.SecurityManagement.DataTypes;

namespace WInterop.Authentication
{
    public static partial class AuthenticationMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378299.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public unsafe static extern NTSTATUS LsaOpenPolicy(
                UNICODE_STRING* SystemName,
                LSA_OBJECT_ATTRIBUTES* ObjectAttributes,
                PolicyAccessRights DesiredAccess,
                out SafeLsaHandle PolicyHandle);
        }

        public static SafeLsaHandle LsaOpenLocalPolicy(PolicyAccessRights access)
        {
            LSA_OBJECT_ATTRIBUTES attributes = new LSA_OBJECT_ATTRIBUTES();
            SafeLsaHandle handle;
            NTSTATUS status;

            unsafe
            {
                status = Direct.LsaOpenPolicy(null, &attributes, access, out handle);
            }

            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);

            return handle;
        }
    }
}
