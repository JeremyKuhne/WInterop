// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724224.aspx
    public enum COMPUTER_NAME_FORMAT : uint
    {
        ComputerNameNetBIOS,
        ComputerNameDnsHostname,
        ComputerNameDnsDomain,
        ComputerNameDnsFullyQualified,
        ComputerNamePhysicalNetBIOS,
        ComputerNamePhysicalDnsHostname,
        ComputerNamePhysicalDnsDomain,
        ComputerNamePhysicalDnsFullyQualified
        // ComputerNameMax just marks the end of the enum, isn't really used
        // ComputerNameMax
    }
}
