// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications;

// https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
public enum ProviderSubType : uint
{
    Unspecified = 0x00000000,
    Rs232 = 0x00000001,
    ParallelPort = 0x00000002,
    Rs422 = 0x00000003,
    Rs423 = 0x00000004,
    Rs449 = 0x00000005,
    Modem = 0x00000006,
    Fax = 0x00000021,
    Scanner = 0x00000022,
    NetworkBridge = 0x00000100,
    Lat = 0x00000101,
    TcpipTelnet = 0x00000102,
    X25 = 0x00000103
}