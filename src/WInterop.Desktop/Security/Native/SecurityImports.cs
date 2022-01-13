// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Memory;
using WInterop.ProcessAndThreads;
using WInterop.SafeString.Native;

namespace WInterop.Security.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class SecurityImports
{
    // Advapi (Advanced API) provides Win32 security and registry calls and as such hosts most
    // of the Authorization APIs.
    //
    // Advapi usually calls the NT Marta provider (Windows NT Multiple Access RouTing Authority).
    // https://msdn.microsoft.com/en-us/library/aa939264.aspx

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379304.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe IntBoolean PrivilegeCheck(
        AccessToken ClientToken,
        PrivilegeSet* RequiredPrivileges,
        out IntBoolean pfResult);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378612.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe IntBoolean ImpersonateLoggedOnUser(
        AccessToken hToken);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378610.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe IntBoolean ImpersonateAnonymousToken(
        ThreadHandle thread);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379317.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern IntBoolean RevertToSelf();

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa378299.aspx
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe NTStatus LsaOpenPolicy(
        SafeString.Native.UNICODE_STRING* SystemName,
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
    public static extern unsafe NTStatus LsaEnumerateAccountRights(
        LsaHandle PolicyHandle,
        SID* AccountSid,
        out LsaMemoryHandle UserRights,
        out uint CountOfRights);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376399.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static unsafe extern IntBoolean ConvertSidToStringSidW(
        SID* Sid,
        out LocalHandle StringSid);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379159.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern unsafe bool LookupAccountNameW(
        string? lpSystemName,
        string lpAccountName,
        SID* Sid,
        ref uint cbSid,
        char* ReferencedDomainName,
        ref uint cchReferencedDomainName,
        out SidNameUse peUse);

    // https://docs.microsoft.com/en-us/windows/desktop/api/aclapi/nf-aclapi-getsecurityinfo
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe WindowsError GetSecurityInfo(
        SafeHandle handle,
        ObjectType ObjectType,
        SecurityInformation SecurityInfo,
        SID** ppsidOwner = null,
        SID** ppsidGroup = null,
        ACL** ppDacl = null,
        ACL** ppSacl = null,
        SECURITY_DESCRIPTOR** ppSecurityDescriptor = null);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379296.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern bool OpenThreadToken(
        ThreadHandle ThreadHandle,
        AccessTokenRights DesiredAccess,
        IntBoolean OpenAsSelf,
        out AccessToken TokenHandle);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379295.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern IntBoolean OpenProcessToken(
        IntPtr ProcessHandle,
        AccessTokenRights DesiredAccesss,
        out AccessToken TokenHandle);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446617.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe IntBoolean DuplicateTokenEx(
        AccessToken hExistingToken,
        AccessTokenRights dwDesiredAccess,
        SECURITY_ATTRIBUTES* lpTokenAttributes,
        ImpersonationLevel ImpersonationLevel,
        TokenType TokenType,
        out AccessToken phNewToken);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379590.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern IntBoolean SetThreadToken(
        ThreadHandle Thread,
        AccessToken Token);

    // https://msdn.microsoft.com/en-us/library/aa379180.aspx
    [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern IntBoolean LookupPrivilegeValueW(
        string? lpSystemName,
        string lpName,
        out LUID lpLuid);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379176.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern IntBoolean LookupPrivilegeNameW(
        IntPtr lpSystemName,
        ref LUID lpLuid,
        ref char lpName,
        ref uint cchName);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446671.aspx
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe IntBoolean GetTokenInformation(
        AccessToken TokenHandle,
        TokenInformation TokenInformationClass,
        void* TokenInformation,
        uint TokenInformationLength,
        out uint ReturnLength);
}