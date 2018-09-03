// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.ProcessAndThreads;
using WInterop.SafeString.Unsafe;

namespace WInterop.Security.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // Advapi (Advanced API) provides Win32 security and registry calls and as such hosts most
        // of the Authorization APIs.
        //
        // Advapi usually calls the NT Marta provider (Windows NT Multiple Access RouTing Authority).
        // https://msdn.microsoft.com/en-us/library/aa939264.aspx

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379304.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern Boolean32 PrivilegeCheck(
            AccessToken ClientToken,
            PrivilegeSet* RequiredPrivileges,
            out Boolean32 pfResult);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378612.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern Boolean32 ImpersonateLoggedOnUser(
            AccessToken hToken);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378610.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern Boolean32 ImpersonateAnonymousToken(
            ThreadHandle thread);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379317.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public static extern Boolean32 RevertToSelf();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446583.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public static unsafe extern Boolean32 CreateRestrictedToken(
            AccessToken ExistingTokenHandle,
            uint Flags,
            uint DisableSidCount,
            SID_AND_ATTRIBUTES* SidsToDisable,
            uint DeletePrivilegeCount,
            LuidAndAttributes* PrivilegesToDelete,
            uint RestrictedSidCount,
            SID_AND_ATTRIBUTES* SidsToRestrict,
            out AccessToken NewTokenHandle);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378299.aspx
        [DllImport(Libraries.Advapi32, ExactSpelling = true)]
        public unsafe static extern NTStatus LsaOpenPolicy(
            UNICODE_STRING* SystemName,
            LSA_OBJECT_ATTRIBUTES* ObjectAttributes,
            PolicyAccessRights DesiredAccess,
            out LsaHandle PolicyHandle);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
        [DllImport(Libraries.Advapi32, ExactSpelling = true)]
        public static extern WindowsError LsaNtStatusToWinError(NTStatus Status);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721787.aspx
        [DllImport(Libraries.Advapi32, ExactSpelling = true)]
        public static extern NTStatus LsaClose(
            IntPtr ObjectHandle);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721796.aspx
        [DllImport(Libraries.Advapi32, ExactSpelling = true)]
        public static extern NTStatus LsaFreeMemory(
            IntPtr ObjectHandle);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721790.aspx
        [DllImport(Libraries.Advapi32, ExactSpelling = true)]
        public static extern NTStatus LsaEnumerateAccountRights(
            LsaHandle PolicyHandle,
            in SID AccountSid,
            out LsaMemoryHandle UserRights,
            out uint CountOfRights);

        // This isn't allowed in Windows Store apps, but is exactly the same as
        // calling LookupAccountSidW with a null or empty computer name.

        // https://msdn.microsoft.com/en-us/library/windows/desktop/mt779143.aspx
        // The docs claim that it is in Advapi.dll, but it actually lives in sechost.dll
        //[DllImport(ApiSets.api_ms_win_security_lsalookup_l1_1_0, SetLastError = true, ExactSpelling = true)]
        //public static extern BOOL LookupAccountSidLocalW(
        //    in SID lpSid,
        //    SafeHandle lpName,
        //    ref uint cchName,
        //    SafeHandle lpReferencedDomainName,
        //    ref uint cchReferencedDomainName,
        //    out SidNameUse peUse);
    }
}
