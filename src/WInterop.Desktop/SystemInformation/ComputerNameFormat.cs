// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation
{
    /// <summary>
    ///  Computer name formats. [COMPUTER_NAME_FORMAT]
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms724224.aspx"/>
    /// </summary>
    public enum ComputerNameFormat : uint
    {
        /// <summary>
        ///  [ComputerNameNetBIOS]
        /// </summary>
        NetBIOS,

        /// <summary>
        ///  [ComputerNameDnsHostname]
        /// </summary>
        DnsHostname,

        /// <summary>
        ///  [ComputerNameDnsDomain]
        /// </summary>
        DnsDomain,

        /// <summary>
        ///  [ComputerNameDnsFullyQualified]
        /// </summary>
        DnsFullyQualified,

        /// <summary>
        ///  [ComputerNamePhysicalNetBIOS]
        /// </summary>
        PhysicalNetBIOS,

        /// <summary>
        ///  [ComputerNamePhysicalDnsHostname]
        /// </summary>
        PhysicalDnsHostname,

        /// <summary>
        ///  [ComputerNamePhysicalDnsDomain]
        /// </summary>
        PhysicalDnsDomain,

        /// <summary>
        ///  [ComputerNamePhysicalDnsFullyQualified]
        /// </summary>
        PhysicalDnsFullyQualified
    }
}
