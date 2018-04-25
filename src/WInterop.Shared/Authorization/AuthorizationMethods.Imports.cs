// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;

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


            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379159.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
            public unsafe static extern bool LookupAccountNameW(
                string lpSystemName,
                string lpAccountName,
                SID* Sid,
                ref uint cbSid,
                char* ReferencedDomainName,
                ref uint cchReferencedDomainName,
                out SidNameUse peUse);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379166.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]

            public unsafe static extern bool LookupAccountSidW(
                string lpSystemName,
                ref SID lpSid,
                SafeHandle lpName,
                ref uint cchName,
                SafeHandle lpReferencedDomainName,
                ref uint cchReferencedDomainName,
                out SidNameUse peUse);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446654.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379588.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public unsafe static extern WindowsError SetSecurityInfo(
                SafeHandle handle,
                SecurityObjectType ObjectType,
                SecurityInformation SecurityInfo,
                SID* psidOwner,
                SID* psidGroup,
                ACL* pDacl,
                ACL* pSacl);
        }
    }
}
