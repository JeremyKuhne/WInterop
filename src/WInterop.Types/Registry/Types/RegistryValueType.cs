// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Registry.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724884.aspx
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773476.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff553410.aspx
    public enum RegistryValueType : uint
    {
        /// <summary>
        /// No defined value type.
        /// </summary>
        REG_NONE = 0,

        /// <summary>
        /// Null terminated string.
        /// </summary>
        /// <remarks>
        /// Unicode strings are returned from Unicode functions.
        /// </remarks>
        REG_SZ = 1,

        /// <summary>
        /// Null terminated string with unexpanded references to environment variables.
        /// </summary>
        /// <remarks>
        /// Use ExpandEnvironmentStrings to expand the references.
        /// Unicode strings are returned from Unicode functions.
        /// </remarks>
        REG_EXPAND_SZ = 2,

        /// <summary>
        /// Binary data.
        /// </summary>
        REG_BINARY = 3,

        /// <summary>
        /// 32 bit unsigned integer.
        /// </summary>
        REG_DWORD = 4,

        /// <summary>
        /// Same as REG_DWORD.
        /// </summary>
        REG_DWORD_LITTLE_ENDIAN = 4,

        /// <summary>
        /// 32 bit unsigned integer in big endian format.
        /// </summary>
        REG_DWORD_BIG_ENDIAN = 5,

        /// <summary>
        /// Null terminated string that contains the target path of a symbolic link.
        /// </summary>
        REG_LINK = 6,

        /// <summary>
        /// Sequence of null-terminated strings terminated by an empty string.
        /// </summary>
        REG_MULTI_SZ = 7,

        /// <summary>
        /// A device driver's list of hardware resources, used by the driver or one of
        /// the physical devices it controls, in the \ResourceMap tree.
        /// </summary>
        REG_RESOURCE_LIST = 8,

        /// <summary>
        /// A list of hardware resources that a physical device is using, detected and written
        /// into the \HardwareDescription tree by the system.
        /// </summary>
        REG_FULL_RESOURCE_DESCRIPTOR = 9,

        /// <summary>
        /// A device driver's list of possible hardware resources it or one of the physical
        /// devices it controls can use, from which the system writes a subset into the
        /// \ResourceMap tree.
        /// </summary>
        REG_RESOURCE_REQUIREMENTS_LIST = 10,

        /// <summary>
        /// 64 bit unsigned integer.
        /// </summary>
        REG_QWORD = 11,

        /// <summary>
        /// Same as reg qword.
        /// </summary>
        REG_QWORD_LITTLE_ENDIAN = 11
    }
}
