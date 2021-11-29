// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices;

/// <summary>
///  [MOUNTMGR_CHANGE_NOTIFY_INFO]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/hardware/ff562273.aspx
public struct MountManagerChangeNotifyInfo
{
    public uint EpicNumber;
}