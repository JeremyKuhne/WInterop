// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Memory;
using WInterop.Security.Native;

namespace WInterop.Security
{
    /// <summary>
    /// Wrapper for ACLs ([SACL]/[DACL]). Discretionary ACLs (DACLs) are
    /// rights to the given object. System ACLs (SACLs) are for logging
    /// of object access.
    /// </summary>
    public unsafe class SecurityDescriptor : LocalHandle
    {
        private SECURITY_DESCRIPTOR_CONTROL _control;
        private SafeHandle _objectHandle;
        private ObjectType _objectType;

        public SecurityDescriptor() : base() { }

        public SecurityDescriptor(SafeHandle objectHandle, ObjectType objectType, SECURITY_DESCRIPTOR* descriptor, bool ownsHandle = true)
            : base((IntPtr)descriptor, ownsHandle)
        {
            _control = descriptor->Control;
            _objectHandle = objectHandle;
            _objectType = objectType;
        }

        public bool HasOwnerSid => handle != IntPtr.Zero;
        public bool HasGroupSid => handle != IntPtr.Zero;
        public bool HasDiscretionaryAcl => handle != IntPtr.Zero && (_control & SECURITY_DESCRIPTOR_CONTROL.SE_DACL_PRESENT) != 0;
        public bool HasSystemAcl => handle != IntPtr.Zero && (_control & SECURITY_DESCRIPTOR_CONTROL.SE_SACL_PRESENT) != 0;

        private bool IsRelative => (_control & SECURITY_DESCRIPTOR_CONTROL.SE_SELF_RELATIVE) != 0;

        private ACL* DACL =>
            HasDiscretionaryAcl
                ? IsRelative
                    ? (ACL*)IntPtr.Add(handle, (int)((SECURITY_DESCRIPTOR_RELATIVE*)handle)->Dacl).ToPointer()
                    : ((SECURITY_DESCRIPTOR*)handle)->Dacl
                : null;

        private ACL* SACL =>
            HasSystemAcl
                ? IsRelative
                    ? (ACL*)IntPtr.Add(handle, (int)((SECURITY_DESCRIPTOR_RELATIVE*)handle)->Sacl).ToPointer()
                    : ((SECURITY_DESCRIPTOR*)handle)->Sacl
                : null;

        public IEnumerable<ExplicitAccess> GetExplicitEntriesFromAcl(bool systemAcl = false)
        {
            if ((systemAcl && !HasSystemAcl) || (!systemAcl && !HasDiscretionaryAcl))
                throw new InvalidOperationException("The specified ACL isn't present on the Security Descriptor");

            EXPLICIT_ACCESS* entries;
            Imports.GetExplicitEntriesFromAclW(systemAcl ? SACL : DACL, out uint count, &entries).ThrowIfFailed();
            return new ExplicitAccessEnumerable(entries, count);
        }

        // Note that this descriptor will be stale after calling this
        public void SetAclAccess(in SID sid, AccessMask access, AccessMode mode, Inheritance inheritance = default, bool systemAcl = false)
        {
            EXPLICIT_ACCESS ea;
            fixed (SID* sidp = &sid)
            {
                ea = new EXPLICIT_ACCESS
                {
                    grfAccessPermissions = access,
                    grfAccessMode = mode,
                    grfInheritance = inheritance,
                    Trustee = new TRUSTEE(sidp)
                };
            }

            ACL* newAcl;
            Imports.SetEntriesInAclW(1, &ea, systemAcl ? SACL : DACL, &newAcl).ThrowIfFailed();

            try
            {
                Imports.SetSecurityInfo(
                    _objectHandle,
                    _objectType,
                    systemAcl ? SecurityInformation.Sacl : SecurityInformation.Dacl,
                    null,
                    null,
                    systemAcl ? null : newAcl,
                    systemAcl ? newAcl : null)
                    .ThrowIfFailed();
            }
            finally
            {
                if (newAcl != null)
                    Memory.Memory.LocalFree((IntPtr)newAcl);
            }
        }

        private class ExplicitAccessEnumerable : LocalHandle, IEnumerable<ExplicitAccess>, IEnumerator<ExplicitAccess>
        {
            private uint _count;
            private int _current;
            private EXPLICIT_ACCESS* _entries;

            public ExplicitAccessEnumerable() : base() { }

            public unsafe ExplicitAccessEnumerable(EXPLICIT_ACCESS* entries, uint count, bool ownsHandle = true)
                : base((IntPtr)entries, ownsHandle)
            {
                _count = count;
                _entries = entries;
                Reset();
            }

            public IEnumerator<ExplicitAccess> GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public void Reset() => _current = -1;
            object IEnumerator.Current => Current;

            public ExplicitAccess Current
                => (_current >= 0 && _current < _count) ? new ExplicitAccess(_entries + _current) : throw new InvalidOperationException();

            public bool MoveNext()
            {
                if (_current + 1 < _count)
                {
                    _current++;
                    return true;
                }
                return false;
            }
        }
    }
}
