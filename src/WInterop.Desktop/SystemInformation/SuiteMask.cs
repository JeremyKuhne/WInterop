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
        ///  Small Business Server was installed, but may have been upgraded.
        /// <see cref="SmallBusinessRestricted"/> will additionally be set if not upgraded to a different edition.
        ///  [VER_SUITE_SMALLBUSINESS]
        /// </summary>
        SmallBusiness = 0x00000001,

        /// <summary>
        ///  Enterprise edition is installed. [VER_SUITE_ENTERPRISE]
        /// </summary>
        Enterprise = 0x00000002,

        /// <summary>
        ///  Microsoft BackOffice components are installed. [VER_SUITE_BACKOFFICE]
        /// </summary>
        BackOffice = 0x00000004,

        /// <summary>
        ///  Communications Server is installed. [VER_SUITE_COMMUNICATIONS]
        /// </summary>
        Communications = 0x00000008,

        /// <summary>
        ///  Terminal Services is installed. This is always true. [VER_SUITE_TERMINAL]
        /// </summary>
        Terminal = 0x00000010,

        /// <summary>
        ///  See <see cref="SmallBusiness"/>. [VER_SUITE_SMALLBUSINESS_RESTRICTED]
        /// </summary>
        SmallBusinessRestricted = 0x00000020,

        /// <summary>
        ///  Windows XP Embedded. [VER_SUITE_EMBEDDEDNT]
        /// </summary>
        EmbeddedNT = 0x00000040,

        /// <summary>
        ///  Datacenter edition is installed. [VER_SUITE_DATACENTER]
        /// </summary>
        DataCenter = 0x00000080,

        /// <summary>
        ///  If this is not set the system is in application server mode. [VER_SUITE_SINGLEUSERTS]
        /// </summary>
        SingleUserTerminalServices = 0x00000100,

        /// <summary>
        ///  Home edition is installed. [VER_SUITE_PERSONAL]
        /// </summary>
        Personal = 0x00000200,

        /// <summary>
        ///  Web edition is installed. [VER_SUITE_BLADE]
        /// </summary>
        Blade = 0x00000400,

        /// <summary>
        ///  [VER_SUITE_EMBEDDED_RESTRICTED]
        /// </summary>
        EmbeddedRestricted = 0x00000800,

        /// <summary>
        ///  [VER_SUITE_SECURITY_APPLIANCE]
        /// </summary>
        SecurityAppliance = 0x00001000,

        /// <summary>
        ///  Storage Server edition is installed. [VER_SUITE_STORAGE_SERVER]
        /// </summary>
        StorageServer = 0x00002000,

        /// <summary>
        ///  Compute Cluster edition is installed. [VER_SUITE_COMPUTE_SERVER]
        /// </summary>
        ComputeServer = 0x00004000,

        /// <summary>
        ///  Home Server edition is installed. [VER_SUITE_WH_SERVER]
        /// </summary>
        WindowsHomeServer = 0x00008000
    }
}