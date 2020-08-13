// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Registry
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724884.aspx
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773476.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff553410.aspx
    public enum RegistryValueType : uint
    {
        /// <summary>
        ///  No defined value type. [REG_NONE]
        /// </summary>
        None = 0,

        /// <summary>
        ///  Null terminated string. [REG_SZ]
        /// </summary>
        /// <remarks>
        ///  Unicode strings are returned from Unicode functions.
        /// </remarks>
        String = 1,

        /// <summary>
        ///  Null terminated string with unexpanded references to environment variables. [REG_EXPAND_SZ]
        /// </summary>
        /// <remarks>
        ///  Use ExpandEnvironmentStrings to expand the references.
        ///  Unicode strings are returned from Unicode functions.
        /// </remarks>
        ExpandString = 2,

        /// <summary>
        ///  Binary data. [REG_BINARY]
        /// </summary>
        Binary = 3,

        /// <summary>
        ///  32 bit unsigned integer. [REG_DWORD]
        /// </summary>
        Unsigned32BitInteger = 4,

        /// <summary>
        ///  Same as REG_DWORD. [REG_DWORD_LITTLE_ENDIAN]
        /// </summary>
        Unsigned32BitIntegerLittleEndian = 4,

        /// <summary>
        ///  32 bit unsigned integer in big endian format. [REG_DWORD_BIG_ENDIAN]
        /// </summary>
        Unsigned32BitIntegerBigEndian = 5,

        /// <summary>
        ///  Null terminated string that contains the target path of a symbolic link. [REG_LINK]
        /// </summary>
        SymbolicLink = 6,

        /// <summary>
        ///  Sequence of null-terminated strings terminated by an empty string. [REG_MULTI_SZ]
        /// </summary>
        MultiString = 7,

        /// <summary>
        ///  A device driver's list of hardware resources, used by the driver or one of
        ///  the physical devices it controls, in the \ResourceMap tree. [REG_RESOURCE_LIST]
        /// </summary>
        ResourceList = 8,

        /// <summary>
        ///  A list of hardware resources that a physical device is using, detected and written
        ///  into the \HardwareDescription tree by the system. [REG_FULL_RESOURCE_DESCRIPTOR]
        /// </summary>
        FullResourceDescriptor = 9,

        /// <summary>
        ///  A device driver's list of possible hardware resources it or one of the physical
        ///  devices it controls can use, from which the system writes a subset into the
        ///  \ResourceMap tree. [REG_RESOURCE_REQUIREMENTS_LIST]
        /// </summary>
        ResourceRequirementsList = 10,

        /// <summary>
        ///  64 bit unsigned integer. [REG_QWORD]
        /// </summary>
        Unsigned64BitInteger = 11,

        /// <summary>
        ///  Same as reg qword. [REG_QWORD_LITTLE_ENDIAN]
        /// </summary>
        Unsigned64BitIntegerLittleEndian = 11
    }
}
