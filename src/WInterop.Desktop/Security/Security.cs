// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Security.Native;
using WInterop.ErrorHandling;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.SystemInformation;
using System.Collections.Generic;
using System.Linq;

namespace WInterop.Security
{
    public static partial class Security
    {
        // In winnt.h
        private const uint PRIVILEGE_SET_ALL_NECESSARY = 1;

        /// <summary>
        /// Checks if the given privilege is enabled. This does not tell you whether or not it
        /// is possible to get a privilege- most held privileges are not enabled by default.
        /// </summary>
        public unsafe static bool IsPrivilegeEnabled(this AccessToken token, Privilege privilege)
        {
            LUID luid = LookupPrivilegeValue(privilege);

            var luidAttributes = new LuidAndAttributes { Luid = luid };

            var set = new PRIVILEGE_SET
            {
                Control = PRIVILEGE_SET_ALL_NECESSARY,
                PrivilegeCount = 1,
                Privilege = new LuidAndAttributes { Luid = luid }
            };

            if (!Imports.PrivilegeCheck(token, &set, out BOOL result))
                throw Errors.GetIoExceptionForLastError(privilege.ToString());

            return result;
        }

        /// <summary>
        /// Returns true if all of the given privileges are enabled for the current process.
        /// </summary>
        public static bool AreAllPrivilegesEnabled(this AccessToken token, params Privilege[] privileges)
        {
            return ArePrivilegesEnabled(token, all: true, privileges: privileges);
        }

        /// <summary>
        /// Returns true if any of the given privileges are enabled for the current process.
        /// </summary>
        public static bool AreAnyPrivilegesEnabled(this AccessToken token, params Privilege[] privileges)
        {
            return ArePrivilegesEnabled(token, all: false, privileges: privileges);
        }

        private unsafe static bool ArePrivilegesEnabled(this AccessToken token, bool all, Privilege[] privileges)
        {
            if (privileges == null || privileges.Length == 0)
                return true;

            byte* buffer = stackalloc byte[sizeof(PRIVILEGE_SET) + (sizeof(LuidAndAttributes) * (privileges.Length - 1))];
            PRIVILEGE_SET* set = (PRIVILEGE_SET*)buffer;
            set->Control = all ? PRIVILEGE_SET_ALL_NECESSARY : 0;
            set->PrivilegeCount = (uint)privileges.Length;
            Span<LuidAndAttributes> luids = new Span<LuidAndAttributes>(&set->Privilege, privileges.Length);
            for (int i = 0; i < privileges.Length; i++)
            {
                luids[i] = new LuidAndAttributes { Luid = LookupPrivilegeValue(privileges[i]) };
            }

            if (!Imports.PrivilegeCheck(token, set, out BOOL result))
                throw Errors.GetIoExceptionForLastError();

            return result;
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
                string name = SystemInformation.SystemInformation.GetUserName(ExtendedNameFormat.SamCompatible);

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

        public unsafe static AccessToken CreateRestrictedToken(this AccessToken token, params SID[] sidsToDisable)
        {
            if (sidsToDisable == null || sidsToDisable.Length == 0)
                throw new ArgumentNullException();

            SID_AND_ATTRIBUTES* sids = stackalloc SID_AND_ATTRIBUTES[sidsToDisable.Length];
            fixed (SID* sid = sidsToDisable)
            {
                for (int i = 0; i < sidsToDisable.Length; i++)
                    sids[i].Sid = &sid[i];

                if (!Imports.CreateRestrictedToken(token, 0, (uint)sidsToDisable.Length, sids, 0, null, 0, null, out AccessToken restricted))
                {
                    throw Errors.GetIoExceptionForLastError();
                }

                return restricted;
            }
        }

        public static void ImpersonateLoggedOnUser(this AccessToken token)
        {
            if (!Imports.ImpersonateLoggedOnUser(token))
                throw Errors.GetIoExceptionForLastError();
        }

        public static void RevertToSelf()
        {
            if (!Imports.RevertToSelf())
                throw Errors.GetIoExceptionForLastError();
        }

        public static LsaHandle LsaOpenLocalPolicy(PolicyAccessRights access)
        {
            LSA_OBJECT_ATTRIBUTES attributes = new LSA_OBJECT_ATTRIBUTES();
            LsaHandle handle;
            NTSTATUS status;

            unsafe
            {
                status = Imports.LsaOpenPolicy(null, &attributes, access, out handle);
            }

            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);

            return handle;
        }

        /// <summary>
        /// Convert an NTSTATUS to a Windows error code (returns ERROR_MR_MID_NOT_FOUND if unable to find an error)
        /// </summary>
        public static WindowsError NtStatusToWinError(NTSTATUS status)
        {
            return Imports.LsaNtStatusToWinError(status);
        }

        public static void LsaClose(IntPtr handle)
        {
            NTSTATUS status = Imports.LsaClose(handle);
            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);
        }

        public static void LsaFreeMemory(IntPtr handle)
        {
            NTSTATUS status = Imports.LsaFreeMemory(handle);
            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);
        }

        /// <summary>
        /// Enumerates rights explicitly given to the specified SID. If the given SID
        /// doesn't have any directly applied rights, returns an empty collection.
        /// </summary>
        public static IEnumerable<string> LsaEnumerateAccountRights(LsaHandle policyHandle, in SID sid)
        {
            NTSTATUS status = Imports.LsaEnumerateAccountRights(policyHandle, in sid, out var rightsBuffer, out uint rightsCount);
            switch (status)
            {
                case NTSTATUS.STATUS_OBJECT_NAME_NOT_FOUND:
                    return Enumerable.Empty<string>();
                case NTSTATUS.STATUS_SUCCESS:
                    break;
                default:
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);
            }

            List<string> rights = new List<string>();
            Reader reader = new Reader(rightsBuffer);
            for (int i = 0; i < rightsCount; i++)
                rights.Add(reader.ReadUNICODE_STRING());

            return rights;
        }
    }
}
