// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Authorization.BufferWrappers;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;
using WInterop.Handles.Types;
using WInterop.ProcessAndThreads;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Authorization
{
    public static partial class AuthorizationMethods
    {
        // In winnt.h
        private const uint PRIVILEGE_SET_ALL_NECESSARY = 1;

        private unsafe static void TokenInformationInvoke(
            TokenHandle token,
            TOKEN_INFORMATION_CLASS info,
            Action<IntPtr> action)
        {
            BufferHelper.BufferInvoke<HeapBuffer>(buffer =>
            {
                while (!Imports.GetTokenInformation(
                    token,
                    info,
                    buffer.VoidPointer,
                    (uint)buffer.ByteCapacity,
                    out uint bytesNeeded))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureByteCapacity(bytesNeeded);
                }

                action(buffer.DangerousGetHandle());
            });
        }

        public unsafe static IEnumerable<PrivilegeSetting> GetTokenPrivileges(TokenHandle token)
        {
            var privileges = new List<PrivilegeSetting>();

            TokenInformationInvoke(token, TOKEN_INFORMATION_CLASS.TokenPrivileges,
            buffer =>
            {
                uint* u = (uint*)buffer;

                // Loop through and get our privileges
                uint count = *u++;

                BufferHelper.BufferInvoke((StringBuffer stringBuffer) =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        LUID luid = new LUID
                        {
                            LowPart = *u++,
                            HighPart = *u++,
                        };

                        uint length = stringBuffer.CharCapacity;

                        if (!Imports.LookupPrivilegeNameW(IntPtr.Zero, ref luid, stringBuffer, ref length))
                            throw Errors.GetIoExceptionForLastError();

                        stringBuffer.Length = length;

                        PrivilegeAttributes attributes = (PrivilegeAttributes)(*u++);
                        privileges.Add(new PrivilegeSetting(stringBuffer.ToString(), attributes));
                        stringBuffer.Length = 0;
                    }
                });
            });

            return privileges;
        }

        /// <summary>
        /// Returns true if the given token has the specified privilege. The privilege may or may not be enabled.
        /// </summary>
        public static bool HasPrivilege(TokenHandle token, Privileges privilege)
        {
            return GetTokenPrivileges(token).Any(t => t.Privilege == privilege);
        }

        private static LUID LookupPrivilegeValue(string name)
        {
            LUID luid = new LUID();
            if (!Imports.LookupPrivilegeValueW(null, name, ref luid))
                throw Errors.GetIoExceptionForLastError(name);

            return luid;
        }

        /// <summary>
        /// Checks if the given privilege is enabled. This does not tell you whether or not it
        /// is possible to get a privilege- most held privileges are not enabled by default.
        /// </summary>
        public static bool IsPrivilegeEnabled(TokenHandle token, Privileges privilege)
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

            if (!Imports.PrivilegeCheck(token, ref set, out bool result))
                throw Errors.GetIoExceptionForLastError(privilege.ToString());

            return result;
        }

        /// <summary>
        /// Opens a process token.
        /// </summary>
        public static TokenHandle OpenProcessToken(TokenRights desiredAccess)
        {
            if (!Imports.OpenProcessToken(ProcessMethods.GetCurrentProcess(), desiredAccess, out var processToken))
                throw Errors.GetIoExceptionForLastError(desiredAccess.ToString());

            return processToken;
        }

        /// <summary>
        /// Opens a thread token.
        /// </summary>
        public static TokenHandle OpenThreadToken(TokenRights desiredAccess, bool openAsSelf)
        {
            if (!Imports.OpenThreadToken(ThreadMethods.Imports.GetCurrentThread(), desiredAccess, openAsSelf, out var threadToken))
            {
                WindowsError error = Errors.GetLastError();
                if (error != WindowsError.ERROR_NO_TOKEN)
                    throw Errors.GetIoExceptionForError(error, desiredAccess.ToString());

                using (TokenHandle processToken = OpenProcessToken(TokenRights.TOKEN_DUPLICATE))
                {
                    if (!Imports.DuplicateTokenEx(
                        processToken,
                        TokenRights.TOKEN_IMPERSONATE | TokenRights.TOKEN_QUERY | TokenRights.TOKEN_ADJUST_PRIVILEGES,
                        IntPtr.Zero,
                        SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation,
                        TOKEN_TYPE.TokenImpersonation,
                        ref threadToken))
                    {
                        throw Errors.GetIoExceptionForLastError(desiredAccess.ToString());
                    }
                }
            }

            return threadToken;
        }

        /// <summary>
        /// Returns true if the current process is elevated.
        /// </summary>
        public unsafe static bool IsProcessElevated()
        {
            using (TokenHandle token = OpenProcessToken(TokenRights.TOKEN_READ))
            {
                TOKEN_ELEVATION elevation = new TOKEN_ELEVATION();
                if (!Imports.GetTokenInformation(
                    token,
                    TOKEN_INFORMATION_CLASS.TokenElevation,
                    &elevation,
                    (uint)sizeof(TOKEN_ELEVATION),
                    out _))
                {
                    throw Errors.GetIoExceptionForLastError();
                }

                return elevation.TokenIsElevated;
            }
        }

        /// <summary>
        /// Get the SID for the given token.
        /// </summary>
        public unsafe static SID GetTokenSid(TokenHandle token)
        {
            SID sid = new SID();
            TokenInformationInvoke(token, TOKEN_INFORMATION_CLASS.TokenUser,
            buffer =>
            {
                if (!Imports.CopySid((uint)sizeof(SID), out sid, ((SID_AND_ATTRIBUTES*)buffer)->Sid))
                {
                    throw Errors.GetIoExceptionForLastError();
                }
            });

            return sid;
        }

        /// <summary>
        /// Returns true if the given SID is valid.
        /// </summary>
        public static bool IsValidSid(ref SID sid)
        {
            return Imports.IsValidSid(ref sid);
        }

        /// <summary>
        /// Get the specified "well known" SID. Note that not all well known SIDs are available on all OSes.
        /// </summary>
        public unsafe static SID CreateWellKnownSid(WELL_KNOWN_SID_TYPE sidType)
        {
            SID sid = new SID();

            uint size = (uint)sizeof(SID);
            if (!Imports.CreateWellKnownSid(sidType, null, &sid, ref size))
                throw Errors.GetIoExceptionForLastError();

            return sid;
        }

        /// <summary>
        /// Returns true if the given SID is the specified "well known" SID type.
        /// </summary>
        public static bool IsWellKnownSid(ref SID sid, WELL_KNOWN_SID_TYPE sidType)
        {
            return Imports.IsWellKnownSid(ref sid, sidType);
        }

        /// <summary>
        /// Returns the S-n-n-n... string version of the given SID.
        /// </summary>
        public static string ConvertSidToString(ref SID sid)
        {
            if (!Imports.ConvertSidToStringSidW(ref sid, out var handle))
                throw Errors.GetIoExceptionForLastError();

            unsafe
            {
                return new string((char*)handle);
            }
        }

        /// <summary>
        /// Returns the count of sub authorities for the given SID.
        /// </summary>
        public static byte GetSidSubAuthorityCount(ref SID sid)
        {
            unsafe
            {
                byte* b = Imports.GetSidSubAuthorityCount(ref sid);
                if (b == null)
                    throw Errors.GetIoExceptionForLastError();

                return *b;
            }
        }

        /// <summary>
        /// Get the sub authority at the specified index for the given SID.
        /// </summary>
        public static uint GetSidSubAuthority(ref SID sid, uint nSubAuthority)
        {
            unsafe
            {
                uint* u = Imports.GetSidSubAuthority(ref sid, nSubAuthority);
                if (u == null)
                    throw Errors.GetIoExceptionForLastError();

                return *u;
            }
        }

        /// <summary>
        /// Gets the info (name, domain name, usage) for the given SID.
        /// </summary>
        public static AccountSidInfo LookupAccountSidLocal(ref SID sid)
        {
            var wrapper = new LookupAccountSidLocalWrapper { Sid = sid };
            return BufferHelper.TwoBufferInvoke<LookupAccountSidLocalWrapper, StringBuffer, AccountSidInfo>(ref wrapper);
        }
    }
}
