// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Authorization.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379598.aspx
    public struct SID_IDENTIFIER_AUTHORITY
    {
        public unsafe fixed byte Value[6];

        public byte[] Authority
        {
            get
            {
                byte[] authority = new byte[6];
                unsafe
                {
                    fixed (byte* a = authority)
                    fixed (byte* v = Value)
                    {
                        a[0] = v[0];
                        a[1] = v[1];
                        a[2] = v[2];
                        a[3] = v[3];
                        a[4] = v[4];
                        a[5] = v[5];
                    }
                }

                return authority;
            }
        }

        public static byte[] SECURITY_NULL_SID_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 0 };
        public static byte[] SECURITY_WORLD_SID_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 1 };
        public static byte[] SECURITY_LOCAL_SID_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 2 };
        public static byte[] SECURITY_CREATOR_SID_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 3 };
        public static byte[] SECURITY_NON_UNIQUE_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 4 };
        public static byte[] SECURITY_NT_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 5 };
        public static byte[] SECURITY_RESOURCE_MANAGER_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 9 };
        public static byte[] SECURITY_APP_PACKAGE_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 15 };
        public static byte[] SECURITY_MANDATORY_LABEL_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 16 };
        public static byte[] SECURITY_SCOPED_POLICY_ID_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 17 };
        public static byte[] SECURITY_AUTHENTICATION_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 18 };
        public static byte[] SECURITY_PROCESS_TRUST_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 19 };

        public override bool Equals(object obj)
        {
            if (obj is SID_IDENTIFIER_AUTHORITY)
                return (Equals((SID_IDENTIFIER_AUTHORITY)obj));

            if (obj is byte[])
                return Arrays.AreEquivalent(Authority, (byte[])obj);

            return false;
        }

        public bool Equals(SID_IDENTIFIER_AUTHORITY other)
        {
            return Arrays.AreEquivalent(Authority, other.Authority);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
