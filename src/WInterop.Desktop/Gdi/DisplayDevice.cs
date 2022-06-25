// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [DISPLAY_DEVICE]
/// </summary>
/// <msdn><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/dd183569.aspx"/></msdn>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct DisplayDevice
{
    // Should be 840 bytes
    private uint cb;

    public static unsafe DisplayDevice Create()
    {
        return new DisplayDevice { cb = (uint)sizeof(DisplayDevice) };
    }

    internal unsafe void SetSize() => cb = (uint)sizeof(DisplayDevice);

    public FixedString.Size32 DeviceName;
    public FixedString.Size128 DeviceString;
    public DeviceState StateFlags;
    public FixedString.Size128 DeviceID;
    public FixedString.Size128 DeviceKey;
}