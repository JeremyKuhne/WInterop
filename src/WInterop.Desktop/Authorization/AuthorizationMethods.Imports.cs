// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Authorization.Types;
using WInterop.Handles.Types;
using WInterop.MemoryManagement.Types;
using WInterop.ProcessAndThreads.Types;

namespace WInterop.Authorization
{
    public static partial class AuthorizationMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa375202.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AdjustTokenPrivileges(
                TokenHandle TokenHandle,
                [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
                ref TOKEN_PRIVILEGES NewState,
                uint BufferLength,
                out TOKEN_PRIVILEGES PreviousState,
                out uint ReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446671.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public unsafe static extern bool GetTokenInformation(
                TokenHandle TokenHandle,
                TOKEN_INFORMATION_CLASS TokenInformationClass,
                void* TokenInformation,
                uint TokenInformationLength,
                out uint ReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379176.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool LookupPrivilegeNameW(
                IntPtr lpSystemName,
                ref LUID lpLuid,
                SafeHandle lpName,
                ref uint cchName);

            // https://msdn.microsoft.com/en-us/library/aa379180.aspx
            [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool LookupPrivilegeValueW(
                string lpSystemName,
                string lpName,
                ref LUID lpLuid);

            // https://msdn.microsoft.com/en-us/library/aa379304.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PrivilegeCheck(
                TokenHandle ClientToken,
                ref PRIVILEGE_SET RequiredPrivileges,
                [MarshalAs(UnmanagedType.Bool)] out bool pfResult);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379590.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetThreadToken(
                IntPtr Thread,
                TokenHandle Token);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379317.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RevertToSelf();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446617.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DuplicateTokenEx(
                TokenHandle hExistingToken,
                TokenRights dwDesiredAccess,
                IntPtr lpTokenAttributes,
                SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
                TOKEN_TYPE TokenType,
                ref TokenHandle phNewToken);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379295.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool OpenProcessToken(
                IntPtr ProcessHandle,
                TokenRights DesiredAccesss,
                out TokenHandle TokenHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379296.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool OpenThreadToken(
                SafeThreadHandle ThreadHandle,
                TokenRights DesiredAccess,
                [MarshalAs(UnmanagedType.Bool)] bool OpenAsSelf,
                out TokenHandle TokenHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379166.aspx
            // LookupAccountSid

            // https://msdn.microsoft.com/en-us/library/windows/desktop/mt779143.aspx
            // The docs claim that it is in Advapi.dll, but it actually lives in sechost.dll
            [DllImport(ApiSets.api_ms_win_security_lsalookup_l1_1_0, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool LookupAccountSidLocalW(
                ref SID lpSid,
                SafeHandle lpName,
                ref uint cchName,
                SafeHandle lpReferencedDomainName,
                ref uint cchReferencedDomainName,
                out SID_NAME_USE peUse);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379159.aspx
            // LookupAccountName

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446585.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public unsafe static extern bool CreateWellKnownSid(
                WELL_KNOWN_SID_TYPE WellKnownSidType,
                SID* DomainSid,
                SID* pSid,
                ref uint cbSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379151.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public unsafe static extern bool IsValidSid(
                ref SID pSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379154.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public unsafe static extern bool IsWellKnownSid(
                ref SID pSid,
                WELL_KNOWN_SID_TYPE WellKnownSidType);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376399.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ConvertSidToStringSidW(
                ref SID Sid,
                out LocalHandle StringSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446658.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern byte* GetSidSubAuthorityCount(
                ref SID pSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446657.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern uint* GetSidSubAuthority(
                ref SID pSid,
                uint nSubAuthority);

            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool CopySid(
                uint nDestinationSidLength,
                out SID pDestinationSid,
                SID* pSourceSid);
        }
    }
}
