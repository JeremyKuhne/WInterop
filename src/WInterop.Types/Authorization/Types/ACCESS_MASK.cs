// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Describes standard, specific, and generic rights.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx"/>
    /// </remarks>
    public struct ACCESS_MASK
    {
        public const uint STANDARD_RIGHTS_ALL    = 0x001F0000;
        public const uint ACCESS_SYSTEM_SECURITY = 0x01000000;
        public const uint SPECIFIC_RIGHTS_ALL    = 0x0000FFFF;
        public const uint GenericRightsMask      = 0xF0000000;

        public uint Value;

        public StandardAccessRights StandardRights
        {
            get { return (StandardAccessRights)(Value & STANDARD_RIGHTS_ALL); }
            set
            {
                Value &= ~STANDARD_RIGHTS_ALL;
                Value |= (uint)value;
            }
        }

        public GenericAccessRights GenericRights
        {
            get { return (GenericAccessRights)(Value & GenericRightsMask); }
            set
            {
                Value &= ~GenericRightsMask;
                Value |= (uint)value;
            }
        }

        public bool AccessSystemSecurity
        {
            get { return (Value & ACCESS_SYSTEM_SECURITY) != 0; }
            set
            {
                if (value)
                    Value &= ACCESS_SYSTEM_SECURITY;
                else
                    Value &= ~ACCESS_SYSTEM_SECURITY;
            }
        }
    }
}
