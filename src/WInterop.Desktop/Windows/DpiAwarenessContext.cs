// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows;

[StructLayout(LayoutKind.Sequential)]
public struct DpiAwarenessContext
{
    private readonly nint _value;

    public DpiAwarenessContext(nint value) => _value = value;

    public bool IsNull => _value == 0;

    // These only have meaning for setting the value
    public static DpiAwarenessContext Unaware = new(-1);
    public static DpiAwarenessContext SystemAware = new(-2);
    public static DpiAwarenessContext PerMonitor = new(-3);

    // Added in 1803
    public static DpiAwarenessContext PerMonitorV2 = new(-4);

    // Added in 1809
    public static DpiAwarenessContext UnawareGdiScaled = new(-5);
}