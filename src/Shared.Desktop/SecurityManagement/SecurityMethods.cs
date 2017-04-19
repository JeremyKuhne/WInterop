// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Authorization.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.SecurityManagement.DataTypes;
using WInterop.Support.Buffers;

namespace WInterop.SecurityManagement
{
    public static partial class SecurityMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public static extern WindowsError LsaNtStatusToWinError(NTSTATUS Status);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721787.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public static extern NTSTATUS LsaClose(
                IntPtr ObjectHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721796.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public static extern NTSTATUS LsaFreeMemory(
                IntPtr ObjectHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721790.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public static extern NTSTATUS LsaEnumerateAccountRights(
                SafeLsaHandle PolicyHandle,
                ref SID AccountSid,
                out SafeLsaMemoryHandle UserRights,
                out uint CountOfRights);
        }

        /// <summary>
        /// Convert an NTSTATUS to a Windows error code (returns ERROR_MR_MID_NOT_FOUND if unable to find an error)
        /// </summary>
        public static WindowsError NtStatusToWinError(NTSTATUS status)
        {
            return Direct.LsaNtStatusToWinError(status);
        }

        public static void LsaClose(IntPtr handle)
        {
            NTSTATUS status = Direct.LsaClose(handle);
            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);
        }

        public static void LsaFreeMemory(IntPtr handle)
        {
            NTSTATUS status = Direct.LsaFreeMemory(handle);
            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);
        }

        /// <summary>
        /// Enumerates rights explicitly given to the specified SID. If the given SID
        /// doesn't have any directly applied rights, returns an empty collection.
        /// </summary>
        public static IEnumerable<string> LsaEnumerateAccountRights(SafeLsaHandle policyHandle, ref SID sid)
        {
            NTSTATUS status = Direct.LsaEnumerateAccountRights(policyHandle, ref sid, out var rightsBuffer, out uint rightsCount);
            switch (status)
            {
                case NTSTATUS.STATUS_OBJECT_NAME_NOT_FOUND:
                    return Enumerable.Empty<string>();
                case NTSTATUS.STATUS_SUCCESS:
                    break;
                default:
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);
            }

            List<string> rights = new List<string>();
            Reader reader = new Reader(rightsBuffer);
            for (int i = 0; i < rightsCount; i++)
                rights.Add(reader.ReadUNICODE_STRING());

            return rights;
        }
    }
}
