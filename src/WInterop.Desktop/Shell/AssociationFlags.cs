// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// [ASSOCF]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762471.aspx
    [Flags]
    public enum AssociationFlags
    {
        /// <summary>
        /// None [ASSOCF_NONE]
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Don't map CLSID values to ProgID values. [ASSOCF_INIT_NOREMAPCLSID]
        /// </summary>
        InitNoRemapClsid = 0x00000001,

        /// <summary>
        /// pwszAssoc parameter is an executable file name. [ASSOCF_INIT_BYEXENAME]
        /// </summary>
        InitByExeName = 0x00000002,

        /// <summary>
        /// [ASSOCF_OPEN_BYEXENAME]
        /// </summary>
        OpenByExeName = 0x00000002,

        /// <summary>
        /// If the value isn't under the root key, look under the * subkey. [ASSOCF_INIT_DEFAULTTOSTAR]
        /// </summary>
        InitDefaultToStar = 0x00000004,

        /// <summary>
        /// If the value isn't under the root key, look under the Folder subkey. [ASSOCF_INIT_DEFAULTTOFOLDER]
        /// </summary>
        InitDefaultToFolder = 0x00000008,

        /// <summary>
        /// Specifies that only HKEY_CLASSES_ROOT should be searched, and that HKEY_CURRENT_USER should be ignored.
        /// [ASSOCF_NOUSERSETTINGS]
        /// </summary>
        NoUserSettings = 0x00000010,

        /// <summary>
        /// Specifies that the return string should not be truncated. [ASSOCF_NOTRUNCATE]
        /// Instead, return E_POINTER and the required size for the complete string.
        /// </summary>
        NoTruncate = 0x00000020,

        /// <summary>
        /// Validate that the data is accurate. [ASSOCF_VERIFY}
        /// </summary>
        Verify = 0x00000040,

        /// <summary>
        /// Ignore Rundll.exe and return info about it's target (when a command uses rundll).
        /// [ASSOCF_REMAPRUNDLL}
        /// </summary>
        RemapRundll = 0x00000080,

        /// <summary>
        /// Don't fix errors in the registry, such as friendly names not matching. [ASSOCF_NOFIXUPS]
        /// </summary>
        NoFixups = 0x00000100,

        /// <summary>
        /// Ignore the BaseClass value. [ASSOCF_IGNOREBASECLASS]
        /// </summary>
        IgnoreBaseClass = 0x00000200,

        /// <summary>
        /// Unknown ProgID should be ignored. [ASSOCF_INIT_IGNOREUNKNOWN]
        /// </summary>
        InitIgnoreUnknown = 0x00000400,

        /// <summary>
        /// Supplied ProgID should use system, rather than current user defaults.
        /// [ASSOCF_INIT_FIXED_PROGID]
        /// </summary>
        /// <remarks>New in Windows 8.</remarks>
        InitFixedProgId = 0x00000800,

        /// <summary>
        /// Value is a protocol, and should be mapped using the current user defaults.
        /// [ASSOCF_IS_PROTOCOL]
        /// </summary>
        /// <remarks>New in Windows 8.</remarks>
        IsProtocol = 0x00001000,

        /// <summary>
        /// ProgID corresponds with a file extension based association. Use together with InitFixedProgId.
        /// [ASSOCF_INIT_FOR_FILE]
        /// </summary>
        /// <remarks>New in Windows 8.1.</remarks>
        InitForFile = 0x00002000
    }
}
