// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.SystemInformation
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/mt668928.aspx
    [Flags]
    public enum SuiteMask : uint
    {
        /// <summary>
        /// Small Business Server was installed, but may have been upgraded.
        /// VER_SUITE_SMALLBUSINESS_RESTRICTED will additionally be set if not upgraded to a different edition.
        /// </summary>
        VER_SUITE_SMALLBUSINESS            = 0x00000001,

        /// <summary>
        /// Enterprise edition is installed.
        /// </summary>
        VER_SUITE_ENTERPRISE               = 0x00000002,

        /// <summary>
        /// Microsoft BackOffice components are installed.
        /// </summary>
        VER_SUITE_BACKOFFICE               = 0x00000004,

        /// <summary>
        /// Communications Server is installed.
        /// </summary>
        VER_SUITE_COMMUNICATIONS           = 0x00000008,

        /// <summary>
        /// Terminal Services is installed. This is always true.
        /// </summary>
        VER_SUITE_TERMINAL                 = 0x00000010,

        /// <summary>
        /// See VER_SUITE_SMALLBUSINESS.
        /// </summary>
        VER_SUITE_SMALLBUSINESS_RESTRICTED = 0x00000020,

        /// <summary>
        /// Windows XP Embedded.
        /// </summary>
        VER_SUITE_EMBEDDEDNT               = 0x00000040,

        /// <summary>
        /// Datacenter edition is installed.
        /// </summary>
        VER_SUITE_DATACENTER               = 0x00000080,

        /// <summary>
        /// If this is not set the system is in application server mode.
        /// </summary>
        VER_SUITE_SINGLEUSERTS             = 0x00000100,

        /// <summary>
        /// Home edition is installed.
        /// </summary>
        VER_SUITE_PERSONAL                 = 0x00000200,

        /// <summary>
        /// Web edition is installed.
        /// </summary>
        VER_SUITE_BLADE                    = 0x00000400,
        VER_SUITE_EMBEDDED_RESTRICTED      = 0x00000800,
        VER_SUITE_SECURITY_APPLIANCE       = 0x00001000,

        /// <summary>
        /// Storage Server edition is installed.
        /// </summary>
        VER_SUITE_STORAGE_SERVER           = 0x00002000,

        /// <summary>
        /// Compute Cluster edition is installed.
        /// </summary>
        VER_SUITE_COMPUTE_SERVER           = 0x00004000,

        /// <summary>
        /// Home Server edition is installed.
        /// </summary>
        VER_SUITE_WH_SERVER                = 0x00008000
    }
}
