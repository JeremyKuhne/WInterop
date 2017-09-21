// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379598.aspx
    public struct SID_IDENTIFIER_AUTHORITY
    {
        private FixedByte.Size6 _Value;
        public Span<byte> Value => _Value.Buffer;

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
            switch (obj)
            {
                case SID_IDENTIFIER_AUTHORITY authority:
                    return Equals(authority);
                case byte[] bytes:
                    return Value.SequenceEqual(new ReadOnlySpan<byte>(bytes));
                default:
                    return false;
            }
        }

        public bool Equals(SID_IDENTIFIER_AUTHORITY other) => Value.SequenceEqual(other.Value);

        public override int GetHashCode() => base.GetHashCode();
    }
}
