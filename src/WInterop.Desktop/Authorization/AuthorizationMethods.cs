// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.SystemInformation;
using WInterop.SystemInformation.Types;

namespace WInterop.Authorization
{
    public static partial class AuthorizationMethods
    {
        // In winnt.h
        private const uint PRIVILEGE_SET_ALL_NECESSARY = 1;

        /// <summary>
        /// Checks if the given privilege is enabled. This does not tell you whether or not it
        /// is possible to get a privilege- most held privileges are not enabled by default.
        /// </summary>
        public unsafe static bool IsPrivilegeEnabled(AccessToken token, Privilege privilege)
        {
            LUID luid = LookupPrivilegeValue(privilege);

            var luidAttributes = new LUID_AND_ATTRIBUTES { Luid = luid };

            var set = new PRIVILEGE_SET
            {
                Control = PRIVILEGE_SET_ALL_NECESSARY,
                PrivilegeCount = 1,
                Privilege = new LUID_AND_ATTRIBUTES { Luid = luid }
            };

            if (!Imports.PrivilegeCheck(token, &set, out BOOL result))
                throw Errors.GetIoExceptionForLastError(privilege.ToString());

            return result;
        }

        /// <summary>
        /// Returns true if all of the given privileges are enabled for the current process.
        /// </summary>
        public static bool AreAllPrivilegesEnabled(AccessToken token, params Privilege[] privileges)
        {
            return ArePrivilegesEnabled(token, all: true, privileges: privileges);
        }

        /// <summary>
        /// Returns true if any of the given privileges are enabled for the current process.
        /// </summary>
        public static bool AreAnyPrivilegesEnabled(AccessToken token, params Privilege[] privileges)
        {
            return ArePrivilegesEnabled(token, all: false, privileges: privileges);
        }

        private unsafe static bool ArePrivilegesEnabled(AccessToken token, bool all, Privilege[] privileges)
        {
            if (privileges == null || privileges.Length == 0)
                return true;

            byte* buffer = stackalloc byte[sizeof(PRIVILEGE_SET) + (sizeof(LUID_AND_ATTRIBUTES) * (privileges.Length - 1))];
            PRIVILEGE_SET* set = (PRIVILEGE_SET*)buffer;
            set->Control = all ? PRIVILEGE_SET_ALL_NECESSARY : 0;
            set->PrivilegeCount = (uint)privileges.Length;
            Span<LUID_AND_ATTRIBUTES> luids = new Span<LUID_AND_ATTRIBUTES>(&set->Privilege, privileges.Length);
            for (int i = 0; i < privileges.Length; i++)
            {
                luids[i] = new LUID_AND_ATTRIBUTES { Luid = LookupPrivilegeValue(privileges[i]) };
            }

            if (!Imports.PrivilegeCheck(token, set, out BOOL result))
                throw Errors.GetIoExceptionForLastError();

            return result;
        }

        /// <summary>
        /// Gets the info (name, domain name, usage) for the given SID.
        /// </summary>
        public static AccountSidInformation LookupAccountSidLocal(in SID sid)
        {
            var wrapper = new LookupAccountSidLocalWrapper(in sid);
            return BufferHelper.TwoBufferInvoke<LookupAccountSidLocalWrapper, StringBuffer, AccountSidInformation>(ref wrapper);
        }

        private readonly struct LookupAccountSidLocalWrapper : ITwoBufferFunc<StringBuffer, AccountSidInformation>
        {
            private readonly SID _sid;

            public LookupAccountSidLocalWrapper(in SID sid)
            {
                _sid = sid;
            }

            AccountSidInformation ITwoBufferFunc<StringBuffer, AccountSidInformation>.Func(StringBuffer nameBuffer, StringBuffer domainNameBuffer)
            {
                SidNameUse usage;
                uint nameCharCapacity = nameBuffer.CharCapacity;
                uint domainNameCharCapacity = domainNameBuffer.CharCapacity;

                while (!Imports.LookupAccountSidLocalW(
                    in _sid,
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

                return new AccountSidInformation
                {
                    Name = nameBuffer.ToString(),
                    DomainName = domainNameBuffer.ToString(),
                    Usage = usage
                };
            }
        }

        /// <summary>
        /// Get the current domain name.
        /// </summary>
        public static string GetDomainName()
        {
            var wrapper = new GetDomainNameWrapper();
            return BufferHelper.TwoBufferInvoke<GetDomainNameWrapper, StringBuffer, string>(ref wrapper);
        }

        private struct GetDomainNameWrapper : ITwoBufferFunc<StringBuffer, string>
        {
            unsafe string ITwoBufferFunc<StringBuffer, string>.Func(StringBuffer nameBuffer, StringBuffer domainNameBuffer)
            {
                string name = SystemInformationMethods.GetUserName(EXTENDED_NAME_FORMAT.NameSamCompatible);

                SID sid = new SID();
                uint sidLength = (uint)sizeof(SID);
                uint domainNameLength = domainNameBuffer.CharCapacity;
                while (!Imports.LookupAccountNameW(
                    lpSystemName: null,
                    lpAccountName: name,
                    Sid: &sid,
                    cbSid: ref sidLength,
                    ReferencedDomainName: domainNameBuffer.CharPointer,
                    cchReferencedDomainName: ref domainNameLength,
                    peUse: out _))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    domainNameBuffer.EnsureCharCapacity(domainNameLength);
                }

                domainNameBuffer.Length = domainNameLength;
                return domainNameBuffer.ToString();
            }
        }
    }
}
