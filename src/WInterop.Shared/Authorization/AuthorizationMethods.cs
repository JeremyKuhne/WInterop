﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;
using WInterop.ProcessAndThreads;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Authorization
{
    public static partial class AuthorizationMethods
    {
        private static Dictionary<Privilege, string> s_privileges;

        static AuthorizationMethods()
        {
            s_privileges = new Dictionary<Privilege, string>
            {
                { Privilege.AssignPrimaryToken, "SeAssignPrimaryTokenPrivilege" },
                { Privilege.Audit, "SeAuditPrivilege" },
                { Privilege.Backup, "SeBackupPrivilege" },
                { Privilege.ChangeNotify, "SeChangeNotifyPrivilege" },
                { Privilege.CreateGlobal, "SeCreateGlobalPrivilege" },
                { Privilege.CreatePagefile, "SeCreatePagefilePrivilege" },
                { Privilege.CreatePermanent, "SeCreatePermanentPrivilege" },
                { Privilege.CreateSymbolicLink, "SeCreateSymbolicLinkPrivilege" },
                { Privilege.CreateToken, "SeCreateTokenPrivilege" },
                { Privilege.Debug, "SeDebugPrivilege" },
                { Privilege.EnableDelegation, "SeEnableDelegationPrivilege" },
                { Privilege.Impersonate, "SeImpersonatePrivilege" },
                { Privilege.IncreaseBasePriority, "SeIncreaseBasePriorityPrivilege" },
                { Privilege.IncreaseQuota, "SeIncreaseQuotaPrivilege" },
                { Privilege.IncreaseWorkingSet, "SeIncreaseWorkingSetPrivilege" },
                { Privilege.LoadDriver, "SeLoadDriverPrivilege" },
                { Privilege.LockMemory, "SeLockMemoryPrivilege" },
                { Privilege.MachineAccount, "SeMachineAccountPrivilege" },
                { Privilege.ManageVolume, "SeManageVolumePrivilege" },
                { Privilege.ProfileSingleProcess, "SeProfileSingleProcessPrivilege" },
                { Privilege.Relabel, "SeRelabelPrivilege" },
                { Privilege.RemoteShutdown, "SeRemoteShutdownPrivilege" },
                { Privilege.Restore, "SeRestorePrivilege" },
                { Privilege.Security, "SeSecurityPrivilege" },
                { Privilege.Shutdown, "SeShutdownPrivilege" },
                { Privilege.SyncAgent, "SeSyncAgentPrivilege" },
                { Privilege.SystemEnvironment, "SeSystemEnvironmentPrivilege" },
                { Privilege.SystemProfile, "SeSystemProfilePrivilege" },
                { Privilege.SystemTime, "SeSystemtimePrivilege" },
                { Privilege.TakeOwnership, "SeTakeOwnershipPrivilege" },
                { Privilege.TrustedComputerBase, "SeTcbPrivilege" },
                { Privilege.TimeZone, "SeTimeZonePrivilege" },
                { Privilege.TrustedCredentialManagerAccess, "SeTrustedCredManAccessPrivilege" },
                { Privilege.Undock, "SeUndockPrivilege" },
                { Privilege.UnsolicitedInput, "SeUnsolicitedInputPrivilege" }
            };
        }

        /// <summary>
        /// Get the string constant for the given privilege.
        /// </summary>
        public static string GetPrivilegeConstant(Privilege privilege)
        {
            if (!s_privileges.TryGetValue(privilege, out string value))
                throw new ArgumentOutOfRangeException(nameof(privilege));

            return value;
        }

        internal static Privilege ParsePrivilege(ReadOnlySpan<char> privilege)
        {
            foreach (var pair in s_privileges)
            {
                if (privilege.SequenceEqual(pair.Value.AsSpan()))
                {
                    return pair.Key;
                }
            }

            return default;
        }

        private static LUID LookupPrivilegeValue(Privilege privilege)
        {
            if (!Imports.LookupPrivilegeValueW(null, GetPrivilegeConstant(privilege), out LUID luid))
                throw Errors.GetIoExceptionForLastError(privilege.ToString());

            return luid;
        }

        /// <summary>
        /// Get the privilege for the specified LUID.
        /// </summary>
        public unsafe static Privilege LookupPrivilege(LUID luid)
        {
            char* c = stackalloc char[32];
            Span<char> nameBuffer = new Span<char>(c, 32);

            uint length = (uint)nameBuffer.Length;
            while (!Imports.LookupPrivilegeNameW(IntPtr.Zero, ref luid, ref MemoryMarshal.GetReference(nameBuffer), ref length))
            {
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                char* n = stackalloc char[(int)length];
                nameBuffer = new Span<char>(n, (int)length);
            }

            return ParsePrivilege(nameBuffer.Slice(0, (int)length));
        }

        private unsafe static void TokenInformationInvoke(
            AccessToken token,
            TokenInformation info,
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

        /// <summary>
        /// Returns true if the given token has the specified privilege. The privilege may or may not be enabled.
        /// </summary>
        public static bool HasPrivilege(AccessToken token, Privilege privilege)
        {
            return GetTokenPrivileges(token).Any(t => t.Privilege == privilege);
        }

        /// <summary>
        /// Get all privilege information for the given access token.
        /// </summary>
        public unsafe static IEnumerable<PrivilegeSetting> GetTokenPrivileges(AccessToken token)
        {
            var privileges = new List<PrivilegeSetting>();

            TokenInformationInvoke(token, TokenInformation.Privileges,
            buffer =>
            {
                foreach (LUID_AND_ATTRIBUTES privilege in ((TOKEN_PRIVILEGES*)buffer)->Privileges)
                {
                    privileges.Add(new PrivilegeSetting(LookupPrivilege(privilege.Luid), privilege.Attributes));
                }
            });

            return privileges;
        }

        /// <summary>
        /// Gets the group SIDs associated with the current token.
        /// </summary>
        public unsafe static IEnumerable<GroupSidInformation> GetTokenGroupSids(AccessToken token)
        {
            List<GroupSidInformation> info = null;

            TokenInformationInvoke(token, TokenInformation.Groups,
            buffer =>
            {
                TOKEN_GROUPS* groups = (TOKEN_GROUPS*)buffer;
                ReadOnlySpan<SID_AND_ATTRIBUTES> data = new ReadOnlySpan<SID_AND_ATTRIBUTES>(&groups->Groups, (int)groups->GroupCount);
                info = new List<GroupSidInformation>(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    info.Add(new GroupSidInformation(CopySid(data[i].Sid), (GroupSidAttributes)data[i].Attributes));
                }
            });

            return info;
        }

        /// <summary>
        /// Get the user SID for the given token.
        /// </summary>
        public unsafe static SID GetTokenUserSid(AccessToken token)
        {
            // This size should always be sufficient as SID alignment is uint.
            int size = sizeof(TOKEN_USER) + sizeof(SID);
            byte* buffer = stackalloc byte[size];

            if (!Imports.GetTokenInformation(token, TokenInformation.User, buffer, (uint)size, out uint _))
                throw Errors.GetIoExceptionForLastError();

            return CopySid(((TOKEN_USER*)buffer)->User.Sid);
        }

        /// <summary>
        /// Get the SID that will be used as the owner for objects created by the given token.
        /// </summary>
        public unsafe static SID GetTokenOwnerSid(AccessToken token)
        {
            // This size should always be sufficient as SID alignment is uint.
            int size = sizeof(TOKEN_OWNER) + sizeof(SID);
            byte* buffer = stackalloc byte[size];

            if (!Imports.GetTokenInformation(token, TokenInformation.Owner, buffer, (uint)size, out uint _))
                throw Errors.GetIoExceptionForLastError();

            return CopySid((IntPtr)((TOKEN_OWNER*)buffer)->Owner);
        }

        /// <summary>
        /// Get the SID that will be used as the primary group for objects created by the given token.
        /// </summary>
        public unsafe static SID GetTokenPrimaryGroupSid(AccessToken token)
        {
            // This size should always be sufficient as SID alignment is uint.
            int size = sizeof(TOKEN_PRIMARY_GROUP) + sizeof(SID);
            byte* buffer = stackalloc byte[size];

            if (!Imports.GetTokenInformation(token, TokenInformation.PrimaryGroup, buffer, (uint)size, out uint _))
                throw Errors.GetIoExceptionForLastError();

            return CopySid((IntPtr)((TOKEN_PRIMARY_GROUP*)buffer)->PrimaryGroup);
        }

        /// <summary>
        /// Get the specified "well known" SID. Note that not all well known SIDs are available on all OSes.
        /// </summary>
        public unsafe static SID CreateWellKnownSid(WellKnownSID sidType)
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
        public static bool IsWellKnownSid(in SID sid, WellKnownSID sidType)
            => Imports.IsWellKnownSid(in sid, sidType);

        /// <summary>
        /// Returns true if the given SID is valid.
        /// </summary>
        public static bool IsValidSid(in SID sid) => Imports.IsValidSid(in sid);

        /// <summary>
        /// Returns the S-n-n-n... string version of the given SID.
        /// </summary>
        public unsafe static string ConvertSidToString(in SID sid)
        {
            if (!Imports.ConvertSidToStringSidW(sid, out var handle))
                throw Errors.GetIoExceptionForLastError();

            return new string((char*)handle);
        }

        /// <summary>
        /// Returns the count of sub authorities for the given SID.
        /// </summary>
        public unsafe static byte GetSidSubAuthorityCount(in SID sid)
        {
            byte* b = Imports.GetSidSubAuthorityCount(sid);
            if (b == null)
                throw Errors.GetIoExceptionForLastError();

            return *b;
        }

        /// <summary>
        /// Get the sub authority at the specified index for the given SID.
        /// </summary>
        public unsafe static uint GetSidSubAuthority(in SID sid, uint nSubAuthority)
        {
            uint* u = Imports.GetSidSubAuthority(sid, nSubAuthority);
            if (u == null)
                throw Errors.GetIoExceptionForLastError();

            return *u;
        }

        private static unsafe SID CopySid(IntPtr source)
        {
            if (!Imports.CopySid((uint)sizeof(SID), out SID destination, (SID*)source))
                throw Errors.GetIoExceptionForLastError();
            return destination;
        }

        /// <summary>
        /// Get information about the given security identifier.
        /// </summary>
        /// <param name="systemName">
        /// The target computer to look up the SID on. When null will look on the local machine
        /// then trusted domain controllers.
        /// </param>
        public unsafe static AccountSidInformation LookupAccountSid(in SID sid, string systemName = null)
        {
            var wrapper = new LookupAccountSidWrapper(in sid, systemName);
            return BufferHelper.TwoBufferInvoke<LookupAccountSidWrapper, StringBuffer, AccountSidInformation>(ref wrapper);
        }

        private readonly struct LookupAccountSidWrapper : ITwoBufferFunc<StringBuffer, AccountSidInformation>
        {
            private readonly string _systemName;
            private readonly SID _sid;

            public LookupAccountSidWrapper(in SID sid, string systemName)
            {
                _systemName = systemName;
                _sid = sid;
            }

            AccountSidInformation ITwoBufferFunc<StringBuffer, AccountSidInformation>.Func(StringBuffer nameBuffer, StringBuffer domainNameBuffer)
            {
                SidNameUse usage;
                uint nameCharCapacity = nameBuffer.CharCapacity;
                uint domainNameCharCapacity = domainNameBuffer.CharCapacity;

                while (!Imports.LookupAccountSidW(
                    _systemName,
                    _sid,
                    nameBuffer,
                    ref nameCharCapacity,
                    domainNameBuffer,
                    ref domainNameCharCapacity,
                    out usage))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    nameBuffer.EnsureCharCapacity(nameCharCapacity);
                    domainNameBuffer.EnsureCharCapacity(domainNameCharCapacity);
                }

                nameBuffer.SetLengthToFirstNull();
                domainNameBuffer.SetLengthToFirstNull();

                AccountSidInformation info = new AccountSidInformation
                {
                    Name = nameBuffer.ToString(),
                    DomainName = domainNameBuffer.ToString(),
                    Usage = usage
                };

                return info;
            }
        }

        /// <summary>
        /// Opens a process token.
        /// </summary>
        public static AccessToken OpenProcessToken(AccessTokenRights desiredAccess)
        {
            if (!Imports.OpenProcessToken(ProcessMethods.GetCurrentProcess(), desiredAccess, out var processToken))
                throw Errors.GetIoExceptionForLastError(desiredAccess.ToString());

            return processToken;
        }

        /// <summary>
        /// Returns true if the current process is elevated.
        /// </summary>
        public unsafe static bool IsProcessElevated()
        {
            using (AccessToken token = OpenProcessToken(AccessTokenRights.Read))
            {
                TOKEN_ELEVATION elevation = new TOKEN_ELEVATION();
                if (!Imports.GetTokenInformation(
                    token,
                    TokenInformation.Elevation,
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
        /// Opens a thread token.
        /// </summary>
        public static AccessToken OpenThreadToken(AccessTokenRights desiredAccess, bool openAsSelf)
        {
            if (!Imports.OpenThreadToken(ThreadMethods.Imports.GetCurrentThread(), desiredAccess, openAsSelf, out var threadToken))
            {
                WindowsError error = Errors.GetLastError();
                if (error != WindowsError.ERROR_NO_TOKEN)
                    throw Errors.GetIoExceptionForError(error, desiredAccess.ToString());

                using (AccessToken processToken = OpenProcessToken(AccessTokenRights.Duplicate))
                {
                    if (!Imports.DuplicateTokenEx(
                        processToken,
                        AccessTokenRights.Impersonate | AccessTokenRights.Query | AccessTokenRights.AdjustPrivileges,
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
    }
}