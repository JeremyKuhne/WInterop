// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Storage;

namespace WInterop.Security
{
    /// <summary>
    /// Describes standard, specific, and generic rights. [ACCESS_MASK]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx"/>
    /// </remarks>
    public struct AccessMask
    {
        public const uint GenericRightsMask = 0xF0000000;

        public uint Value;

        public AccessMask(uint value) => Value = value;

        public StandardAccessRights StandardRights
        {
            get { return (StandardAccessRights)(Value & (uint)StandardAccessRights.All); }
            set
            {
                Value &= ~(uint)StandardAccessRights.All;
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
            get { return (Value & (uint)StandardAccessRights.AccessSystemSecurity) != 0; }
            set
            {
                if (value)
                    Value &= (uint)StandardAccessRights.AccessSystemSecurity;
                else
                    Value &= ~(uint)StandardAccessRights.AccessSystemSecurity;
            }
        }

        public static implicit operator AccessMask(FileAccessRights rights) => new AccessMask((uint)rights);
        public static implicit operator FileAccessRights(AccessMask mask) => (FileAccessRights)mask.Value;
        public static implicit operator AccessMask(DirectoryAccessRights rights) => new AccessMask((uint)rights);
        public static implicit operator DirectoryAccessRights(AccessMask mask) => (DirectoryAccessRights)mask.Value;
    }
}
