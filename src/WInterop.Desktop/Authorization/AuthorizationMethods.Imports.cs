// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;
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
            // Advapi (Advanced API) provides Win32 security and registry calls and as such hosts most
            // of the Authorization APIs.
            //
            // Advapi usually calls the NT Marta provider (Windows NT Multiple Access RouTing Authority).
            // https://msdn.microsoft.com/en-us/library/aa939264.aspx


            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa375202.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL AdjustTokenPrivileges(
                AccessToken TokenHandle,
                BOOL DisableAllPrivileges,
                ref TOKEN_PRIVILEGES NewState,
                uint BufferLength,
                out TOKEN_PRIVILEGES PreviousState,
                out uint ReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446671.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern BOOL GetTokenInformation(
                AccessToken TokenHandle,
                TOKEN_INFORMATION_CLASS TokenInformationClass,
                void* TokenInformation,
                uint TokenInformationLength,
                out uint ReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379176.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL LookupPrivilegeNameW(
                IntPtr lpSystemName,
                ref LUID lpLuid,
                SafeHandle lpName,
                ref uint cchName);

            // https://msdn.microsoft.com/en-us/library/aa379180.aspx
            [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL LookupPrivilegeValueW(
                string lpSystemName,
                string lpName,
                out LUID lpLuid);

            // https://msdn.microsoft.com/en-us/library/aa379304.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern BOOL PrivilegeCheck(
                AccessToken ClientToken,
                PRIVILEGE_SET* RequiredPrivileges,
                out BOOL pfResult);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379590.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL SetThreadToken(
                IntPtr Thread,
                AccessToken Token);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379317.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL RevertToSelf();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446617.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL DuplicateTokenEx(
                AccessToken hExistingToken,
                AccessTokenRights dwDesiredAccess,
                IntPtr lpTokenAttributes,
                SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
                TOKEN_TYPE TokenType,
                ref AccessToken phNewToken);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379295.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL OpenProcessToken(
                IntPtr ProcessHandle,
                AccessTokenRights DesiredAccesss,
                out AccessToken TokenHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379296.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern bool OpenThreadToken(
                SafeThreadHandle ThreadHandle,
                AccessTokenRights DesiredAccess,
                BOOL OpenAsSelf,
                out AccessToken TokenHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379166.aspx
            // LookupAccountSid

            // https://msdn.microsoft.com/en-us/library/windows/desktop/mt779143.aspx
            // The docs claim that it is in Advapi.dll, but it actually lives in sechost.dll
            [DllImport(ApiSets.api_ms_win_security_lsalookup_l1_1_0, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL LookupAccountSidLocalW(
                ref SID lpSid,
                SafeHandle lpName,
                ref uint cchName,
                SafeHandle lpReferencedDomainName,
                ref uint cchReferencedDomainName,
                out SidNameUse peUse);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379159.aspx
            // LookupAccountName

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446585.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern BOOL CreateWellKnownSid(
                WELL_KNOWN_SID_TYPE WellKnownSidType,
                SID* DomainSid,
                SID* pSid,
                ref uint cbSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379151.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public unsafe static extern BOOL IsValidSid(
                ref SID pSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379154.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public unsafe static extern BOOL IsWellKnownSid(
                ref SID pSid,
                WELL_KNOWN_SID_TYPE WellKnownSidType);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376399.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOL ConvertSidToStringSidW(
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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376404.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern BOOL CopySid(
                uint nDestinationSidLength,
                out SID pDestinationSid,
                SID* pSourceSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa374951.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern BOOL AddAccessAllowedAceEx(
                ACL* pAcl,
                uint dwAceRevision,
                // This is AceInheritence
                uint AceFlags,
                ACCESS_MASK AccessMask,
                SID* pSid);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446654.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern WindowsError GetSecurityInfo(
                SafeHandle handle,
                SecurityObjectType ObjectType,
                SecurityInformation SecurityInfo,
                SID** ppsidOwner = null,
                SID** ppsidGroup = null,
                ACL** ppDacl = null,
                ACL** ppSacl = null,
                SECURITY_DESCRIPTOR** ppSecurityDescriptor = null
            );
        }
    }
}
