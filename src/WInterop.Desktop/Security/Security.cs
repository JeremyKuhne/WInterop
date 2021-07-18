// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.ProcessAndThreads;
using WInterop.Security.Native;
using WInterop.Support.Buffers;
using WInterop.SystemInformation;

namespace WInterop.Security
{
    public static partial class Security
    {
        // In winnt.h
        private const uint PRIVILEGE_SET_ALL_NECESSARY = 1;

        private static readonly Dictionary<Privilege, string> s_privileges;

        static Security()
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
        ///  Get the string constant for the given privilege.
        /// </summary>
        public static string GetPrivilegeConstant(Privilege privilege)
        {
            if (!s_privileges.TryGetValue(privilege, out string? value))
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
            Error.ThrowLastErrorIfFalse(
                SecurityImports.LookupPrivilegeValueW(null, GetPrivilegeConstant(privilege), out LUID luid),
                privilege.ToString());

            return luid;
        }

        /// <summary>
        ///  Get the privilege for the specified LUID.
        /// </summary>
        public static unsafe Privilege LookupPrivilege(LUID luid)
        {
            char* c = stackalloc char[32];
            Span<char> nameBuffer = new Span<char>(c, 32);

            uint length = (uint)nameBuffer.Length;
            while (!SecurityImports.LookupPrivilegeNameW(IntPtr.Zero, ref luid, ref MemoryMarshal.GetReference(nameBuffer), ref length))
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
#pragma warning disable CA2014 // Do not use stackalloc in loops
                // Should only loop once
                char* n = stackalloc char[(int)length];
#pragma warning restore CA2014 // Do not use stackalloc in loops
                nameBuffer = new Span<char>(n, (int)length);
            }

            return ParsePrivilege(nameBuffer.Slice(0, (int)length));
        }

        private static unsafe void TokenInformationInvoke(
            AccessToken token,
            TokenInformation info,
            Action<IntPtr> action)
        {
            BufferHelper.BufferInvoke<HeapBuffer>(buffer =>
            {
                while (!SecurityImports.GetTokenInformation(
                    token,
                    info,
                    buffer.VoidPointer,
                    (uint)buffer.ByteCapacity,
                    out uint bytesNeeded))
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureByteCapacity(bytesNeeded);
                }

                action(buffer.DangerousGetHandle());
            });
        }

        /// <summary>
        ///  Get information about the specified token.
        /// </summary>
        public static unsafe TokenStatistics GetTokenStatistics(this AccessToken token)
        {
            TokenStatistics stats = default;
            Error.ThrowLastErrorIfFalse(
                SecurityImports.GetTokenInformation(token, TokenInformation.Statistics, &stats, (uint)sizeof(TokenStatistics), out uint _));
            return stats;
        }

        /// <summary>
        ///  Returns true if the given token has the specified privilege. The privilege may or may not be enabled.
        /// </summary>
        public static bool HasPrivilege(this AccessToken token, Privilege privilege)
            => GetTokenPrivileges(token).Any(t => t.Privilege == privilege);

        /// <summary>
        ///  Get all privilege information for the given access token.
        /// </summary>
        public static unsafe IEnumerable<PrivilegeSetting> GetTokenPrivileges(this AccessToken token)
        {
            var privileges = new List<PrivilegeSetting>();

            TokenInformationInvoke(
                token,
                TokenInformation.Privileges,
                buffer =>
                {
                    foreach (LuidAndAttributes privilege in ((TOKEN_PRIVILEGES*)buffer)->Privileges)
                    {
                        privileges.Add(new PrivilegeSetting(LookupPrivilege(privilege.Luid), privilege.Attributes));
                    }
                });

            return privileges;
        }

        /// <summary>
        ///  Gets the group SIDs associated with the current token.
        /// </summary>
        public static unsafe IEnumerable<SidAndAttributes> GetTokenGroupSids(this AccessToken token)
        {
            SidAndAttributes[]? info = null;

            TokenInformationInvoke(
                token,
                TokenInformation.Groups,
                buffer =>
                {
                    TOKEN_GROUPS* groups = (TOKEN_GROUPS*)buffer;
                    info = new SidAndAttributes[(int)groups->GroupCount];
                    SID_AND_ATTRIBUTES* group = &groups->Groups;

                    // Copy the SID pointers into our own SID structs.
                    for (int i = 0; i < info.Length; i++)
                    {
                        info[i] = new SidAndAttributes(
                            CopySid(group[i].Sid), group[i].Attributes);
                    }
                });

            return info!;
        }

        /// <summary>
        ///  Get the user SID for the given token.
        /// </summary>
        public static unsafe SidAndAttributes GetTokenUserSid(this AccessToken token)
        {
            // This size should always be sufficient as SID alignment is uint.
            int size = sizeof(TOKEN_USER) + sizeof(SID);
            byte* buffer = stackalloc byte[size];

            Error.ThrowLastErrorIfFalse(
                SecurityImports.GetTokenInformation(token, TokenInformation.User, buffer, (uint)size, out uint _));

            TOKEN_USER* user = (TOKEN_USER*)buffer;
            return new SidAndAttributes(CopySid(user->User.Sid), user->User.Attributes);
        }

        /// <summary>
        ///  Get the SID that will be used as the owner for objects created by the given token.
        /// </summary>
        public static unsafe SID GetTokenOwnerSid(this AccessToken token)
        {
            // This size should always be sufficient as SID alignment is uint.
            int size = sizeof(TOKEN_OWNER) + sizeof(SID);
            byte* buffer = stackalloc byte[size];

            Error.ThrowLastErrorIfFalse(
                SecurityImports.GetTokenInformation(token, TokenInformation.Owner, buffer, (uint)size, out uint _));

            TOKEN_OWNER* owner = (TOKEN_OWNER*)buffer;
            return CopySid(owner->Owner);
        }

        /// <summary>
        ///  Get the SID that will be used as the primary group for objects created by the given token.
        /// </summary>
        public static unsafe SID GetTokenPrimaryGroupSid(this AccessToken token)
        {
            // This size should always be sufficient as SID alignment is uint.
            int size = sizeof(TOKEN_PRIMARY_GROUP) + sizeof(SID);
            byte* buffer = stackalloc byte[size];

            Error.ThrowLastErrorIfFalse(
                SecurityImports.GetTokenInformation(token, TokenInformation.PrimaryGroup, buffer, (uint)size, out uint _));

            return CopySid(((TOKEN_PRIMARY_GROUP*)buffer)->PrimaryGroup);
        }

        /// <summary>
        ///  Get the specified "well known" SID. Note that not all well known SIDs are available on all OSes.
        /// </summary>
        public static unsafe SID CreateWellKnownSid(WellKnownSID sidType)
        {
            SID sid = default;

            uint size = (uint)sizeof(SID);
            Error.ThrowLastErrorIfFalse(
                SecurityImports.CreateWellKnownSid(sidType, null, &sid, ref size));

            return sid;
        }

        /// <summary>
        ///  Returns true if the given SID is the specified "well known" SID type.
        /// </summary>
        public static bool IsWellKnownSid(this in SID sid, WellKnownSID sidType)
            => SecurityImports.IsWellKnownSid(in sid, sidType);

        /// <summary>
        ///  Returns true if the given SID is valid.
        /// </summary>
        public static bool IsValidSid(this in SID sid) => SecurityImports.IsValidSid(in sid);

        /// <summary>
        ///  Returns the S-n-n-n... string version of the given SID.
        /// </summary>
        public static unsafe string ConvertSidToString(this in SID sid)
        {
            Error.ThrowLastErrorIfFalse(
                SecurityImports.ConvertSidToStringSidW(sid, out var handle));

            return new string((char*)handle);
        }

        /// <summary>
        ///  Returns the count of sub authorities for the given SID.
        /// </summary>
        public static unsafe byte GetSidSubAuthorityCount(this in SID sid)
        {
            byte* b = SecurityImports.GetSidSubAuthorityCount(sid);
            if (b == null)
                Error.ThrowLastError();

            return *b;
        }

        /// <summary>
        ///  Get the sub authority at the specified index for the given SID.
        /// </summary>
        public static unsafe uint GetSidSubAuthority(this in SID sid, uint nSubAuthority)
        {
            uint* u = SecurityImports.GetSidSubAuthority(sid, nSubAuthority);
            if (u == null)
                Error.ThrowLastError();

            return *u;
        }

        private static unsafe SID CopySid(SID* source)
        {
            Error.ThrowLastErrorIfFalse(
                SecurityImports.CopySid((uint)sizeof(SID), out SID destination, source));
            return destination;
        }

        /// <summary>
        ///  Get information about the given security identifier.
        /// </summary>
        /// <param name="systemName">
        ///  The target computer to look up the SID on. When null will look on the local machine then trusted
        ///  domain controllers.
        /// </param>
        public static unsafe AccountSidInformation LookupAccountSid(this in SID sid, string? systemName = null)
        {
            var wrapper = new LookupAccountSidWrapper(in sid, systemName);
            return BufferHelper.TwoBufferInvoke<LookupAccountSidWrapper, StringBuffer, AccountSidInformation>(ref wrapper);
        }

        private readonly struct LookupAccountSidWrapper : ITwoBufferFunc<StringBuffer, AccountSidInformation>
        {
            private readonly string? _systemName;
            private readonly SID _sid;

            public LookupAccountSidWrapper(in SID sid, string? systemName)
            {
                _systemName = systemName;
                _sid = sid;
            }

            AccountSidInformation ITwoBufferFunc<StringBuffer, AccountSidInformation>.Func(StringBuffer nameBuffer, StringBuffer domainNameBuffer)
            {
                SidNameUse usage;
                uint nameCharCapacity = nameBuffer.CharCapacity;
                uint domainNameCharCapacity = domainNameBuffer.CharCapacity;

                while (!SecurityImports.LookupAccountSidW(
                    _systemName,
                    _sid,
                    nameBuffer,
                    ref nameCharCapacity,
                    domainNameBuffer,
                    ref domainNameCharCapacity,
                    out usage))
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
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
        ///  Opens a process token.
        /// </summary>
        public static AccessToken OpenProcessToken(AccessTokenRights desiredAccess)
        {
            if (!SecurityImports.OpenProcessToken(Processes.GetCurrentProcess(), desiredAccess, out var processToken))
                Error.ThrowLastError(desiredAccess.ToString());

            return processToken;
        }

        /// <summary>
        ///  Returns true if the current process is elevated.
        /// </summary>
        public static unsafe bool IsProcessElevated()
        {
            using AccessToken token = OpenProcessToken(AccessTokenRights.Read);
            TokenElevation elevation = default;
            Error.ThrowLastErrorIfFalse(
                SecurityImports.GetTokenInformation(
                    token,
                    TokenInformation.Elevation,
                    &elevation,
                    (uint)sizeof(TokenElevation),
                    out _));

            return elevation.TokenIsElevated;
        }

        /// <summary>
        ///  Opens a thread token. Returns null if the thead has no token (i.e. it isn't impersonating
        ///  and is implicitly inheriting the process token).
        /// </summary>
        public static AccessToken? OpenThreadToken(AccessTokenRights desiredAccess, bool openAsSelf)
        {
            if (!SecurityImports.OpenThreadToken(Threads.GetCurrentThread(), desiredAccess, openAsSelf, out var threadToken))
            {
                // Threads only have their own token if the are impersonating, otherwise they inherit process
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_NO_TOKEN);
                return null;
            }

            return threadToken;
        }

        /// <summary>
        ///  Duplicates a token. Token must have been created with <see cref="AccessTokenRights.Duplicate"/>.
        /// </summary>
        /// <param name="token">The token to duplicate.</param>
        /// <param name="rights">Rights to apply to the token. default is all of the rights from the source token.</param>
        public static unsafe AccessToken DuplicateToken(
            this AccessToken token,
            AccessTokenRights rights = default,
            ImpersonationLevel impersonationLevel = ImpersonationLevel.Anonymous,
            TokenType tokenType = TokenType.Impersonation)
        {
            // Note that DuplicateToken calls DuplicateTokenEx with TOKEN_IMPERSONATE | TOKEN_QUERY and null.
            Error.ThrowLastErrorIfFalse(
                SecurityImports.DuplicateTokenEx(
                    token,
                    rights,
                    null,
                    impersonationLevel,
                    tokenType,
                    out AccessToken duplicatedToken));

            return duplicatedToken;
        }

        public static void SetThreadToken(this ThreadHandle thread, AccessToken token)
            => Error.ThrowLastErrorIfFalse(SecurityImports.SetThreadToken(thread, token));

        /// <summary>
        ///  Get the owner SID for the given handle.
        /// </summary>
        public static unsafe SID GetOwner(SafeHandle handle, ObjectType type)
        {
            SID* sidp;
            SECURITY_DESCRIPTOR* descriptor;

            SecurityImports.GetSecurityInfo(
                handle,
                type,
                SecurityInformation.Owner,
                ppsidOwner: &sidp,
                ppSecurityDescriptor: &descriptor)
                .ThrowIfFailed();

            SID sid = new SID(sidp);
            Memory.Memory.LocalFree((IntPtr)descriptor);
            return sid;
        }

        /// <summary>
        ///  Get the primary group SID for the given handle.
        /// </summary>
        public static unsafe SID GetPrimaryGroup(SafeHandle handle, ObjectType type)
        {
            SID* sidp;
            SECURITY_DESCRIPTOR* descriptor;

            SecurityImports.GetSecurityInfo(
                handle,
                type,
                SecurityInformation.Group,
                ppsidGroup: &sidp,
                ppSecurityDescriptor: &descriptor)
                .ThrowIfFailed();

            SID sid = new SID(sidp);
            Memory.Memory.LocalFree((IntPtr)descriptor);
            return sid;
        }

        /// <summary>
        ///  Get discretionary access control list for the specified handle.
        /// </summary>
        public static unsafe SecurityDescriptor GetAccessControlList(SafeHandle handle, ObjectType type)
        {
            ACL* acl;
            SECURITY_DESCRIPTOR* descriptor;

            SecurityImports.GetSecurityInfo(
                handle,
                type,
                SecurityInformation.Dacl,
                ppDacl: &acl,
                ppSecurityDescriptor: &descriptor)
                .ThrowIfFailed();

            return new SecurityDescriptor(handle, type, descriptor);
        }

        /// <summary>
        ///  Checks if the given privilege is enabled. This does not tell you whether or not it
        ///  is possible to get a privilege- most held privileges are not enabled by default.
        /// </summary>
        public static unsafe bool IsPrivilegeEnabled(this AccessToken token, Privilege privilege)
        {
            LUID luid = LookupPrivilegeValue(privilege);

            var set = new PrivilegeSet
            {
                Control = PRIVILEGE_SET_ALL_NECESSARY,
                PrivilegeCount = 1,
                Privilege = luid
            };

            Error.ThrowLastErrorIfFalse(
                SecurityImports.PrivilegeCheck(token, &set, out IntBoolean result),
                privilege.ToString());

            return result;
        }

        /// <summary>
        ///  Returns true if all of the given privileges are enabled for the current process.
        /// </summary>
        public static bool AreAllPrivilegesEnabled(this AccessToken token, params Privilege[] privileges)
        {
            return ArePrivilegesEnabled(token, all: true, privileges: privileges);
        }

        /// <summary>
        ///  Returns true if any of the given privileges are enabled for the current process.
        /// </summary>
        public static bool AreAnyPrivilegesEnabled(this AccessToken token, params Privilege[] privileges)
        {
            return ArePrivilegesEnabled(token, all: false, privileges: privileges);
        }

        private static unsafe bool ArePrivilegesEnabled(this AccessToken token, bool all, Privilege[] privileges)
        {
            if (privileges == null || privileges.Length == 0)
                return true;

            byte* buffer = stackalloc byte[sizeof(PrivilegeSet) + (sizeof(LuidAndAttributes) * (privileges.Length - 1))];
            PrivilegeSet* set = (PrivilegeSet*)buffer;
            set->Control = all ? PRIVILEGE_SET_ALL_NECESSARY : 0;
            set->PrivilegeCount = (uint)privileges.Length;
            Span<LuidAndAttributes> luids = new Span<LuidAndAttributes>(&set->Privilege, privileges.Length);
            for (int i = 0; i < privileges.Length; i++)
            {
                luids[i] = LookupPrivilegeValue(privileges[i]);
            }

            Error.ThrowLastErrorIfFalse(
                SecurityImports.PrivilegeCheck(token, set, out IntBoolean result));

            return result;
        }

        /// <summary>
        ///  Get the current domain name.
        /// </summary>
        public static string GetDomainName()
        {
            GetDomainNameWrapper wrapper = default;
            return BufferHelper.TwoBufferInvoke<GetDomainNameWrapper, StringBuffer, string>(ref wrapper);
        }

        private struct GetDomainNameWrapper : ITwoBufferFunc<StringBuffer, string>
        {
            unsafe string ITwoBufferFunc<StringBuffer, string>.Func(StringBuffer nameBuffer, StringBuffer domainNameBuffer)
            {
                string? name = SystemInformation.SystemInformation.GetUserName(ExtendedNameFormat.SamCompatible);

                if (name is null)
                    throw new InvalidOperationException($"Could not get the {nameof(ExtendedNameFormat.SamCompatible)} user name.");

                SID sid = default;
                uint sidLength = (uint)sizeof(SID);
                uint domainNameLength = domainNameBuffer.CharCapacity;
                while (!SecurityImports.LookupAccountNameW(
                    lpSystemName: null,
                    lpAccountName: name,
                    Sid: &sid,
                    cbSid: ref sidLength,
                    ReferencedDomainName: domainNameBuffer.CharPointer,
                    cchReferencedDomainName: ref domainNameLength,
                    peUse: out _))
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    domainNameBuffer.EnsureCharCapacity(domainNameLength);
                }

                domainNameBuffer.Length = domainNameLength;
                return domainNameBuffer.ToString();
            }
        }

        public static unsafe AccessToken CreateRestrictedToken(this AccessToken token, params SID[] sidsToDisable)
        {
            if (sidsToDisable == null || sidsToDisable.Length == 0)
                throw new ArgumentNullException();

            SID_AND_ATTRIBUTES* sids = stackalloc SID_AND_ATTRIBUTES[sidsToDisable.Length];
            fixed (SID* sid = sidsToDisable)
            {
                for (int i = 0; i < sidsToDisable.Length; i++)
                    sids[i].Sid = &sid[i];

                Error.ThrowLastErrorIfFalse(
                    SecurityImports.CreateRestrictedToken(token, 0, (uint)sidsToDisable.Length, sids, 0, null, 0, null, out AccessToken restricted));

                return restricted;
            }
        }

        public static void ImpersonateLoggedOnUser(this AccessToken token)
            => Error.ThrowLastErrorIfFalse(SecurityImports.ImpersonateLoggedOnUser(token));

        public static void ImpersonateAnonymousToken()
        {
            using ThreadHandle thread = Threads.GetCurrentThread();
            Error.ThrowLastErrorIfFalse(SecurityImports.ImpersonateAnonymousToken(thread));
        }

        public static void RevertToSelf()
            => Error.ThrowLastErrorIfFalse(SecurityImports.RevertToSelf());

        public static unsafe LsaHandle LsaOpenLocalPolicy(PolicyAccessRights access)
        {
            LSA_OBJECT_ATTRIBUTES attributes = default;
            SecurityImports.LsaOpenPolicy(null, &attributes, access, out LsaHandle handle).ThrowIfFailed();
            return handle;
        }

        /// <summary>
        ///  Convert an NTSTATUS to a Windows error code (returns ERROR_MR_MID_NOT_FOUND if unable to find an error)
        /// </summary>
        public static WindowsError NtStatusToWinError(NTStatus status)
        {
            return SecurityImports.LsaNtStatusToWinError(status);
        }

        public static void LsaClose(IntPtr handle) => SecurityImports.LsaClose(handle).ThrowIfFailed();

        public static void LsaFreeMemory(IntPtr handle) => SecurityImports.LsaFreeMemory(handle).ThrowIfFailed();

        /// <summary>
        ///  Enumerates rights explicitly given to the specified SID. If the given SID doesn't have any directly
        ///  applied rights, returns an empty collection.
        /// </summary>
        public static IEnumerable<string> LsaEnumerateAccountRights(LsaHandle policyHandle, in SID sid)
        {
            NTStatus status = SecurityImports.LsaEnumerateAccountRights(policyHandle, in sid, out var rightsBuffer, out uint rightsCount);
            using (rightsBuffer)
            {
                switch (status)
                {
                    case NTStatus.STATUS_OBJECT_NAME_NOT_FOUND:
                        return Enumerable.Empty<string>();
                    case NTStatus.STATUS_SUCCESS:
                        break;
                    default:
                        throw status.GetException();
                }

                List<string> rights = new List<string>();
                Reader reader = new Reader(rightsBuffer);
                for (int i = 0; i < rightsCount; i++)
                    rights.Add(reader.ReadUNICODE_STRING());

                return rights;
            }
        }
    }
}