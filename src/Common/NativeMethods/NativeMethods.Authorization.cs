// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using WInterop.Authorization;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.Handles;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static class Authorization
        {
            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static class Direct
            {
#if DESKTOP
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa375202.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool AdjustTokenPrivileges(
                    IntPtr TokenHandle,
                    [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
                    ref TOKEN_PRIVILEGES NewState,
                    uint BufferLength,
                    out TOKEN_PRIVILEGES PreviousState,
                    out uint ReturnLength);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446671.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetTokenInformation(
                    IntPtr TokenHandle,
                    TOKEN_INFORMATION_CLASS TokenInformationClass,
                    SafeHandle TokenInformation,
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
                    SafeCloseHandle ClientToken,
                    ref PRIVILEGE_SET RequiredPrivileges,
                    [MarshalAs(UnmanagedType.Bool)] out bool pfResult);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379590.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool SetThreadToken(
                    IntPtr Thread,
                    SafeCloseHandle Token);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379317.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool RevertToSelf();

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446617.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DuplicateTokenEx(
                    SafeCloseHandle hExistingToken,
                    TokenAccessLevels dwDesiredAccess,
                    IntPtr lpTokenAttributes,
                    SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
                    TOKEN_TYPE TokenType,
                    ref SafeCloseHandle phNewToken);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379295.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool OpenProcessToken(
                    IntPtr ProcessHandle,
                    TokenAccessLevels DesiredAccesss,
                    out SafeCloseHandle TokenHandle);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379296.aspx
                [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool OpenThreadToken(
                    IntPtr ThreadHandle,
                    TokenAccessLevels DesiredAccess,
                    [MarshalAs(UnmanagedType.Bool)] bool OpenAsSelf,
                    out SafeCloseHandle TokenHandle);
#endif // DESKTOP

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379166.aspx
                // LookupAccountSid

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379159.aspx
                // LookupAccountName
            }

            // In winnt.h
            private const uint PRIVILEGE_SET_ALL_NECESSARY = 1;

            // From winnt.h:
            //
            //         1   1   1   1   1   1
            //         5   4   3   2   1   0   9   8   7   6   5   4   3   2   1   0
            //      +---------------------------------------------------------------+
            //      |      SubAuthorityCount        |Reserved1 (SBZ)|   Revision    |
            //      +---------------------------------------------------------------+
            //      |                   IdentifierAuthority[0]                      |
            //      +---------------------------------------------------------------+
            //      |                   IdentifierAuthority[1]                      |
            //      +---------------------------------------------------------------+
            //      |                   IdentifierAuthority[2]                      |
            //      +---------------------------------------------------------------+
            //      |                                                               |
            //      +- -  -  -  -  -  -  -  SubAuthority[]  -  -  -  -  -  -  -  - -+
            //      |                                                               |
            //      +---------------------------------------------------------------+
            //
            //
            //      typedef struct _SID
            //      {
            //          BYTE Revision;
            //          BYTE SubAuthorityCount;
            //          SID_IDENTIFIER_AUTHORITY IdentifierAuthority;
            //          DWORD SubAuthority[ANYSIZE_ARRAY];
            //      } SID, *PISID;
            //
            // As a SID is variable in length it is a little more complicated to wrap. Using a flat buffer makes the most sense.
            // System.Security.Principal.SecurityIdentifier allows copying to/from a byte array.
            // 
            // https://msdn.microsoft.com/en-us/library/system.security.principal.securityidentifier.aspx

#if DESKTOP
            public static IEnumerable<PrivilegeSetting> GetTokenPrivileges(SafeCloseHandle token)
            {
                // Get the buffer size we need
                uint bytesNeeded;
                if (!Direct.GetTokenInformation(
                    token.DangerousGetHandle(),
                    TOKEN_INFORMATION_CLASS.TokenPrivileges,
                    EmptySafeHandle.Instance,
                    0,
                    out bytesNeeded))
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    if (error != WinErrors.ERROR_INSUFFICIENT_BUFFER)
                        throw ErrorHelper.GetIoExceptionForError(error);
                }
                else
                {
                    // Didn't need any space for output, let's assume there are no privileges
                    return Enumerable.Empty<PrivilegeSetting>();
                }

                // Initialize the buffer and get the data
                var streamBuffer = new StreamBuffer(bytesNeeded);
                if (!Direct.GetTokenInformation(
                    token.DangerousGetHandle(),
                    TOKEN_INFORMATION_CLASS.TokenPrivileges,
                    streamBuffer,
                    (uint)streamBuffer.Length,
                    out bytesNeeded))
                {
                    throw ErrorHelper.GetIoExceptionForLastError();
                }

                // Loop through and get our privileges
                BinaryReader reader = new BinaryReader(streamBuffer, Encoding.Unicode, leaveOpen: true);
                uint count = reader.ReadUInt32();

                var privileges = new List<PrivilegeSetting>();
                StringBuffer nameBuffer = StringBufferCache.Instance.Acquire(256);

                for (int i = 0; i < count; i++)
                {
                    LUID luid = new LUID
                    {
                        LowPart = reader.ReadUInt32(),
                        HighPart = reader.ReadUInt32(),
                    };

                    uint length = nameBuffer.CharCapacity;

                    if (!Direct.LookupPrivilegeNameW(IntPtr.Zero, ref luid, nameBuffer, ref length))
                        throw ErrorHelper.GetIoExceptionForLastError();

                    nameBuffer.Length = length;

                    PrivilegeAttributes attributes = (PrivilegeAttributes)reader.ReadUInt32();
                    privileges.Add(new PrivilegeSetting(nameBuffer.ToString(), attributes));
                    nameBuffer.Length = 0;
                }

                StringBufferCache.Instance.Release(nameBuffer);
                return privileges;
            }

            /// <summary>
            /// Returns true if the given token has the specified privilege. The privilege may or may not be enabled.
            /// </summary>
            public static bool HasPrivilege(SafeCloseHandle token, Privileges privilege)
            {
                return GetTokenPrivileges(token).Any(t => t.Privilege == privilege);
            }

            private static LUID LookupPrivilegeValue(string name)
            {
                LUID luid = new LUID();
                if (!Direct.LookupPrivilegeValueW(null, name, ref luid))
                    throw ErrorHelper.GetIoExceptionForLastError(name);

                return luid;
            }

            /// <summary>
            /// Checks if the given privilege is enabled. This does not tell you whether or not it
            /// is possible to get a privilege- most held privileges are not enabled by default.
            /// </summary>
            public static bool IsPrivilegeEnabled(SafeCloseHandle token, Privileges privilege)
            {
                LUID luid = LookupPrivilegeValue(privilege.ToString());

                var luidAttributes = new LUID_AND_ATTRIBUTES
                {
                    Luid = luid,
                    Attributes = (uint)PrivilegeAttributes.SE_PRIVILEGE_ENABLED
                };

                var set = new PRIVILEGE_SET
                {
                    Control = PRIVILEGE_SET_ALL_NECESSARY,
                    PrivilegeCount = 1,
                    Privilege = new[] { luidAttributes }
                };


                bool result;
                if (!Direct.PrivilegeCheck(token, ref set, out result))
                    throw ErrorHelper.GetIoExceptionForLastError(privilege.ToString());

                return result;
            }

            public static SafeCloseHandle OpenProcessToken(TokenAccessLevels desiredAccess)
            {
                SafeCloseHandle processToken;
                if (!Direct.OpenProcessToken(Process.GetCurrentProcess().Handle, desiredAccess, out processToken))
                    throw ErrorHelper.GetIoExceptionForLastError(desiredAccess.ToString());

                return processToken;
            }

            public static SafeCloseHandle OpenThreadToken(TokenAccessLevels desiredAccess, bool openAsSelf)
            {
                SafeCloseHandle threadToken;
                if (!Direct.OpenThreadToken(ProcessAndThreads.Direct.GetCurrentThread(), desiredAccess, openAsSelf, out threadToken))
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    if (error != WinErrors.ERROR_NO_TOKEN)
                        throw ErrorHelper.GetIoExceptionForError(error, desiredAccess.ToString());

                    SafeCloseHandle processToken = OpenProcessToken(TokenAccessLevels.Duplicate);
                    if (!Direct.DuplicateTokenEx(
                        processToken,
                        TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges,
                        IntPtr.Zero,
                        SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation,
                        TOKEN_TYPE.TokenImpersonation,
                        ref threadToken))
                    {
                        throw ErrorHelper.GetIoExceptionForLastError(desiredAccess.ToString());
                    }
                }

                return threadToken;
            }
#endif // DESKTOP
        }
    }
}
