// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WInterop.Authorization.DataTypes;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Handles.DataTypes;
using WInterop.ProcessAndThreads;

namespace WInterop.Authorization
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static partial class AuthorizationDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa375202.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AdjustTokenPrivileges(
                SafeTokenHandle TokenHandle,
                [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
                ref TOKEN_PRIVILEGES NewState,
                uint BufferLength,
                out TOKEN_PRIVILEGES PreviousState,
                out uint ReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446671.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetTokenInformation(
                SafeTokenHandle TokenHandle,
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
                SafeTokenHandle ClientToken,
                ref PRIVILEGE_SET RequiredPrivileges,
                [MarshalAs(UnmanagedType.Bool)] out bool pfResult);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379590.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetThreadToken(
                IntPtr Thread,
                SafeTokenHandle Token);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379317.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RevertToSelf();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446617.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DuplicateTokenEx(
                SafeTokenHandle hExistingToken,
                TokenRights dwDesiredAccess,
                IntPtr lpTokenAttributes,
                SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
                TOKEN_TYPE TokenType,
                ref SafeTokenHandle phNewToken);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379295.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool OpenProcessToken(
                IntPtr ProcessHandle,
                TokenRights DesiredAccesss,
                out SafeTokenHandle TokenHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379296.aspx
            [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool OpenThreadToken(
                SafeThreadHandle ThreadHandle,
                TokenRights DesiredAccess,
                [MarshalAs(UnmanagedType.Bool)] bool OpenAsSelf,
                out SafeTokenHandle TokenHandle);

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

        public static IEnumerable<PrivilegeSetting> GetTokenPrivileges(SafeTokenHandle token)
        {
            // Get the buffer size we need
            uint bytesNeeded;
            if (!Direct.GetTokenInformation(
                token,
                TOKEN_INFORMATION_CLASS.TokenPrivileges,
                EmptySafeHandle.Instance,
                0,
                out bytesNeeded))
            {
                WindowsError error = ErrorHelper.GetLastError();
                if (error != WindowsError.ERROR_INSUFFICIENT_BUFFER)
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
                token,
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
        public static bool HasPrivilege(SafeTokenHandle token, Privileges privilege)
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
        public static bool IsPrivilegeEnabled(SafeTokenHandle token, Privileges privilege)
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

        /// <summary>
        /// Opens a process token.
        /// </summary>
        public static SafeTokenHandle OpenProcessToken(TokenRights desiredAccess)
        {
            SafeTokenHandle processToken;
            if (!Direct.OpenProcessToken(ProcessMethods.Direct.GetCurrentProcess().DangerousGetHandle(), desiredAccess, out processToken))
                throw ErrorHelper.GetIoExceptionForLastError(desiredAccess.ToString());

            return processToken;
        }

        /// <summary>
        /// Opens a thread token.
        /// </summary>
        public static SafeTokenHandle OpenThreadToken(TokenRights desiredAccess, bool openAsSelf)
        {
            SafeTokenHandle threadToken;
            if (!Direct.OpenThreadToken(ThreadMethods.Direct.GetCurrentThread(), desiredAccess, openAsSelf, out threadToken))
            {
                WindowsError error = ErrorHelper.GetLastError();
                if (error != WindowsError.ERROR_NO_TOKEN)
                    throw ErrorHelper.GetIoExceptionForError(error, desiredAccess.ToString());

                SafeTokenHandle processToken = OpenProcessToken(TokenRights.TOKEN_DUPLICATE);
                if (!Direct.DuplicateTokenEx(
                    processToken,
                    TokenRights.TOKEN_IMPERSONATE | TokenRights.TOKEN_QUERY | TokenRights.TOKEN_ADJUST_PRIVILEGES,
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
    }
}
